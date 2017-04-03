﻿// <copyright file="HttpResponseMessageExtensionsTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class HttpResponseMessageExtensionsTests
    {
        private const string LineErrorJson = "Extensions\\LineError.json";

        [TestMethod]
        public async Task CheckResult_IsSuccess_ThrowsNoException()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            await responseMessage.CheckResult();
        }

        [TestMethod]
        public async Task CheckResult_ResponseIsNull_ThrowsException()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            LineBotException exception = await ExceptionAssert.ThrowsAsync<LineBotException>("Unknown error", async () =>
            {
                await responseMessage.CheckResult();
            });

            Assert.AreEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.IsNotNull(exception.Details);
            Assert.AreEqual(0, exception.Details.Count());
        }

        [TestMethod]
        public async Task CheckResult_ResponseIsEmpyString_ThrowsException()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(string.Empty)
            };

            LineBotException exception = await ExceptionAssert.ThrowsAsync<LineBotException>("Unknown error", async () =>
            {
                await responseMessage.CheckResult();
            });
        }

        [TestMethod]
        public async Task CheckResult_ResponseIsEmpyErrorObject_ThrowsException()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("{}")
            };

            LineBotException exception = await ExceptionAssert.ThrowsAsync<LineBotException>("Unknown error", async () =>
            {
                await responseMessage.CheckResult();
            });
        }

        [TestMethod]
        [DeploymentItem(LineErrorJson)]
        public async Task CheckResult_ResponseIsError_ThrowsException()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(File.ReadAllText(LineErrorJson))
            };

            LineBotException exception = await ExceptionAssert.ThrowsAsync<LineBotException>("The request body has 2 error(s)", async () =>
            {
                await responseMessage.CheckResult();
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.IsNotNull(exception.Details);

            List<ILineErrorDetails> details = exception.Details.ToList();
            Assert.AreEqual("May not be empty", details[0].Message);
            Assert.AreEqual("messages[0].text", details[0].Property);
            Assert.AreEqual("Must be one of the following values: [text, image, video, audio, location, sticker, template, imagemap]", details[1].Message);
            Assert.AreEqual("messages[1].type", details[1].Property);
        }
    }
}
