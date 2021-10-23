﻿// ***********************************************************************
// <copyright file="DefaultScripts.Array.cs" company="ServiceStack, Inc.">
//     Copyright (c) ServiceStack, Inc. All Rights Reserved.
// </copyright>
// <summary>Fork for YetAnotherForum.NET, Licensed under the Apache License, Version 2.0</summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ServiceStack.Script
{
    /// <summary>
    /// Class DefaultScripts.
    /// Implements the <see cref="ServiceStack.Script.ScriptMethods" />
    /// Implements the <see cref="ServiceStack.Script.IConfigureScriptContext" />
    /// </summary>
    /// <seealso cref="ServiceStack.Script.ScriptMethods" />
    /// <seealso cref="ServiceStack.Script.IConfigureScriptContext" />
    public partial class DefaultScripts
    {
        /// <summary>
        /// Pushes the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        public int push(IList list, object item)
        {
            list.Add(item);
            return list.Count;
        }

        /// <summary>
        /// Pops the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>System.Object.</returns>
        public object pop(IList list)
        {
            if (list.Count > 0)
            {
                var ret = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                return ret;
            }
            return null;
        }

        /// <summary>
        /// Shifts the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>System.Object.</returns>
        public object shift(IList list) => splice(list, 0);

        /// <summary>
        /// Unshifts the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <returns>System.Object.</returns>
        public object unshift(IList list, object item)
        {
            if (item is IList addItems)
            {
                for (var i = addItems.Count - 1; i >= 0; i--)
                {
                    list.Insert(0, addItems[i]);
                }
            }
            else
            {
                list.Insert(0, item);
            }
            return list.Count;
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public int indexOf(object target, object item)
        {
            if (target is string s)
                return item is char c
                    ? s.IndexOf(c)
                    : item is string str
                        ? s.IndexOf(str, (StringComparison)Context.Args[ScriptConstants.DefaultStringComparison])
                        : throw new NotSupportedException($"{item.GetType().Name} not supported in indexOf");
            if (target is IList list)
                return list.IndexOf(item);

            throw new NotSupportedException($"{target.GetType().Name} not supported in indexOf");
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="item">The item.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public int indexOf(object target, object item, int startIndex)
        {
            if (target is string s)
                return item is char c
                    ? s.IndexOf(c, startIndex)
                    : item is string str
                        ? s.IndexOf(str, startIndex, (StringComparison)Context.Args[ScriptConstants.DefaultStringComparison])
                        : throw new NotSupportedException($"{item.GetType().Name} not supported in indexOf");
            if (target is List<object> list)
                return list.IndexOf(item, startIndex);

            throw new NotSupportedException($"{target.GetType().Name} not supported in indexOf");
        }

        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public int lastIndexOf(object target, object item)
        {
            if (target is string s)
                return item is char c
                    ? s.LastIndexOf(c)
                    : item is string str
                        ? s.LastIndexOf(str, (StringComparison)Context.Args[ScriptConstants.DefaultStringComparison])
                        : throw new NotSupportedException($"{item.GetType().Name} not supported in indexOf");
            if (target is List<object> list)
                return list.LastIndexOf(item);
            if (target is IList iList)
                return iList.Cast<object>().ToList().LastIndexOf(item);

            throw new NotSupportedException($"{target.GetType().Name} not supported in indexOf");
        }

        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="item">The item.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public int lastIndexOf(object target, object item, int startIndex)
        {
            if (target is string s)
                return item is char c
                    ? s.LastIndexOf(c, startIndex)
                    : item is string str
                        ? s.LastIndexOf(str, startIndex, (StringComparison)Context.Args[ScriptConstants.DefaultStringComparison])
                        : throw new NotSupportedException($"{item.GetType().Name} not supported in indexOf");
            if (target is List<object> list)
                return list.LastIndexOf(item, startIndex);

            throw new NotSupportedException($"{target.GetType().Name} not supported in indexOf");
        }

        /// <summary>
        /// Splices the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="removeAt">The remove at.</param>
        /// <returns>System.Object.</returns>
        public object splice(IList list, int removeAt)
        {
            if (list.Count > 0)
            {
                var ret = list[removeAt];
                list.RemoveAt(removeAt);
                return ret;
            }
            return null;
        }

        /// <summary>
        /// Splices the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="removeAt">The remove at.</param>
        /// <param name="deleteCount">The delete count.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> splice(IList list, int removeAt, int deleteCount) =>
            splice(list, removeAt, deleteCount, null);

        /// <summary>
        /// Splices the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="removeAt">The remove at.</param>
        /// <param name="deleteCount">The delete count.</param>
        /// <param name="insertItems">The insert items.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> splice(IList list, int removeAt, int deleteCount, List<object> insertItems)
        {
            if (list.Count > 0)
            {
                var ret = new List<object>();
                for (var i = 0; i < deleteCount; i++)
                {
                    ret.Add(list[removeAt]);
                    list.RemoveAt(removeAt);
                }
                if (insertItems != null)
                {
                    foreach (var item in insertItems.AsEnumerable().Reverse())
                    {
                        list.Insert(removeAt, item);
                    }
                }
                return ret;
            }
            return new List<object>();
        }

        /// <summary>
        /// Slices the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> slice(IList list) => list.Map(x => x);

        /// <summary>
        /// Slices the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="begin">The begin.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> slice(IList list, int begin) => list.Map(x => x).Skip(begin).ToList();

        /// <summary>
        /// Slices the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="end">The end.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> slice(IList list, int begin, int end) => list.Map(x => x).Skip(begin).Take(end - begin).ToList();

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="target">The target.</param>
        /// <param name="arrowExpr">The arrow expr.</param>
        /// <returns>IgnoreResult.</returns>
        /// <exception cref="System.NotSupportedException">Dictionary.forEach requires 2 lambda params</exception>
        /// <exception cref="System.NotSupportedException">Can only use forEach on Lists or Dictionaries</exception>
        public IgnoreResult forEach(ScriptScopeContext scope, object target, JsArrowFunctionExpression arrowExpr)
        {
            var token = arrowExpr.Body;

            scope = scope.Clone();
            if (target is IList list)
            {
                var itemBinding = arrowExpr.Params[0].Name;
                var indexBinding = arrowExpr.Params.Length > 1 ? arrowExpr.Params[1].Name : ScriptConstants.Index;
                var arrayBinding = arrowExpr.Params.Length > 2 ? arrowExpr.Params[2].Name : null;

                for (var i = 0; i < list.Count; i++)
                {
                    scope.ScopedParams[indexBinding] = i;
                    if (arrayBinding != null)
                        scope.ScopedParams[arrayBinding] = list;

                    scope = scope.AddItemToScope(itemBinding, list[i]);
                    token.Evaluate(scope);
                }
            }
            else if (target is IDictionary d)
            {
                if (arrowExpr.Params.Length != 2)
                    throw new NotSupportedException("Dictionary.forEach requires 2 lambda params");

                var keyBinding = arrowExpr.Params[0].Name;
                var valueBinding = arrowExpr.Params[1].Name;

                foreach (var key in d.Keys)
                {
                    scope.ScopedParams[keyBinding] = key;
                    scope.ScopedParams[valueBinding] = d[key];
                    token.Evaluate(scope);
                }
            }
            else throw new NotSupportedException("Can only use forEach on Lists or Dictionaries");

            return IgnoreResult.Value;
        }

        /// <summary>
        /// Everies the specified scope.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool every(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression) =>
            all(scope, list, expression, null);

        /// <summary>
        /// Somes the specified scope.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool some(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression) =>
            any(scope, list, expression, null);

        /// <summary>
        /// Finds the specified scope.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>System.Object.</returns>
        public object find(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression) =>
            first(scope, list, expression, null);

        /// <summary>
        /// Finds the index.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>System.Int32.</returns>
        public int findIndex(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression)
        {
            var items = list.AssertEnumerable(nameof(findIndex));
            var itemBinding = expression.Params[0].Name;
            var expr = expression.Body;

            var i = 0;
            foreach (var item in items)
            {
                scope.AddItemToScope(itemBinding, item, i);
                var result = expr.EvaluateToBool(scope);
                if (result)
                    return i;
                i++;
            }

            return -1;
        }

        /// <summary>
        /// Filters the specified scope.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> filter(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression) =>
            where(scope, list, expression, null).ToList();

        /// <summary>
        /// Flats the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> flat(IList list) => flatten(list, 1);
        /// <summary>
        /// Flats the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="depth">The depth.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> flat(IList list, int depth) => flatten(list, depth);

        /// <summary>
        /// Flats the map.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> flatMap(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression) =>
            flat((IList)map(scope, list, expression, null));

        /// <summary>
        /// Flats the map.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="list">The list.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="depth">The depth.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> flatMap(ScriptScopeContext scope, IList list, JsArrowFunctionExpression expression, int depth) =>
            flat((IList)map(scope, list, expression, null), depth);

        /// <summary>
        /// Includeses the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool includes(IList list, object item) =>
            includes(list, item, 0);

        /// <summary>
        /// Includeses the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <param name="fromIndex">From index.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool includes(IList list, object item, int fromIndex)
        {
            for (var i = fromIndex; i < list.Count; i++)
            {
                if (list[i].Equals(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Sorts the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public List<object> sort(List<object> list)
        {
            list.Sort();
            return list;
        }
    }
}