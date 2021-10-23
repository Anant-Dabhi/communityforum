﻿/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2021 Ingo Herbote
 * https://www.yetanotherforum.net/
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * https://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
namespace YAF.Data.MySql
{
    using System;

    using ServiceStack.OrmLite;

    using YAF.Configuration;
    using YAF.Core.Events;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces.Events;

    /// <summary>
    /// Sets the MySQL dialect event.
    /// </summary>
    [ExportService(ServiceLifetimeScope.InstancePerDependency, new[] { typeof(IHandleEvent<InitDatabaseProviderEvent>) })]
    public class SetMySqlDialectEvent : IHandleEvent<InitDatabaseProviderEvent>
    {
        #region Public Properties

        /// <summary>
        ///     Gets the order.
        /// </summary>
        public int Order => 1000;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        public void Handle(InitDatabaseProviderEvent @event)
        {
            if (@event.ProviderName != MySqlDbAccess.ProviderTypeName)
            {
                return;
            }

            // set the OrmLite dialect provider...
            OrmLiteConfig.DialectProvider = YafMySqlDialectProvider.Instance;

            OrmLiteConfig.DialectProvider.GetDateTimeConverter().DateStyle = DateTimeKind.Utc;
            OrmLiteConfig.DialectProvider.GetStringConverter().UseUnicode = true;
            OrmLiteConfig.CommandTimeout = Config.SqlCommandTimeout;
        }

        #endregion
    }
}