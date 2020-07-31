﻿// <copyright file="LinkList.cs" company="OpenCensus Authors">
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

namespace OpenCensus.Trace.Export
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class LinkList : ILinks
    {
        internal LinkList(IList<ILink> links, int droppedLinksCount)
        {
            Links = links ?? throw new ArgumentNullException("Null links");
            DroppedLinksCount = droppedLinksCount;
        }

        public int DroppedLinksCount { get; }

        public IEnumerable<ILink> Links { get; }

        public static LinkList Create(IList<ILink> links, int droppedLinksCount)
        {
            if (links == null)
            {
                throw new ArgumentNullException(nameof(links));
            }

            var copy = new List<ILink>(links);

            return new LinkList(copy.AsReadOnly(), droppedLinksCount);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Links{"
                + "links=" + Links + ", "
                + "droppedLinksCount=" + DroppedLinksCount
                + "}";
        }

        /// <inheritdoc/>
        public override bool Equals(object o)
        {
            if (o == this)
            {
                return true;
            }

            if (o is LinkList that)
            {
                return Links.SequenceEqual(that.Links)
                     && (DroppedLinksCount == that.DroppedLinksCount);
            }

            return false;
        }

    /// <inheritdoc/>
        public override int GetHashCode()
        {
            var h = 1;
            h *= 1000003;
            h ^= Links.GetHashCode();
            h *= 1000003;
            h ^= DroppedLinksCount;
            return h;
        }
    }
}
