﻿using Microsoft.Extensions.DependencyInjection;
using SolidHttp.Abstractions;
using SolidHttp.Json;
using System;

namespace SolidHttp
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add SolidHttp to the service collection
        /// </summary>
        /// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddSolidHttp<TFactory>(this IServiceCollection services)
            where TFactory : class, IHttpClientFactory
        {
            var core = services
                .AddSolidHttpCore<TFactory>()
                .AddJson();
            return new SolidHttpBuilder(core);
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddSolidHttp(this IServiceCollection services)
        {
            var core = services
                .AddSolidHttpCore()
                .AddJson();
            return new SolidHttpBuilder(core);
        }

        /// <summary>
        /// Add SolidHttp to the service collection
        /// </summary>
        /// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddSolidHttp<TFactory>(this IServiceCollection services, Action<ISolidHttpOptions> configure)
            where TFactory : class, IHttpClientFactory
        {
            var core = services
                .AddSolidHttpCore<TFactory>(configure)
                .AddJson();
            return new SolidHttpBuilder(core);
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddSolidHttp(this IServiceCollection services, Action<ISolidHttpOptions> configure)
        {
            var core = services
                .AddSolidHttpCore(configure)
                .AddJson();
            return new SolidHttpBuilder(core);
        }
    }
}
