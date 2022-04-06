using HttpRequestDemo.Models;
using HttpRequestDemo.Queries;
using System.Reactive.Linq;

CatFactQuery catFactQuery = new CatFactQuery();

Observable
    .FromAsync(() => catFactQuery.Execute())
    .ValidateCatFactLength(80)
    .Retry(3)
    .Catch(Observable.Return(new CatFact() { Content = "Cats are cool." }))
    .Subscribe((catFact) =>
    {
        Console.WriteLine(catFact.Content);
    });

Console.ReadLine();

static class CatFactObservableExtensions
{
    public static IObservable<CatFact> ValidateCatFactLength(this IObservable<CatFact> observable, int maxLength)
    {
        return observable
            .Select(catFact =>
            {
                if (catFact.Content.Length > maxLength)
                {
                    return Observable.Throw<CatFact>(new Exception("Cat fact was too long."));
                }

                return Observable.Return(catFact);
            })
            .Switch();
    }
};