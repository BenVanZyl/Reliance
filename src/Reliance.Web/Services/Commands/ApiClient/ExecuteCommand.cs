using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Reliance.Core.Services.Infrastructure;
using Reliance.Web.Services.Support;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Commands.ApiClient
{
    public class ExecuteCommand : IRequest<string>
    {
        public enum HttpAction
        {
            Get,
            Post,
            Put,
            Push,
            Delete
        }

        public HttpAction Action { get; set; }
        public string RequestUri { get; set; }
        public StringContent Data { get; set; } = null;

        public ExecuteCommand(HttpAction action, string requestUri)
        {
            Action = action;
            RequestUri = requestUri;
        }

        public ExecuteCommand(HttpAction action, string requestUri, object data)
        {
            Action = action;
            RequestUri = requestUri;
            Data = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"); ;
        }
    }

    public class ExecuteCommandHandler : IRequestHandler<ExecuteCommand, string>
    {
        private readonly IApiClient _apiClient;
        //private readonly IMapper _mapper;

        public ExecuteCommandHandler(IApiClient apiClient) //, IMapper mapper)
        {
            _apiClient = apiClient;
            //_mapper = mapper;
        }

        public async Task<string> Handle(ExecuteCommand request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            string apiResponse = "";

            try
            {
                switch (request.Action)
                {
                    case ExecuteCommand.HttpAction.Get:
                        response = await _apiClient.Client.GetAsync(request.RequestUri);
                        break;
                    case ExecuteCommand.HttpAction.Post:
                        response = await _apiClient.Client.PostAsync(request.RequestUri, request.Data);
                        break;
                    case ExecuteCommand.HttpAction.Put:
                        response = await _apiClient.Client.PutAsync(request.RequestUri, request.Data);
                        break;
                    case ExecuteCommand.HttpAction.Push:
                        response = await _apiClient.Client.PutAsync(request.RequestUri, request.Data);
                        break;
                    case ExecuteCommand.HttpAction.Delete:
                        response = await _apiClient.Client.DeleteAsync(request.RequestUri);
                        break;
                    default:
                        throw new ThisAppException(System.Net.HttpStatusCode.MethodNotAllowed, "Missing Http Action");
                }

                if (response == null)
                    throw new ThisAppException(System.Net.HttpStatusCode.NotFound, "No respone message received.");

                apiResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new ThisAppException(response.StatusCode, $"Save Organisation failed: {response.StatusCode}: {apiResponse}");

            }
            catch (Exception ex)
            {
                Log.Logger.Error($"ERROR: Execute Http Action {request.Action}", ex);
            }

            return apiResponse;
        }
    }
}
