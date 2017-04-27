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
    public class MessageTests
    {
        private const string AudioJson = "Events/Messages/Audio.json";
        private const string ImageJson = "Events/Messages/Image.json";
        private const string InvalidJson = "Events/Invalid.json";
        private const string InvalidMesssageJson = "Events/Messages/InvalidMessage.json";
        private const string LocationJson = "Events/Messages/Location.json";
        private const string MessageEventWithoutMessageJson = "Events/Messages/MessageEventWithoutMessage.json";
        private const string StickerJson = "Events/Messages/Sticker.json";
        private const string TextJson = "Events/Messages/Text.json";
        private const string VideoJson = "Events/Messages/Video.json";

        [TestMethod]
        [DeploymentItem(MessageEventWithoutMessageJson)]
        public async Task GetEvents_RequestWithoutMessage_MessageIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(MessageEventWithoutMessageJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Message, lineEvent.EventType);
            Assert.IsNull(lineEvent.Message);
        }

        [TestMethod]
        [DeploymentItem(InvalidMesssageJson)]
        public async Task GetEvents_InvalidMessageType_MessageTypeIsUnknown()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(InvalidMesssageJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual(MessageType.Unknown, lineEvent.Message.MessageType);
        }

        [TestMethod]
        [DeploymentItem(InvalidJson)]
        public async Task GetEvents_InvalidRequest_MessageIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(InvalidJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.Message);
        }

        [TestMethod]
        [DeploymentItem(AudioJson)]
        public async Task Group_MessageTypeIsAudio_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(AudioJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Audio, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.IsNull(lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(ImageJson)]
        public async Task Group_MessageTypeIsImage_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(ImageJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Image, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.IsNull(lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(LocationJson)]
        public async Task Group_MessageTypeIsLocation_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(LocationJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.AreEqual(MessageType.Location, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Text);
            Assert.IsNull(lineEvent.Message.Sticker);

            ILocation location = lineEvent.Message.Location;
            Assert.IsNotNull(location);
            Assert.AreEqual("〒150-0002 東京都渋谷区渋谷２丁目２１−１", location.Address);
            Assert.AreEqual(35.65910807942215m, location.Latitude);
            Assert.AreEqual(139.70372892916203m, location.Longitude);
            Assert.AreEqual("my location", location.Title);
        }

        [TestMethod]
        [DeploymentItem(StickerJson)]
        public async Task Group_MessageTypeIsSticker_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(StickerJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Sticker, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Text);

            ISticker sticker = lineEvent.Message.Sticker;
            Assert.IsNotNull(sticker);
            Assert.AreEqual("1", sticker.PackageId);
            Assert.AreEqual("2", sticker.StickerId);
        }

        [TestMethod]
        [DeploymentItem(TextJson)]
        public async Task Group_MessageTypeIsText_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(TextJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Text, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.AreEqual("Hello, world", lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(VideoJson)]
        public async Task Group_MessageTypeIsVideo_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(VideoJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Video, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.IsNull(lineEvent.Message.Text);
        }
    }
}
