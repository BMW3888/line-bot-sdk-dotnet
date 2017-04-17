﻿// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class FollowEventTests
    {
        private const string FollowEventJson = "Events\\Follow\\FollowEvent.json";
        private const string InvalidJson = "Events\\Invalid.json";

        [TestMethod]
        [DeploymentItem(FollowEventJson)]
        public async Task GetEvents_ValidRequest_ReturnsFollowEvent()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(FollowEventJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual(LineEventType.Follow, lineEvent.EventType);

            IFollowEvent followEvent = lineEvent.FollowEvent;
            Assert.IsNotNull(followEvent);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", followEvent.ReplyToken);
        }

        [TestMethod]
        [DeploymentItem(InvalidJson)]
        public async Task GetEvents_InvalidRequest_FollowEventReturnsNull()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(InvalidJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.FollowEvent);
        }
    }
}
