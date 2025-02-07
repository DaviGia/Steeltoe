// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Steeltoe.Common.Logging
{
    internal static class BootstrapLoggerFactory
    {
        public static IBoostrapLoggerFactory Instance { get; } = new UpgradableBootstrapLoggerFactory();
    }
}