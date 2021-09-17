﻿// ***********************************************************************
// <copyright file="OrmLiteReadExpressionsApiAsyncLegacy.cs" company="ServiceStack, Inc.">
//     Copyright (c) ServiceStack, Inc. All Rights Reserved.
// </copyright>
// <summary>Fork for YetAnotherForum.NET, Licensed under the Apache License, Version 2.0</summary>
// ***********************************************************************
#if ASYNC
// Copyright (c) ServiceStack, Inc. All Rights Reserved.
// License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceStack.OrmLite.Legacy
{
    /// <summary>
    /// Class OrmLiteReadExpressionsApiAsyncLegacy.
    /// </summary>
    [Obsolete(Messages.LegacyApi)]
    public static class OrmLiteReadExpressionsApiAsyncLegacy
    {
        /// <summary>
        /// Returns results from using an SqlExpression lambda. E.g:
        /// <para>db.Select&lt;Person&gt;(q =&gt; q.Where(x =&gt; x.Age &gt; 40))</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        [Obsolete("Use db.SelectAsync(db.From<T>())")]
        public static Task<List<T>> SelectAsync<T>(this IDbConnection dbConn, Func<SqlExpression<T>, SqlExpression<T>> expression, CancellationToken token = default)
        {
            return dbConn.Exec(dbCmd => dbCmd.SelectAsync(expression, token));
        }

        /// <summary>
        /// Project results from a number of joined tables into a different model
        /// </summary>
        /// <typeparam name="Into">The type of the into.</typeparam>
        /// <typeparam name="From">The type of from.</typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;List&lt;Into&gt;&gt;.</returns>
        [Obsolete("Use db.SelectAsync<Into, From>(db.From<T>())")]
        public static Task<List<Into>> SelectAsync<Into, From>(this IDbConnection dbConn, Func<SqlExpression<From>, SqlExpression<From>> expression, CancellationToken token = default)
        {
            return dbConn.Exec(dbCmd => dbCmd.SelectAsync<Into, From>(expression, token));
        }

        /// <summary>
        /// Returns a single result from using an SqlExpression lambda. E.g:
        /// <para>db.Single&lt;Person&gt;(q =&gt; q.Where(x =&gt; x.Age == 42))</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        [Obsolete("Use db.SingleAsync(db.From<T>())")]
        public static Task<T> SingleAsync<T>(this IDbConnection dbConn, Func<SqlExpression<T>, SqlExpression<T>> expression, CancellationToken token = default)
        {
            return dbConn.Exec(dbCmd => dbCmd.SingleAsync(expression, token));
        }

        /// <summary>
        /// Returns the first result using a SqlFormat query. E.g:
        /// <para>db.SingleFmt&lt;Person&gt;("Age = {0}", 42)</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="sqlFormat">The SQL format.</param>
        /// <param name="filterParams">The filter parameters.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        [Obsolete("Use db.SingleAsync(\"Age = @age\", new { age = 42 })")]
        public static Task<T> SingleFmtAsync<T>(this IDbConnection dbConn, CancellationToken token, string sqlFormat, params object[] filterParams)
        {
            return dbConn.Exec(dbCmd => dbCmd.SingleFmtAsync<T>(token, sqlFormat, filterParams));
        }
        /// <summary>
        /// Singles the FMT asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="sqlFormat">The SQL format.</param>
        /// <param name="filterParams">The filter parameters.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        [Obsolete("Use db.SingleAsync(\"Age = @age\", new { age = 42 })")]
        public static Task<T> SingleFmtAsync<T>(this IDbConnection dbConn, string sqlFormat, params object[] filterParams)
        {
            return dbConn.Exec(dbCmd => dbCmd.SingleFmtAsync<T>(default(CancellationToken), sqlFormat, filterParams));
        }

        /// <summary>
        /// Returns the count of rows that match the SqlExpression lambda, E.g:
        /// <para>db.Count&lt;Person&gt;(q =&gt; q.Where(x =&gt; x.Age &lt; 50))</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.Int64&gt;.</returns>
        [Obsolete("Use db.CountAsync(db.From<T>())")]
        public static Task<long> CountAsync<T>(this IDbConnection dbConn, Func<SqlExpression<T>, SqlExpression<T>> expression, CancellationToken token = default)
        {
            return dbConn.Exec(dbCmd => dbCmd.CountAsync(expression, token));
        }

        /// <summary>
        /// Returns results with references from using an SqlExpression lambda. E.g:
        /// <para>db.LoadSelectAsync&lt;Person&gt;(q =&gt; q.Where(x =&gt; x.Age &gt; 40))</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConn">The database connection.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="include">The include.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        [Obsolete("Use db.LoadSelectAsync(db.From<T>())")]
        public static Task<List<T>> LoadSelectAsync<T>(this IDbConnection dbConn, Func<SqlExpression<T>, SqlExpression<T>> expression, string[] include = null, CancellationToken token = default)
        {
            return dbConn.Exec(dbCmd => dbCmd.LoadSelectAsync(expression, include, token));
        }
    }
}

#endif