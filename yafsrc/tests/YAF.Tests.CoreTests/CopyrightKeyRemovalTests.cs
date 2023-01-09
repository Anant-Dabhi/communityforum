/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
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

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Tests.CoreTests;

using System;

using YAF.Types.Constants;
using YAF.Types.Extensions;

/// <summary>
/// The copyright key removal tests.
/// </summary>
[TestFixture]
public class CopyrightKeyRemovalTests
{
    /// <summary>
    /// Verify Copyright Removal Key Test.
    /// </summary>
    [Test]
    [Description("Verify Copyright Removal Key Test")]
    public void CopyrightRemoval_Match_Test()
    {
        const string CheckUrl = "yetanotherforum.net";

        var url = new Uri($"https://{CheckUrl}");

        var dnsSafeHost = url.Host.ToLowerInvariant();

        if (dnsSafeHost.LastIndexOf('.') != dnsSafeHost.IndexOf('.'))
        {
            dnsSafeHost = dnsSafeHost.Remove(
                0,
                dnsSafeHost.IndexOf('.') + 1);
        }

        var currentDomainHash = HashHelper.Hash(
            dnsSafeHost,
            HashAlgorithmType.SHA1,
            this.GetType().GetSigningKey(),
            false);

        Assert.AreEqual(currentDomainHash, "QLZDULM1U21NB2SISMH4TSRRBA0=");
    }
}