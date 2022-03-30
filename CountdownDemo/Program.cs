using System.Reactive.Linq;

void Countdown(int seconds)
{
    Observable
        .Timer(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1))
        .Select(currentSeconds => seconds - currentSeconds)
        .TakeWhile(currentSeconds => currentSeconds > 0)
        .Subscribe((currentSeconds) =>
        {
            Console.WriteLine(currentSeconds);
        }, 
        () =>
        {
            Console.WriteLine("Blast off!");
        });
}

Countdown(5);

Console.ReadLine();