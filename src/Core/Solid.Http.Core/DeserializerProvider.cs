﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http
{
    internal class DeserializerProvider
    {
        private ConcurrentDictionary<(string MediaType, Type TypeToReturn), IDeserializer> _cache = new ConcurrentDictionary<(string MediaType, Type TypeToReturn), IDeserializer>();
        private IEnumerable<IDeserializer> _deserializers;

        public DeserializerProvider(IEnumerable<IDeserializer> deserializers)
        {
            _deserializers = deserializers;
        }

        public IDeserializer GetDeserializer(MediaTypeHeaderValue contentType, Type typeToReturn)
            => _cache.GetOrAdd((contentType.MediaType, typeToReturn), key => _deserializers.FirstOrDefault(d => d.CanDeserialize(key.MediaType, key.TypeToReturn)));
    }
}
