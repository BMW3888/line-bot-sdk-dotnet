﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselColumnTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenImageUrlIsNull()
            {
                var column = new ImageCarouselColumn()
                {
                    Action = new PostbackAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The image url cannot be null.", () =>
                {
                    column.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsNull()
            {
                var column = new ImageCarouselColumn()
                {
                    ImageUrl = new Uri("https://foo.bar"),
                };

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    column.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var column = new ImageCarouselColumn()
                {
                    ImageUrl = new Uri("https://foo.bar"),
                    Action = new PostbackAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    column.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var column = new ImageCarouselColumn()
                {
                    ImageUrl = new Uri("https://foo.bar"),
                    Action = new PostbackAction() { Data = "Foo", Label = "Bar" }
                };

                column.Validate();
            }
        }
    }
}
