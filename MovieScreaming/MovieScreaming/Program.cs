using Akka.Actor;
using MovieScreaming.Actors;
using MovieScreaming.Messages;
using System;

namespace MovieScreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system is created");

            Props playbackActorProps = Props.Create<PlaybackActor>();

            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps,"PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));
            playbackActorRef.Tell(new PlayMovieMessage("Codenan The Destroyer", 1));
            playbackActorRef.Tell(new PlayMovieMessage("Terminator", 99));


            Console.ReadLine();

            MovieStreamingActorSystem.Terminate();

            MovieStreamingActorSystem.AwaitTermination();

            Console.WriteLine("Actor system shutdown");

            Console.ReadKey();

            

        }
    }
}
