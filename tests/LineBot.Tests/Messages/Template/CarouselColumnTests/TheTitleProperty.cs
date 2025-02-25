﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TheThumbnailUrlProperty
    {
        [TestClass]
        public class TheTitleProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn
                {
                    Title = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan40Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 40 characters.", () =>
                {
                    column.Title = new string('x', 41);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs40Chars()
            {
                var value = new string('x', 40);

                var column = new CarouselColumn()
                {
                    Title = value
                };

                Assert.AreEqual(value, column.Title);
            }
        }
    }
}
