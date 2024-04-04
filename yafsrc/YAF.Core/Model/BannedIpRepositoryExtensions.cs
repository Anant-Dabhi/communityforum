﻿/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2024 Ingo Herbote
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
namespace YAF.Core.Model;

using System;

using YAF.Types.Models;

/// <summary>
///     The banned IP repository extensions.
/// </summary>
public static class BannedIpRepositoryExtensions
{
    /// <summary>
    /// Save or Update Banned IP
    /// </summary>
    /// <param name="repository">
    /// The repository. 
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="mask">
    /// The mask. 
    /// </param>
    /// <param name="reason">
    /// The reason. 
    /// </param>
    /// <param name="userId">
    /// The user id. 
    /// </param>
    /// <param name="boardId">
    /// The board Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool Save(
        this IRepository<BannedIP> repository,
        int? id,
        string mask,
        string reason,
        int userId,
        int? boardId = null)
    {
        

        if (id.HasValue)
        {
            repository.UpdateOnly(
                () => new BannedIP {Mask = mask, Reason = reason, UserID = userId },
                b => b.ID == id.Value && b.BoardID == repository.BoardID);

            return true;
        }

        if (repository.Exists(b => b.BoardID == repository.BoardID && b.Mask == mask))
        {
            return false;
        }

        var newId = repository.Insert(
            new BannedIP
                {
                    BoardID = boardId ?? repository.BoardID,
                    Mask = mask,
                    Reason = reason,
                    UserID = userId,
                    Since = DateTime.Now
                });

        repository.FireNew(newId);

        return true;
    }
}