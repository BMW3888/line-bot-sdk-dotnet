﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheCreateRichMenuMethod
        {
            [TestMethod]
            public async Task ShouldThrowExceptionWhenRichMenuIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () => { await bot.CreateRichMenu(null); });
            }

            [TestMethod]
            public async Task ShouldThrowExceptionWhenRichMenuIsInvalid()
            {
                var richMenu = new RichMenu();

                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsAsync<InvalidOperationException>("The areas cannot be null.", async () => { await bot.CreateRichMenu(richMenu); });
            }

            [TestMethod]
            public async Task ShouldCreateRichMenu()
            {
                var richMenu = new RichMenu()
                {
                    Size = new RichMenuSize(1686),

                    Selected = false,

                    Name = "testName",

                    ChatBarText = "testChatBarTxt",

                    Areas = new[]
                    {
                        new RichMenuArea()
                        {
                            Action = new UriAction { Label = "testLabel", Url = new Uri("http://www.google.com") },
                            Bounds = new RichMenuBounds
                            {
                                Width = 110,
                                Height = 120,
                                X = 11,
                                Y = 12
                            }
                        },
                        new RichMenuArea
                        {
                            Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                            Bounds = new RichMenuBounds
                            {
                                Width = 210,
                                Height = 220,
                                X = 21,
                                Y = 22
                            }
                        }
                    }
                };

                var richMenuIdJson = @"{""richMenuId"": ""richmenu-801b2cd26b2f13587329ed501d279d27""}";
                var httpClient = TestHttpClient.ThatReturnsData(Encoding.ASCII.GetBytes(richMenuIdJson));

                var bot = TestConfiguration.CreateBot(httpClient);
                var result = await bot.CreateRichMenu(richMenu);

                Assert.AreEqual("/richmenu", httpClient.RequestPath);

                string postedData =
                    @"{""areas"":[{""action"":{""type"":""uri"",""label"":""testLabel"",""uri"":""http://www.google.com""},""bounds"":{""x"":11,""y"":12,""width"":110,""height"":120}},{""action"":{""type"":""uri"",""label"":""testLabel2"",""uri"":""http://www.bing.com""},""bounds"":{""x"":21,""y"":22,""width"":210,""height"":220}}],""chatBarText"":""testChatBarTxt"",""name"":""testName"",""selected"":false,""size"":{""width"":2500,""height"":1686}}";
                Assert.AreEqual(postedData, httpClient.PostedData);

                Assert.AreEqual(result, "richmenu-801b2cd26b2f13587329ed501d279d27");
            }
        }
    }
}