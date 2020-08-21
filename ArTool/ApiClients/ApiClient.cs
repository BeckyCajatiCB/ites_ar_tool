using Flurl.Http;
using FlurlClientWrapper.Core;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ArTool.Exceptions;
using ArTool.Models;


namespace ArTool.ApiClients
{
    public class ApiClient
    {
        private readonly ILogger _logger;
        protected readonly ApiClientOptions ApiOptions;

        protected ApiClient(ApiClientOptions apiOptions, ILogger logger)
        {
            ApiOptions = apiOptions;
            _logger = logger;
        }

        protected async Task<T> PostJsonAsync<T>(string path, object body, RequestorInformation requestorInformation)
        {
            try
            {
                _logger?.Info(GetInfoText("POST", path, requestorInformation));

                var request = CreateFlurlRequest(path, requestorInformation);

                return await request.PostJsonAsync(body)
                    .ReceiveJson<T>();
            }
            catch (FlurlHttpException fhex)
            {
                var errorText = fhex.Call.Response != null ? await fhex.Call.Response.Content.ReadAsStringAsync() : null;

                _logger?.Error(GetErrorText("POST", path, requestorInformation, fhex.Call.HttpStatus, errorText));
                throw new ClientException("PostJsonAsync failure", fhex, fhex.Call.HttpStatus, GetErrorJson(errorText));
            }
        }

        protected async Task<T> GetJsonAsync<T>(string path, RequestorInformation requestorInformation)
        {
            try
            {
                _logger?.Info(GetInfoText("GET", path, requestorInformation));

                var request = CreateFlurlRequest(path, requestorInformation);
                return await request.GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }

                _logger?.Error(GetErrorText("GET", path, requestorInformation, ex.Call.HttpStatus, ex.Message));
                throw new ClientException("GetJsonAsync failure", ex, ex.Call.HttpStatus,
                    GetErrorJson(await ex.Call.Response.Content.ReadAsStringAsync()));
            }
        }

        protected async Task<List<T>> GetJsonListAsync<T>(string path, RequestorInformation requestorInformation)
        {
            try
            {
                _logger?.Info(GetInfoText("GET", path, requestorInformation));

                var request = CreateFlurlRequest(path, requestorInformation);
                var response = await request.GetAsync();
                var listData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<T>>(listData);
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }

                _logger?.Error(GetErrorText("GET", path, requestorInformation, ex.Call.HttpStatus, ex.Message));
                throw new ClientException("GetJsonListAsync failure", ex, ex.Call.HttpStatus,
                    GetErrorJson(await ex.Call.Response.Content.ReadAsStringAsync()));
            }
        }

        private IFlurlRequest CreateFlurlRequest(string requestPath, RequestorInformation requestorInformation)
        {
            if (string.IsNullOrWhiteSpace(requestorInformation.RequestId))
            {
                requestorInformation.RequestId = GenerateRequestId();
            }

            if (ApiOptions.ApiBaseUrl == null)
            {
                throw new ClientException("Missing api options base url");
            }

            if (ApiOptions.TokenHeader == null)
            {
                throw new ClientException("Missing api options token header");
            }

            var requestUri = $"{ApiOptions.ApiBaseUrl}{requestPath}";
            var factory = new FlurlRequestFactory(requestorInformation.RequestId, requestorInformation.UserId, requestorInformation.ImpersonatedUserId);

            return factory.Create(requestUri, ApiOptions.TokenHeader);
        }

        public static string GenerateRequestId()
        {
            return Guid.NewGuid().ToString();
        }

        private static string GetInfoText(string action, string path, RequestorInformation requestorInformation)
        {
            return $"{action} Url: {path}, RequestId: {requestorInformation.RequestId}, UserId: {requestorInformation.UserId}, ImpersonatedUserId: {requestorInformation.ImpersonatedUserId}";
        }

        private static string GetErrorText(string action, string path, RequestorInformation requestorInformation, HttpStatusCode? httpStatusCode, string errorResponse)
        {
            return
                $"{action} Url: {path}, RequestId: {requestorInformation.RequestId}, UserId: {requestorInformation.UserId}, ImpersonatedUserId: {requestorInformation.ImpersonatedUserId}, HttpStatusCode: {httpStatusCode}, Error: {errorResponse}";
        }

        private static dynamic GetErrorJson(string errorResponse)
        {
            return errorResponse != null ? JsonConvert.DeserializeObject<dynamic>(errorResponse) : null;
        }
    }
}

