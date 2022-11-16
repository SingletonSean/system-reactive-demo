using System.Net.Http.Json;

namespace PaginationDemo.CatFacts
{
    public class CatFactsQuery
    {
        public async Task<CatFactListing> Execute(int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
        {
            using (HttpClient client = new HttpClient())
            {
                CatFactListingResponse response = await client.GetFromJsonAsync<CatFactListingResponse>("https://catfact.ninja/facts?limit=45", cancellationToken);

                if (response == null)
                {
                    throw new Exception();
                }

                IEnumerable<CatFact> catFacts = response.Data
                    .Skip(offset)
                    .Take(limit)
                    .Select(c => new CatFact()
                    {
                        Content = c.Fact
                    });

                return new CatFactListing()
                {
                    CatFacts = catFacts,
                    Total = response.Data.Count()
                };
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
