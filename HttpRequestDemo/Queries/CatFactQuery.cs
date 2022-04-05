using HttpRequestDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestDemo.Queries
{
    public class CatFactQuery
    {
        public async Task<CatFact> Execute()
        {
            using(HttpClient client = new HttpClient())
            {
                CatFactResponse? response = await client.GetFromJsonAsync<CatFactResponse>("https://catfact.ninja/fact");

                if(response == null)
                {
                    throw new Exception();
                }

                return new CatFact()
                {
                    Content = response.Fact
                };
            }
        }

        private class CatFactResponse
        {
            public string Fact { get; set; }
        }
    }
}
