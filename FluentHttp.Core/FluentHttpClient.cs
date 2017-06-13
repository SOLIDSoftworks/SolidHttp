﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    public class FluentHttpClient
    {
        private HttpClient _client;

        public FluentHttpClient(HttpClient client)
        {
            _client = client;
        }

        public event EventHandler<FluentHttpRequestCreatedEventArgs> RequestCreated;

        public FluentHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            var request = new FluentHttpRequest(_client, method, url, cancellationToken);
            if (RequestCreated != null)
                RequestCreated(this, new FluentHttpRequestCreatedEventArgs { Request = request });
            return request;
        }
    }
}
