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
using System.Collections.Generic;

namespace Line.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestImagemapMessage : IImagemapMessage
    {
        public TestImagemapMessage()
        {
            Actions = new IImagemapAction[]
            {
                    new TestImagemapUriAction(),
                    new TestImagemapMessageAction()
            };
        }

        public Uri BaseUrl => new Uri("https://foo.url");

        public string AlternativeText => "Alternative";

        public ImagemapSize BaseSize => new ImagemapSize(1040, 520);

        public IEnumerable<IImagemapAction> Actions { get; set; }
    }
}
