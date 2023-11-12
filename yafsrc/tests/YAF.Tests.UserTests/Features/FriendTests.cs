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

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Tests.UserTests.Features;

/// <summary>
/// The user friend tests.
/// </summary>
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FriendTests : TestBase
{
    /// <summary>
    /// Request Add Friend Test
    /// Login as Admin and add the TestUser as Friend
    /// </summary>
    [Test, Order(1)]
    public async Task AddRequestFriendTest()
    {
        await this.Base.PlaywrightFixture.GotoPageAsync(
            this.Base.TestSettings.TestForumUrl,
            async page =>
                {
                    // Log user in first!
                    Assert.IsTrue(
                        await page.LoginUserAsync(
                            this.Base.TestSettings,
                            this.Base.TestSettings.AdminUserName,
                            this.Base.TestSettings.AdminPassword),
                        "Login failed");

                    // Do actual test

                    // Go to Members Page and Find the Test User
                    await page.GotoAsync($"{this.Base.TestSettings.TestForumUrl}Members");

                    var pageSource = await page.ContentAsync();

                    Assert.IsTrue(pageSource.Contains("Filter"), "Members List View Permissions needs to be enabled");

                    await page.GetByRole(AriaRole.Button, new() { Name = " Filter" }).ClickAsync();

                    await page.Locator("//input[contains(@id,'_UserSearchName')]")
                        .FillAsync(this.Base.TestSettings.TestUserName);

                    await page.Locator("//button[contains(@formaction,'Members/Search')]").ClickAsync();

                    var userProfileLink = page.GetByText(this.Base.TestSettings.TestUserName).First;

                    Assert.IsNotNull(userProfileLink, "User Profile Not Found");

                    await userProfileLink.ClickAsync();

                    pageSource = await page.ContentAsync();

                    Assert.IsFalse(pageSource.Contains("Remove Friend"), "User is already a Friend");

                    Assert.IsTrue(
                        pageSource.Contains("Add as Friend"),
                        "My Friends function is not available for that User, or is disabled for that Forum");

                    await page.Locator("//a[contains(@href,'AddFriendRequest')]").ClickAsync();

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(pageSource.Contains("Friend request sent."));
                },
            this.BrowserType);
    }

    /// <summary>
    /// Approve the Friend request test.
    /// </summary>
    [Test, Order(2)]
    public async Task ApproveFriendRequestTest()
    {
        await this.Base.PlaywrightFixture.GotoPageAsync(
            this.Base.TestSettings.TestForumUrl,
            async page =>
                {
                    // Log user in first!
                    Assert.IsTrue(
                        await page.LoginUserAsync(
                            this.Base.TestSettings,
                            this.Base.TestSettings.TestUserName,
                            this.Base.TestSettings.TestUserPassword),
                        "Login failed");

                    // Do actual test
                    var pageSource = await page.ContentAsync();

                    if (pageSource.Contains("New Friend request"))
                    {
                        await page.Locator(".bootbox-accept").ClickAsync();
                    }
                    else
                    {
                        await page.GotoAsync($"{this.Base.TestSettings.TestForumUrl}Friends");
                    }

                    Assert.IsTrue(
                        pageSource.Contains("Friend List"),
                        "My Friends function is not available for that User, or is disabled for that Forum");

                    await page.GetByRole(AriaRole.Combobox, new() { Name = "friend mode" })
                        .SelectOptionAsync(new[] { "3" });

                    // Select the First Request
                    await page.Locator("//button[contains(@formaction,'Approve')]").First.ClickAsync();

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(pageSource.Contains("You have been added to "), "Approve Friend Failed");
                },
            this.BrowserType);
    }

    /// <summary>
    /// Deny the Newest Friend Request test.
    /// </summary>
    [Test]
    public async Task DenyFriendRequestTest()
    {
        await this.Base.PlaywrightFixture.GotoPageAsync(
            this.Base.TestSettings.TestForumUrl,
            async page =>
                {
                    // Log user in first!
                    Assert.IsTrue(
                        await page.LoginUserAsync(
                            this.Base.TestSettings,
                            this.Base.TestSettings.TestUserName,
                            this.Base.TestSettings.TestUserPassword),
                        "Login failed");

                    // Do actual test
                    var pageSource = await page.ContentAsync();

                    if (pageSource.Contains("New Friend request"))
                    {
                        await page.Locator(".bootbox-accept").ClickAsync();
                    }
                    else
                    {
                        await page.GotoAsync($"{this.Base.TestSettings.TestForumUrl}Friends");
                    }

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(
                        pageSource.Contains("Friend List"),
                        "My Friends function is not available for that User, or is disabled for that Forum");

                    await page.GetByRole(AriaRole.Combobox, new() { Name = "friend mode" })
                        .SelectOptionAsync(new[] { "3" });

                    // Select the First Request
                    await page.Locator("//button[contains(@formaction,'Deny')]").First.ClickAsync();

                    await page.GetByText("Yes").ClickAsync();

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(pageSource.Contains("Friend request denied."), "Deny Request Failed");
                },
            this.BrowserType);
    }

    /// <summary>
    /// Approve and add Friend request test.
    /// </summary>
    [Test]
    public async Task ApproveAndAddFriendRequestTest()
    {
        await this.Base.PlaywrightFixture.GotoPageAsync(
            this.Base.TestSettings.TestForumUrl,
            async page =>
                {
                    // Log user in first!
                    Assert.IsTrue(
                        await page.LoginUserAsync(
                            this.Base.TestSettings,
                            this.Base.TestSettings.TestUserName,
                            this.Base.TestSettings.TestUserPassword),
                        "Login failed");

                    // Do actual test
                    var pageSource = await page.ContentAsync();

                    if (pageSource.Contains("New Friend request"))
                    {
                        await page.Locator(".bootbox-accept").ClickAsync();
                    }
                    else
                    {
                        await page.GotoAsync($"{this.Base.TestSettings.TestForumUrl}Friends");
                    }

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(
                        pageSource.Contains("Friend List"),
                        "My Friends function is not available for that User, or is disabled for that Forum");

                    await page.GetByRole(AriaRole.Combobox, new() { Name = "friend mode" })
                        .SelectOptionAsync(new[] { "3" });

                    // Select the First Request
                    await page.Locator("//button[contains(@formaction,'ApproveAdd')]").First.ClickAsync();

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(pageSource.Contains("You and "), "Approve and Add Friend Failed");
                },
            this.BrowserType);
    }

    /// <summary>
    /// Remove a Friend test.
    /// </summary>
    [Test, Order(3)]
    public async Task RemoveFriendTest()
    {
        await this.Base.PlaywrightFixture.GotoPageAsync(
            this.Base.TestSettings.TestForumUrl,
            async page =>
                {
                    // Log user in first!
                    Assert.IsTrue(
                        await page.LoginUserAsync(
                            this.Base.TestSettings,
                            this.Base.TestSettings.TestUserName,
                            this.Base.TestSettings.TestUserPassword),
                        "Login failed");

                    // Do actual test
                    await page.GotoAsync($"{this.Base.TestSettings.TestForumUrl}Friends");

                    var pageSource = await page.ContentAsync();

                    Assert.IsTrue(
                        pageSource.Contains("Friend List"),
                        "My Friends function is not available for that User, or is disabled for that Forum");

                    // Select the Newest Friend
                    var delete = page.Locator("//button[contains(@formaction,'Remove')]");

                    Assert.IsNotNull(delete, "Currently the Test User doesn't have any Friends");

                    await delete.ClickAsync();

                    pageSource = await page.ContentAsync();

                    Assert.IsTrue(pageSource.Contains("has been removed from your Friend list."));
                },
            this.BrowserType);
    }
}