using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Interfaces.RemoteConnection;

namespace Classes.RemoteConnection
{
    public class RestSharpConsumer
    {
        public string DeleteResponse(string uri, string resource)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(resource, Method.DELETE);
            client.BaseUrl = new Uri(uri);
            string response = client.Execute(request).Content;

            return response;
        }

        public List<T> GetResponseAsJSONList<T>(string uri, string resource) where T : IRestTransferable
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(resource, Method.GET);
            client.BaseUrl = new Uri(uri);
            List<T> dto = client.Execute<List<T>>(request).Data;

            return dto;
        }

        public List<T> GetResponseAsJSONList<T>(string uri) where T : IRestTransferable
        {
            RestClient client = new RestClient(new Uri(uri));
            RestRequest request = new RestRequest(Method.GET);
            List<T> dto = client.Execute<List<T>>(request).Data;

            return dto;
        }

        public T GetResponseAsJSON<T>(string uri, string resource) where T : IRestTransferable, new()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(resource, Method.GET);
            client.BaseUrl = new Uri(uri);
            T dto = client.Execute<T>(request).Data;

            return dto;
        }

        public T GetResponseAsJSON<T>(string uri) where T : IRestTransferable, new()
        {
            RestClient client = new RestClient(new Uri(uri));
            RestRequest request = new RestRequest(Method.GET);
            T dto = client.Execute<T>(request).Data;

            return dto;
        }

        public string GetResponseAsSring(string uri)
        {
            RestClient client = new RestClient(new Uri(uri));
            RestRequest request = new RestRequest(Method.GET);
            client.RemoveDefaultParameter("Accept");
            client.AddDefaultHeader("Accept", "text/plain");
            var response = client.Execute(request);

            return response.Content;
        }

        public T PostResponseAsJSON<T>(string uri, string resource, IRestTransferable dto) where T : IRestTransferable, new()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(uri);
            string json = JsonConvert.SerializeObject(dto);

            RestRequest request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            return client.Execute<T>(request).Data;
        }

        public T PutResponseAsJSON<T>(string uri, string resource, IRestTransferable dto) where T : IRestTransferable, new()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(uri);
            string json = JsonConvert.SerializeObject(dto);

            RestRequest request = new RestRequest(resource, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            return client.Execute<T>(request).Data;
        }
    }
}
