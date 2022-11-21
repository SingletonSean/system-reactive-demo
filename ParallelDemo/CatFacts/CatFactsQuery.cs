using System.Net.Http.Json;

namespace ParallelDemo.CatFacts
{
    public class CatFactsQuery
    {
        public async Task<IEnumerable<CatFact>> Execute()
        {
            using (HttpClient client = new HttpClient())
            {
                CatFactListingResponse response = await client.GetFromJsonAsync<CatFactListingResponse>("https://catfact.ninja/facts?limit=25");

                if (response == null)
                {
                    throw new Exception();
                }

                return response.Data
                    .Select(c => new CatFact()
                    {
                        Content = c.Fact
                    });
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
