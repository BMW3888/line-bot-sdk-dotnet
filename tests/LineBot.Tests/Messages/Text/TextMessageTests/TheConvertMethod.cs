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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TextMessageTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsTextMessage()
            {
                var textMessage = new TextMessage()
                {
                    Text = "Test"
                };

                var message = TextMessage.Convert(textMessage);

                Assert.AreEqual(textMessage, message);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageTextIsNull()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    TextMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomITextMessageToTextMessage()
            {
                var textMessage = new TestTextMessage();

                var message = TextMessage.Convert(textMessage);

                Assert.AreNotEqual(textMessage, message);
                Assert.AreEqual("TestTextMessage", message.Text);
            }
        }
    }
}
