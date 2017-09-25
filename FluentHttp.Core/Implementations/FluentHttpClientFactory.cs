﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public class FluentHttpClientFactory : IFluentHttpClientFactory
    {
        public const string ABSOLUTE_CLIENT_KEY = "__absolute";

        private IHttpClientCache _cache;
        private ISerializerProvider _serializers;
        private IConfiguration _configuration;

        public FluentHttpClientFactory(IHttpClientCache cache, ISerializerProvider serializers, IConfiguration configuration = null)
        {
            _cache = cache;
            _serializers = serializers;
            _configuration = configuration;
        }

        public FluentHttpClient Create()
        {
            if (!_cache.Exists(ABSOLUTE_CLIENT_KEY))
                _cache.Cache(ABSOLUTE_CLIENT_KEY, CreateHttpClient());
            return CreateFluentHttpClient(_cache.Get(ABSOLUTE_CLIENT_KEY));
        }

        public FluentHttpClient Create(string connectionStringName)
        {
            if (!_cache.Exists(connectionStringName))
                _cache.Cache(connectionStringName, CreateHttpClient(connectionStringName));
            return CreateFluentHttpClient(_cache.Get(connectionStringName));
        }

        public event EventHandler<FluentHttpClientCreatedEventArgs> OnClientCreated;

        protected virtual HttpClient CreateHttpClient(string connectionStringName = null)
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(connectionStringName))
            {
                if (_configuration == null)
                {
                    var message =
                        "FluentHttpClientFactory was created with a null configuration." + Environment.NewLine +
                        "If you are creating FluentHttpClientFactory manually, please provide an IConfigurationRoot in the constructor of FluentHttpClientFactory." + Environment.NewLine +
                        // TODO: fix this line in the error message
                        "If you are calling services.UseFluentHttp(), then change that call to services.UseFluentHttp(configuration) where configuration is the application configuration.";
                    throw new InvalidOperationException(message);
                }

                var baseAddress = _configuration.GetConnectionString(connectionStringName);
                client.BaseAddress = new Uri(baseAddress);
            }
            return client;
        }

        private FluentHttpClient CreateFluentHttpClient(HttpClient inner)
        {
            var client = new FluentHttpClient(inner, _serializers);
            if (OnClientCreated != null)
                OnClientCreated(this, new FluentHttpClientCreatedEventArgs { Client = client });
            return client;
        }
    }
}