using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogApp.Common
{
    public class apiPlugin: IapiPlugin
    {
        private readonly IConfiguration _configuration;
        public apiPlugin(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void pagePublish(string message, string postUrl)
        {
            try
            {
                var access_token = _configuration["fbpageToken"];

                HttpClient client = new HttpClient();

                var values = new Dictionary<string, string> { { "message", message }, { "link", postUrl }, { "access_token", access_token } };

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync("https://graph.facebook.com/111224040351840/feed", content);

                var responseString = response.Result;

            }
            catch (Exception ex)
            {

            }
        }
    }
}
