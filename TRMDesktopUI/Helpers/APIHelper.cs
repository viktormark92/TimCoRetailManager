using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TRMDesktopUI.Models;

namespace TRMDesktopUI.Helpers
{
    //It is going to handle all our API call. We may refactor in the feature.
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient { get; set; }

        public APIHelper()
        {
            InitializeClient();
        }

        //We are gona have one HttpClient for the lifetime of the application.
        //We need to create a call which calls the api and get back the login token.
        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];
            apiClient = new HttpClient();
            apiClient.BaseAddress = new System.Uri(api);//app.config -> appSettings + add reference system.configuration. 
            //Other thing is : AppSettings value can be changed in everytime. In runtime as well or we can create a configfile to overrive it.

            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            //we have to send UrlEncoded contetn for username and pasword and the grand type. This is like in postman.
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            // we use using statement because of IDisposable
            // the request Url is not constant. It deppends on your hosting method. LocalHost/Address in azure or something else. We shouldn't hardcode it here.
            //we set up a BaseAddress for it in init method.
            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if(response.IsSuccessStatusCode)
                {
                    // add a reference, manage nuget package: aspnet.webapi.client here as well to get ReadAsASync  then update newtonsoft
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
