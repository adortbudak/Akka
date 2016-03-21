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

            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");
                        
            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Akka.NET: The Movie)");
            userActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));


            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Boolean Lies)");
            userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());
            
            
            Console.ReadKey();

            MovieStreamingActorSystem.Terminate();

            MovieStreamingActorSystem.AwaitTermination();
                        

            Console.WriteLine("Actor system shutdown");

            Console.ReadKey();

            

        }
    }
}
