// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class HttpTrigger
    {
        private readonly ILogger _logger;

        public HttpTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        /*
        [Function("fallback")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            // https://github.com/Azure/azure-functions-dotnet-worker/issues/1635
            // https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide?tabs=hostbuilder%2Cwindows#aspnet-core-integration
            // var response = req.CreateResponse(HttpStatusCode.OK);
            // response.WriteAsJsonAsync("result");
            // return response;
            var response = req.CreateResponse(System.Net.HttpStatusCode.Redirect);
            response.Headers.Add("Location", "/404?originalUrl=");
            return response;
        }
        */

        [Function("fallback")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            bool hasHeader = req.Headers.TryGetValue("x-ms-original-url", out  Microsoft.Extensions.Primitives.StringValues originalUrl);

            _logger.LogInformation($"Original Url: {originalUrl}");

            Uri? uri = new Uri(originalUrl.ToString());
            _logger.LogInformation(uri.AbsolutePath);

            // RedirectResult result = new RedirectResult($"/404?originalUrl={uri.AbsolutePath}");
            // result.Permanent = true;
            // return result;

            // Fallback to 404
            NotFoundResult result = new NotFoundResult();
            return result;
        }
    }
}