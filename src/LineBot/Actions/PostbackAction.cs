﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a postback action.
    /// </summary>
    public sealed class PostbackAction : IAction
    {
        private string? _label;
        private string? _data;
        private string? _text;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        ActionType IAction.Type
            => ActionType.Postback;

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 20 characters.</para>
        /// </summary>
        [JsonProperty("label")]
        public string? Label
        {
            get => _label;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The label cannot be null or whitespace.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The label cannot be longer than 20 characters.");

                _label = value;
            }
        }

        /// <summary>
        /// Gets or sets the string returned via webhook in the postback.data property of the <see cref="IPostback"/> event.
        /// <para>Max: 300 characters.</para>
        /// </summary>
        [JsonProperty("data")]
        public string? Data
        {
            get => _data;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The data cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The data cannot be longer than 300 characters.");

                _data = value;
            }
        }

        /// <summary>
        /// Gets or sets the text sent when the action is performed.
        /// </summary>
        [JsonProperty("text")]
        public string? Text
        {
            get => _text;
            set
            {
                if (value is not null && value.Length > 300)
                    throw new InvalidOperationException("The text cannot be longer than 300 characters.");

                _text = value;
            }
        }

        void IAction.Validate()
        {
            if (_label is null)
                throw new InvalidOperationException("The label cannot be null.");

            if (_data is null)
                throw new InvalidOperationException("The data cannot be null.");
        }
    }
}
