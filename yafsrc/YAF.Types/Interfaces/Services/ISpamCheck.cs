﻿/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2023 Ingo Herbote
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
namespace YAF.Types.Interfaces;

using System.Threading.Tasks;

/// <summary>
/// Spam Check Interface
/// </summary>
public interface ISpamCheck
{
    /// <summary>
    /// Check a Post for SPAM against the internal Spam Words
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="ipAddress">The IP address.</param>
    /// <param name="postMessage">The post message.</param>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="result">The result.</param>
    /// <returns>
    /// Returns if Post is SPAM or not
    /// </returns>
    bool CheckPostForSpam(
        string userName,
        string ipAddress,
        string postMessage,
        string emailAddress,
        out string result);

    /// <summary>
    /// Check a User (Bot) against the StopForumSpam, BotScout Service or both
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="ipAddress">The IP address.</param>
    /// <returns>
    /// Returns if Post is SPAM or not
    /// </returns>
    Task<(string Result, bool IsBot)> CheckUserForSpamBotAsync(
        string userName,
        string emailAddress,
        string ipAddress);

    /// <summary>
    /// Check Content for Spam URLs (Count URLs inside Messages)
    /// </summary>
    /// <param name="message">
    /// The message to check for URLs.
    /// </param>
    /// <param name="result">
    /// The result.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool ContainsSpamUrls(string message, out string result);
}