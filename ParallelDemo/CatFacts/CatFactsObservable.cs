using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelDemo.CatFacts
{
    public static class CatFactsObservable
    {
        public static IObservable<CatFactsForkJoinResult> FromCatFactsAsync(
            DailyCatFactQuery dailyCatFactQuery, 
            CatFactsQuery catFactsQuery)
        {
            return ObservableEx
                .ForkJoin(
                    FromDailyCatFactAsync(dailyCatFactQuery),
                    FromCatFactsListingAsync(catFactsQuery),
                    (dailyCatFactResult, catFactsResult) => 
                        new CatFactsForkJoinResult(dailyCatFactResult, catFactsResult))
                .ObserveOn(SynchronizationContext.Current);
        }

        private static IObservable<Result<CatFact>> FromDailyCatFactAsync(DailyCatFactQuery dailyCatFactQuery)
        {
            return Observable
                .FromAsync(() => dailyCatFactQuery.Execute())
                .Retry(1)
                .Select(data => new Result<CatFact>(data))
                .Catch<Result<CatFact>, Exception>(ex =>
                    Observable.Return(new Result<CatFact>(ex)));
        }

        private static IObservable<Result<IEnumerable<CatFact>>> FromCatFactsListingAsync(CatFactsQuery catFactsQuery)
        {
            return Observable
                .FromAsync(() => catFactsQuery.Execute())
                .Retry(1)
                .Select(data => new Result<IEnumerable<CatFact>>(data))
                .Catch((Func<Exception, IObservable<Result<IEnumerable<CatFact>>>>)(ex =>
                    Observable.Return(new Result<IEnumerable<CatFact>>(ex))));
        }
    }

    public class CatFactsForkJoinResult
    {
        public Result<CatFact> DailyCatFact { get; set; }
        public Result<IEnumerable<CatFact>> CatFacts { get; set; }

        public CatFactsForkJoinResult(Result<CatFact> dailyCatFact, Result<IEnumerable<CatFact>> catFacts)
        {
            DailyCatFact = dailyCatFact;
            CatFacts = catFacts;
        }
    }

    public class Result<T>
    {
        public T Data { get; set; }
        public Exception Error { get; set; }

        public Result(T data)
        {
            Data = data;
        }

        public Result(Exception error)
        {
            Error = error;
        }
    }
}
