﻿// ***********************************************************************
// <copyright file="SqlDataRecordListTVPParameter.cs" company="ServiceStack, Inc.">
//     Copyright (c) ServiceStack, Inc. All Rights Reserved.
// </copyright>
// <summary>Fork for YetAnotherForum.NET, Licensed under the Apache License, Version 2.0</summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ServiceStack.OrmLite.Dapper;

/// <summary>
/// Used to pass a IEnumerable&lt;SqlDataRecord&gt; as a SqlDataRecordListTVPParameter
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class SqlDataRecordListTVPParameter<T> : SqlMapper.ICustomQueryParameter
    where T : IDataRecord
{
    /// <summary>
    /// The data
    /// </summary>
    private readonly IEnumerable<T> data;
    /// <summary>
    /// The type name
    /// </summary>
    private readonly string typeName;
    /// <summary>
    /// Create a new instance of <see cref="SqlDataRecordListTVPParameter&lt;T&gt;" />.
    /// </summary>
    /// <param name="data">The data records to convert into TVPs.</param>
    /// <param name="typeName">The parameter type name.</param>
    public SqlDataRecordListTVPParameter(IEnumerable<T> data, string typeName)
    {
        this.data = data;
        this.typeName = typeName;
    }

    /// <summary>
    /// Add the parameter needed to the command before it executes
    /// </summary>
    /// <param name="command">The raw command prior to execution</param>
    /// <param name="name">Parameter name</param>
    void SqlMapper.ICustomQueryParameter.AddParameter(IDbCommand command, string name)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        Set(param, data, typeName);
        command.Parameters.Add(param);
    }

    /// <summary>
    /// Sets the specified parameter.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="data">The data.</param>
    /// <param name="typeName">Name of the type.</param>
    static internal void Set(IDbDataParameter parameter, IEnumerable<T> data, string typeName)
    {
        parameter.Value = data != null && data.Any() ? data : null;
        StructuredHelper.ConfigureTVP(parameter, typeName);
    }
}
/// <summary>
/// Class StructuredHelper.
/// </summary>
static class StructuredHelper
{
    /// <summary>
    /// The s udt
    /// </summary>
    private readonly static Hashtable s_udt = new Hashtable(), s_tvp = new Hashtable();

    /// <summary>
    /// Gets the udt.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>Action&lt;IDbDataParameter, System.String&gt;.</returns>
    private static Action<IDbDataParameter, string> GetUDT(Type type)
        => (Action<IDbDataParameter, string>)s_udt[type] ?? SlowGetHelper(type, s_udt, "UdtTypeName", 29); // 29 = SqlDbType.Udt (avoiding ref)
    /// <summary>
    /// Gets the TVP.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>Action&lt;IDbDataParameter, System.String&gt;.</returns>
    private static Action<IDbDataParameter, string> GetTVP(Type type)
        => (Action<IDbDataParameter, string>)s_tvp[type] ?? SlowGetHelper(type, s_tvp, "TypeName", 30); // 30 = SqlDbType.Structured (avoiding ref)

    /// <summary>
    /// Slows the get helper.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="hashtable">The hashtable.</param>
    /// <param name="nameProperty">The name property.</param>
    /// <param name="sqlDbType">Type of the SQL database.</param>
    /// <returns>Action&lt;IDbDataParameter, System.String&gt;.</returns>
    static Action<IDbDataParameter, string> SlowGetHelper(Type type, Hashtable hashtable, string nameProperty, int sqlDbType)
    {
        lock (hashtable)
        {
            var helper = (Action<IDbDataParameter, string>)hashtable[type];
            if (helper == null)
            {
                helper = CreateFor(type, nameProperty, sqlDbType);
                hashtable.Add(type, helper);
            }
            return helper;
        }
    }

    /// <summary>
    /// Creates for.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="nameProperty">The name property.</param>
    /// <param name="sqlDbType">Type of the SQL database.</param>
    /// <returns>Action&lt;IDbDataParameter, System.String&gt;.</returns>
    static Action<IDbDataParameter, string> CreateFor(Type type, string nameProperty, int sqlDbType)
    {
        var name = type.GetProperty(nameProperty, BindingFlags.Public | BindingFlags.Instance);
        if (name == null || !name.CanWrite)
        {
            return (p, n) => { };
        }

        var dbType = type.GetProperty("SqlDbType", BindingFlags.Public | BindingFlags.Instance);
        if (dbType != null && !dbType.CanWrite) dbType = null;

        var dm = new DynamicMethod(nameof(CreateFor) + "_" + type.Name, null,
            new[] { typeof(IDbDataParameter), typeof(string) }, true);
        var il = dm.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Castclass, type);
        il.Emit(OpCodes.Ldarg_1);
        il.EmitCall(OpCodes.Callvirt, name.GetSetMethod(), null);

        if (dbType != null)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, type);
            il.Emit(OpCodes.Ldc_I4, sqlDbType);
            il.EmitCall(OpCodes.Callvirt, dbType.GetSetMethod(), null);
        }

        il.Emit(OpCodes.Ret);
        return (Action<IDbDataParameter, string>)dm.CreateDelegate(typeof(Action<IDbDataParameter, string>));

    }

    // this needs to be done per-provider; "dynamic" doesn't work well on all runtimes, although that
    // would be a fair option otherwise
    /// <summary>
    /// Configures the udt.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="typeName">Name of the type.</param>
    static internal void ConfigureUDT(IDbDataParameter parameter, string typeName)
        => GetUDT(parameter.GetType())(parameter, typeName);
    /// <summary>
    /// Configures the TVP.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="typeName">Name of the type.</param>
    static internal void ConfigureTVP(IDbDataParameter parameter, string typeName)
        => GetTVP(parameter.GetType())(parameter, typeName);
}