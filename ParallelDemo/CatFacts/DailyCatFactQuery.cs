using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ParallelDemo.CatFacts
{
    public class DailyCatFactQuery
    {
        public async Task<CatFact> Execute()
        {
            using (HttpClient client = new HttpClient())
            {
                CatFactListingResponse response = await client.GetFromJsonAsync<CatFactListingResponse>("https://catfact.ninja/facts?limit=10");

                if (response == null)
                {
                    throw new Exception();
                }

                IEnumerable<CatFact> catFacts = response.Data
                    .Select(c => new CatFact()
                    {
                        Content = c.Fact
                    });

                return catFacts.ElementAt(4);
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
