﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ConfirmTemplateTests
    {
        [TestClass]
        public class TheOkActionProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The ok action cannot be null.", () =>
                {
                    template.OkAction = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNotNull()
            {
                var action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") };

                var confirmTemplate = new ConfirmTemplate
                {
                    OkAction = action
                };

                Assert.AreEqual(action, confirmTemplate.OkAction);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsInvalidType()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    template.OkAction = new TestAction();
                });
            }
        }
    }
}
