﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steeltoe.Management.Endpoint.Health;
using System;

namespace Steeltoe.Management.Endpoint.SpringBootAdminClient
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register startup/shutdown interactions with Spring Boot Admin server
        /// </summary>
        /// <param name="services">Reference to the service collection</param>
        /// <returns>A reference to the service collection</returns>
        public static IServiceCollection AddSpringBootAdminClient(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<ManagementEndpointOptions>();
            services.TryAddSingleton<HealthEndpointOptions>();

            services.AddSingleton<SpringBootAdminClientOptions>();
            services.AddHttpClient<SpringBootAdminClientHostedService>().ConfigureHttpClient((services, client) => { });
            services.AddHostedService<SpringBootAdminClientHostedService>();
            return services;
        }
    }
}
