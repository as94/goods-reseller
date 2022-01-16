using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;

namespace GoodsReseller.IntegrationTests.Infrastructure
{
	internal sealed class GoodsResellerClient
    {
        private readonly RestClient _restClient;
        
        public GoodsResellerClient()
        {
	        _restClient = new RestClient(Configurations.BaseUrl);
        }
        
        public async Task<GoodsResellerResult<TResult>> GetAsync<TResult>(
	        string uriPath,
			IEnumerable<KeyValuePair<string, string>> parameters)
		{
			var request = new RestRequest(uriPath);
			foreach (var parameter in parameters)
			{
				request.AddParameter(new QueryParameter(parameter.Key, parameter.Value));
			}

			return await HandleResponseAsync<TResult>(request);
		}

		public async Task<GoodsResellerResult<TResult>> GetAsync<TResult>(string uriPath, string resourceId)
		{
			var request = new RestRequest($"{uriPath}/{resourceId}");

			return await HandleResponseAsync<TResult>(request);
		}

		public async Task<GoodsResellerResult> PostAsync<T>(string uriPath, T data) where T : class
		{
			var request = new RestRequest(uriPath, Method.Post)
				.AddJsonBody(data);

			return await HandleResponseAsync(request);
		}

		public async Task<GoodsResellerResult> PutAsync<T>(string uriPath, Guid id, T data) where T : class
		{
			var url = new Uri(new Uri(uriPath + "/"), id.ToString());

			var request = new RestRequest(url, Method.Put)
				.AddJsonBody(data);

			return await HandleResponseAsync(request);
		}

		public async Task<GoodsResellerResult> DeleteAsync(string uriPath, Guid id)
		{
			var url = new Uri(new Uri(uriPath + "/"), id.ToString());

			var request = new RestRequest(url, Method.Delete);

			return await HandleResponseAsync(request);
		}

		private async Task<GoodsResellerResult<TResult>> HandleResponseAsync<TResult>(RestRequest request)
		{
			var response = await _restClient.ExecuteAsync(request);

			if (!response.IsSuccessful && response.ErrorException != null)
			{
				if (response.ErrorException.InnerException != null)
				{
					throw response.ErrorException.InnerException;
				}
				
				throw response.ErrorException;
			}

			if (response.IsSuccessful)
			{
				var data = JsonConvert.DeserializeObject<TResult>(response.Content, new JsonSerializerSettings
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
				
				return new GoodsResellerResult<TResult>
				{
					StatusCode = response.StatusCode,
					Data = data
				};
			}

			return new GoodsResellerResult<TResult>
			{
				StatusCode = response.StatusCode,
				Error = response.Content
			};
		}

		private async Task<GoodsResellerResult> HandleResponseAsync(RestRequest request)
		{
			var response = await _restClient.ExecuteAsync(request);

			if (!response.IsSuccessful && response.ErrorException != null)
			{
				if (response.ErrorException.InnerException != null)
				{
					throw response.ErrorException.InnerException;
				}
				
				throw response.ErrorException;
			}

			if (response.IsSuccessful)
			{
				return new GoodsResellerResult
				{
					StatusCode = response.StatusCode
				};
			}

			return new GoodsResellerResult
			{
				StatusCode = response.StatusCode,
				Error = response.Content
			};
		}
    }
}