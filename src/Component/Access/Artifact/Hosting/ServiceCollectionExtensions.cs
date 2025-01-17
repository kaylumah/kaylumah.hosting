﻿// Copyright (c) Kaylumah, 2025. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.Ssg.Access.Artifact.Interface;
using Kaylumah.Ssg.Access.Artifact.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kaylumah.Ssg.Access.Artifact.Hosting
{
    public static partial class ServiceCollectionExtensions
    {
#pragma warning disable IDE0060
        public static IServiceCollection AddArtifactAccess(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceDescriptor loggerDescriptor = ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(NullLogger<>));
            services.TryAdd(loggerDescriptor);

            services.RegisterImplementationsAsSingleton<IStoreArtifactsStrategy>();

            services.AddProxiedService<IArtifactAccess, ArtifactAccess>();
            return services;
        }
    }
}
