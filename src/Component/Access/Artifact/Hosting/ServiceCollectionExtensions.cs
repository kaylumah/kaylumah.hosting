﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.Ssg.Access.Artifact.Interface;
using Kaylumah.Ssg.Access.Artifact.Service;
using Kaylumah.Ssg.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kaylumah.Ssg.Access.Artifact.Hosting;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddArtifactAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(NullLogger<>)));
        services.AddSingleton<IStoreArtifactsStrategy, FileSystemStoreArtifactsStrategy>();
        services.AddSingleton<IArtifactAccess, ArtifactAccess>();
        return services;
    }
}
