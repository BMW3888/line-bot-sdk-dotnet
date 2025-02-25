﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class StreamExtensionsTests
    {
        [TestClass]
        public class ToArrayMethod
        {
            private static readonly byte[] Utf8Preamable = Encoding.UTF8.GetPreamble();

            [TestMethod]
            public async Task ShouldReturnNullWhenStreamIsEmpty()
            {
                using (var stream = new TestStream())
                {
                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNull(bytes);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenStreamStartsWithPreamable()
            {
                using (var stream = new TestStream())
                {
                    stream.Write(Utf8Preamable, 0, 2);
                    stream.Position = 0;

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(2, bytes.Length);
                    Assert.AreEqual(Utf8Preamable[0], bytes[0]);
                    Assert.AreEqual(Utf8Preamable[1], bytes[1]);
                }
            }

            [TestMethod]
            public async Task ShouldReturnEmptyArrayWhenStreamOnlyContainsPreamable()
            {
                using (var stream = new TestStream())
                {
                    stream.Write(Utf8Preamable, 0, Utf8Preamable.Length);
                    stream.Position = 0;

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(0, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenStreamContainsData()
            {
                using (var stream = new TestStream())
                {
                    byte[] data = new byte[4] { 68, 73, 82, 75 };
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(4, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenStreamContains9000Bytes()
            {
                using (var stream = new TestStream())
                {
                    byte[] data = Enumerable.Repeat((byte)68, 9000).ToArray();
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(9000, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnNullWhenNonSeekableStreamIsEmpty()
            {
                using (var stream = new TestStream())
                {
                    stream.DisableSeeking();

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNull(bytes);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenNonSeekableStreamStartsWithPreamable()
            {
                using (var stream = new TestStream())
                {
                    stream.Write(Utf8Preamable, 0, 2);
                    stream.Position = 0;

                    stream.DisableSeeking();

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(2, bytes.Length);
                    Assert.AreEqual(Utf8Preamable[0], bytes[0]);
                    Assert.AreEqual(Utf8Preamable[1], bytes[1]);
                }
            }

            [TestMethod]
            public async Task ShouldReturnEmptyArrayWhenNonSeekableStreamOnlyContainsPreamable()
            {
                using (var stream = new TestStream())
                {
                    stream.Write(Utf8Preamable, 0, Utf8Preamable.Length);
                    stream.Position = 0;

                    stream.DisableSeeking();

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(0, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenNonSeekableStreamContainsData()
            {
                using (var stream = new TestStream())
                {
                    byte[] data = new byte[4] { 68, 73, 82, 75 };
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;

                    stream.DisableSeeking();

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(4, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenNonSeekableStreamContains9000Bytes()
            {
                using (var stream = new TestStream())
                {
                    byte[] data = Enumerable.Repeat((byte)68, 9000).ToArray();
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;

                    stream.DisableSeeking();

                    byte[] bytes = await stream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(9000, bytes.Length);
                }
            }

            private sealed class TestStream : MemoryStream
            {
                private bool _canSeek = true;

                public override bool CanSeek => _canSeek;

                public void DisableSeeking() => _canSeek = false;

                protected override void Dispose(bool disposing)
                {
                    base.Dispose(disposing);
                }
            }
        }
    }
}
