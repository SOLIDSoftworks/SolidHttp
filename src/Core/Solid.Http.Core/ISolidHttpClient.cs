﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http
{
    public interface ISolidHttpClient
    {
        Uri BaseAddress { get; set; }
        ISolidHttpClient OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler);
        ISolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken = default);
    }
}