﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steeltoe.Common.Contexts;
using Steeltoe.Common.Lifecycle;
using Steeltoe.Messaging.Rabbit.Config;
using Steeltoe.Messaging.Rabbit.Connection;
using Steeltoe.Messaging.Rabbit.Core;
using Steeltoe.Messaging.Rabbit.Host;
using Steeltoe.Messaging.Rabbit.Listener;
using Steeltoe.Messaging.Rabbit.Support.Converter;
using System;
using System.Linq;

namespace Steeltoe.Messaging.Rabbit.Extensions
{
    public static class RabbitServicesExtensions
    {
        public static RabbitTemplate GetRabbitTempate(this IServiceProvider provider)
        {
            return provider.GetServices<RabbitTemplate>().SingleOrDefault((t) => t.Name == RabbitTemplate.DEFAULT_RABBIT_TEMPLATE_SERVICE_NAME);
        }

        public static IAmqpAdmin GetRabbitAdmin(this IServiceProvider provider)
        {
            return provider.GetServices<IAmqpAdmin>().SingleOrDefault((t) => t.Name == RabbitAdmin.DEFAULT_RABBIT_ADMIN_SERVICE_NAME);
        }

        public static IServiceCollection AddRabbitTemplate(this IServiceCollection services)
        {
            services.AddSingleton<RabbitTemplate>();
            services.AddSingleton<IRabbitOperations>((p) =>
            {
                return p.GetServices<RabbitTemplate>().SingleOrDefault((t) => t.Name == RabbitTemplate.DEFAULT_RABBIT_TEMPLATE_SERVICE_NAME);
            });
            return services;
        }

        public static IServiceCollection AddRabbitTemplate(this IServiceCollection services, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            services.AddSingleton<RabbitTemplate>((p) =>
            {
                var template = ActivatorUtilities.CreateInstance<RabbitTemplate>(p);
                template.Name = name;
                return template;
            });

            services.AddSingleton<IRabbitOperations>((p) =>
            {
                return p.GetServices<RabbitTemplate>().SingleOrDefault((t) => t.Name == name);
            });
            return services;
        }

        public static IServiceCollection AddRabbitAdmin(this IServiceCollection services)
        {
            services.AddSingleton<IAmqpAdmin, RabbitAdmin>();
            return services;
        }

        public static IServiceCollection AddRabbitQueue(this IServiceCollection services, Queue queue)
        {
            services.AddSingleton(typeof(Queue), queue);
            return services;
        }

        public static IServiceCollection AddRabbitExchange(this IServiceCollection services, IExchange exchange)
        {
            services.AddSingleton(typeof(IExchange), exchange);
            return services;
        }

        public static IServiceCollection AddRabbitBinding(this IServiceCollection services, Binding binding)
        {
            services.AddSingleton(typeof(Binding), binding);
            return services;
        }

        public static IServiceCollection AddRabbitServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHostedService<RabbitHostService>();
            services.TryAddSingleton<ILifecycleProcessor, DefaultLifecycleProcessor>();
            services.TryAddSingleton<IApplicationContext, GenericApplicationContext>();

            services.TryAddSingleton<IConnectionFactory, CachingConnectionFactory>();

            // services.TryAddSingleton<IMessageConverter, SimpleMessageConverter>();
            services.TryAddSingleton<IMessageConverter, JsonMessageConverter>();
            services.TryAddSingleton<DirectRabbitListenerContainerFactory>();
            services.AddSingleton<IRabbitListenerContainerFactory>((p) =>
            {
                var factory = p.GetRequiredService<DirectRabbitListenerContainerFactory>();
                factory.Name = IRabbitListenerContainerFactory.DEFAULT_RABBIT_LISTENER_CONTAINER_FACTORY_SERVICE_NAME;
                return factory;
            });

            services.TryAddSingleton<RabbitListenerAttributeProcessor>();

            services.TryAddSingleton<RabbitListenerEndpointRegistry>(); // Requires call to initialize
            services.AddSingleton<ILifecycle>((p) => p.GetRequiredService<RabbitListenerEndpointRegistry>());

            return services;
        }
    }
}
