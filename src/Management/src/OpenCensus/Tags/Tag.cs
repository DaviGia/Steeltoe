﻿// <copyright file="Tag.cs" company="OpenCensus Authors">
// Copyright 2018, OpenCensus Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenCensus.Tags
{
    using System;

    public sealed class Tag : ITag
    {
        internal Tag(ITagKey key, ITagValue value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ITagKey Key { get; }

        public ITagValue Value { get; }

        public static ITag Create(ITagKey key, ITagValue value)
        {
            return new Tag(key, value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Tag{"
                + "key=" + Key + ", "
                + "value=" + Value
                + "}";
        }

    /// <inheritdoc/>
        public override bool Equals(object o)
        {
            if (o == this)
            {
                return true;
            }

            if (o is Tag that)
            {
                return Key.Equals(that.Key)
                     && Value.Equals(that.Value);
            }

            return false;
        }

    /// <inheritdoc/>
        public override int GetHashCode()
        {
            var h = 1;
            h *= 1000003;
            h ^= Key.GetHashCode();
            h *= 1000003;
            h ^= Value.GetHashCode();
            return h;
        }
    }
}
