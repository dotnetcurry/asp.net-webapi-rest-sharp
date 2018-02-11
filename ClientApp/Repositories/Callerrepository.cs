using ClientApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;


namespace ClientApp.Repositories
{
    public interface ICallRepository
    {
        List<EmployeeInfo> Get();
        EmployeeInfo Get(int id);
        EmployeeInfo Create(EmployeeInfo entity);
        string Update(int id, EmployeeInfo entity);
        string Delete(int id);
    }

    public class CallerRepository : ICallRepository
    {
        string apiUrl;
        RestClient client;
        public CallerRepository()
        {
            apiUrl = "http://localhost:54018/api/EmployeeInfoAPI";
            client = new RestClient(apiUrl);
        }

        public EmployeeInfo Create(EmployeeInfo entity)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(entity);
            var response = client.Execute<EmployeeInfo>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Some Error Occured {response.Content}" +
                    $"{response.StatusDescription}");
            }
            return response.Data;
        }

        public string Delete(int id)
        {
            var request = new RestRequest($"{apiUrl}/{id}", Method.DELETE);
            var response = client.Execute<bool>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Some Error Occured {response.Content}");
            }
            return response.Content;
        }

        public List<EmployeeInfo> Get()
        {
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<EmployeeInfo>>(request);
            return response.Data;
        }

        public EmployeeInfo Get(int id)
        {
            IRestResponse<EmployeeInfo> response = null;
            var request = new RestRequest($"{apiUrl}/{id}", Method.GET);
            response = client.Execute<EmployeeInfo>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Some Error Occured {response.Content}");
            }
            return response.Data;
        }

        public string Update(int id, EmployeeInfo entity)
        {
            var request = new RestRequest($"{apiUrl}/{id}", Method.PUT);
            request.AddJsonBody(entity);
            var response = client.Execute<bool>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Some Error Occured {response.Content}");
            }
            return response.Content;
        }
    }
}
