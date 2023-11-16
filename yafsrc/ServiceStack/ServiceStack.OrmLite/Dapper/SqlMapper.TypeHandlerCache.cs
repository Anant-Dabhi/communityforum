﻿// ***********************************************************************
// <copyright file="SqlMapper.TypeHandlerCache.cs" company="ServiceStack, Inc.">
//     Copyright (c) ServiceStack, Inc. All Rights Reserved.
// </copyright>
// <summary>Fork for YetAnotherForum.NET, Licensed under the Apache License, Version 2.0</summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Data;

namespace ServiceStack.OrmLite.Dapper;

/// <summary>
/// Class SqlMapper.
/// </summary>
public static partial class SqlMapper
{
    /// <summary>
    /// Not intended for direct usage
    /// </summary>
    /// <typeparam name="T">The type to have a cache for.</typeparam>
    [Obsolete(ObsoleteInternalUsageOnly, false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeHandlerCache<T>
    {
        /// <summary>
        /// Not intended for direct usage.
        /// </summary>
        /// <param name="value">The object to parse.</param>
        /// <returns>T.</returns>
        [Obsolete(ObsoleteInternalUsageOnly, true)]
        public static T Parse(object value) => (T)handler.Parse(typeof(T), value);

        /// <summary>
        /// Not intended for direct usage.
        /// </summary>
        /// <param name="parameter">The parameter to set a value for.</param>
        /// <param name="value">The value to set.</param>
        [Obsolete(ObsoleteInternalUsageOnly, true)]
        public static void SetValue(IDbDataParameter parameter, object value) => handler.SetValue(parameter, value);

        /// <summary>
        /// Sets the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        static internal void SetHandler(ITypeHandler handler)
        {
#pragma warning disable 618
            TypeHandlerCache<T>.handler = handler;
#pragma warning restore 618
        }

        /// <summary>
        /// The handler
        /// </summary>
        private static ITypeHandler handler;
    }
}