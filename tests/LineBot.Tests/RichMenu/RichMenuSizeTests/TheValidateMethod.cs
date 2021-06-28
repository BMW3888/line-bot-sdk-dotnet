﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuSizeTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsNotSet()
            {
                var richMenuSize = new RichMenuSize();

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    richMenuSize.Validate();
                });
            }
        }
    }
}
