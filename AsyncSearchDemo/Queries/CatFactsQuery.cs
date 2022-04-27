using AsyncSearchDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AsyncSearchDemo.Queries
{
    public class CatFactsQuery
    {
        public async Task<IEnumerable<CatFact>> Execute(string search = "")
        {
            using (HttpClient client = new HttpClient())
            {
                await Task.Delay(1000);

                CatFactListingResponse? response = await client.GetFromJsonAsync<CatFactListingResponse>("https://catfact.ninja/facts?limit=332");

                if(response == null)
                {
                    throw new Exception();
                }

                return response.Data
                    .Select(c => new CatFact()
                    {
                        Content = c.Fact
                    })
                    .Where(c => c.Content.ToLower().Contains(search.ToLower()));
            }
        }

        private class CatFactListingResponse
        {
            public IEnumerable<CatFactResponse> Data { get; set; }
        }

        private class CatFactResponse
        {
            public string Fact { get; set; }
        }
    }
}
