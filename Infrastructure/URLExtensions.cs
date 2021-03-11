using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nile.Infrastructure
{
    public static class URLExtensions
    {
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue ? "{request.Path}{request.QueryString}" : request.Path.ToString();
    }
}
