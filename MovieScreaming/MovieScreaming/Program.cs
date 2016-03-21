using Akka.Actor;
using System;

namespace MovieScreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            Console.ReadLine();

            MovieStreamingActorSystem.Terminate();

        }
    }
}
