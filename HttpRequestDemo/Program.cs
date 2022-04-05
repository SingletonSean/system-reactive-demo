using HttpRequestDemo.Models;
using HttpRequestDemo.Queries;

CatFactQuery catFactQuery = new CatFactQuery();

CatFact catFact = await catFactQuery.Execute();

Console.WriteLine(catFact.Content);

Console.ReadLine();
