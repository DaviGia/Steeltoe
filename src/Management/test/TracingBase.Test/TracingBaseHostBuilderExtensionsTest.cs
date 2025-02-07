﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using Steeltoe.Extensions.Logging;
using Steeltoe.Management.OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using Xunit;

namespace Steeltoe.Management.Tracing.Test
{
    public class TracingBaseHostBuilderExtensionsTest : TestBase
    {
        [Fact]
        public void AddDistributedTracing_ThrowsOnNulls()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TracingBaseServiceCollectionExtensions.AddDistributedTracing(null));
            Assert.Equal("services", ex.ParamName);
        }

        [Fact]
        public void AddDistributedTracing_ConfiguresExpectedDefaults()
        {
            var hostBuilder = new HostBuilder();
            IServiceCollection services = null;
            hostBuilder.ConfigureServices(svc =>
            {
                services = svc;
                svc.AddSingleton(GetConfiguration());
                svc.AddDistributedTracing();
            });
            var host = hostBuilder.Build();
            var serviceProvider = services.BuildServiceProvider();
            ValidateServiceCollectionCommon(serviceProvider);
            ValidateServiceCollectionBase(serviceProvider);
        }
    }
}
