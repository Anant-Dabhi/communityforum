/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2019 Ingo Herbote
 * http://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Web.EventsArgs
{
    using System;

    using YAF.Types;

    /// <summary>
    /// EventArgs class for the PageTitleSet event
    /// </summary>
    public class ForumPageTitleArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumPageTitleArgs"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public ForumPageTitleArgs([NotNull] string title)
        {
            this.Title = title;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets Title.
        /// </summary>
        public string Title { get; }

        #endregion
    }
}