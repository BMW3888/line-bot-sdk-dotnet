﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheGetMessageContentMethod
        {
            [TestMethod]
            public async Task ThrowsExceptionWhenMessageIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("message", async () =>
                {
                    await bot.GetMessageContent((IMessage)null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenMessageIdIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messageId", async () =>
                {
                    await bot.GetMessageContent((string)null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenMessageIsEmpty()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messageId", async () =>
                {
                    await bot.GetMessageContent(string.Empty);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.GetMessageContent("test");
                });
            }

            [TestMethod]
            public async Task ReturnsNullWhenResponseIsEmpty()
            {
                TestHttpClient httpClient = TestHttpClient.Create();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                byte[] data = await bot.GetMessageContent("test");

                Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
                Assert.AreEqual("/message/test/content", httpClient.RequestPath);

                Assert.IsNull(data);
            }

            [TestMethod]
            public async Task ReturnsDataWhenWithCorrectMessageId()
            {
                byte[] input = new byte[12] { 68, 105, 114, 107, 32, 76, 101, 109, 115, 116, 114, 97 };

                TestHttpClient httpClient = TestHttpClient.ThatReturnsData(input);

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                byte[] data = await bot.GetMessageContent("test");

                Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
                Assert.AreEqual("/message/test/content", httpClient.RequestPath);

                Assert.IsNotNull(data);
                CollectionAssert.AreEqual(data, input);
            }

            [TestMethod]
            public async Task ReturnsDataWithCorrectMessage()
            {
                byte[] input = new byte[12] { 68, 105, 114, 107, 32, 76, 101, 109, 115, 116, 114, 97 };

                TestHttpClient httpClient = TestHttpClient.ThatReturnsData(input);

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                byte[] data = await bot.GetMessageContent(new TestMessage(MessageType.Image));

                Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
                Assert.AreEqual("/message/testMessage/content", httpClient.RequestPath);

                Assert.IsNotNull(data);
                CollectionAssert.AreEqual(data, input);
            }
        }
    }
}
