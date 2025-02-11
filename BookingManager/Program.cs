CancellationTokenSource tokenSrc = new ();
// démarre une action en arrière plan
Task t = Task.Run(() =>
{
    // s'execute sur un thread différent du thread principal
    for (int i = 0; i < 10000000; i++)
    {
        Console.WriteLine(i);
        if(tokenSrc.Token.IsCancellationRequested)
        {
            break;
        }
    }
}, tokenSrc.Token);

ConsoleKey key = Console.ReadKey().Key;
if(key == ConsoleKey.Spacebar)
{
    tokenSrc.Cancel();
}

//await t;
// s'execute sur le thread principal
for (int i = 1000000; i < 20000000; i++)
{
    Console.WriteLine(i);
}