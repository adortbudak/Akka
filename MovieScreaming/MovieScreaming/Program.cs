using Akka.Actor;
using MovieScreaming.Actors;
using MovieScreaming.Messages;
using System;
using System.Threading;

namespace MovieScreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("enter a command and hit enter");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("exit"))
                {
                    MovieStreamingActorSystem.Terminate();
                    MovieStreamingActorSystem.AwaitTermination();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            } while (true);


            //Props userActorProps = Props.Create<UserActor>();
            //IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");
                        
            //Console.ReadKey();
            //Console.WriteLine("Sending a PlayMovieMessage (Akka.NET: The Movie)");
            //userActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));


            //Console.ReadKey();
            //Console.WriteLine("Sending a PlayMovieMessage (Boolean Lies)");
            //userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));

            //Console.ReadKey();
            //Console.WriteLine("Sending a StopMovieMessage");
            //userActorRef.Tell(new StopMovieMessage());

            //Console.ReadKey();
            //Console.WriteLine("Sending another StopMovieMessage");
            //userActorRef.Tell(new StopMovieMessage());
            
            
            //Console.ReadKey();

            //MovieStreamingActorSystem.Terminate();

            //MovieStreamingActorSystem.AwaitTermination();
                        

            //Console.WriteLine("Actor system shutdown");

            //Console.ReadKey();

            

        }

        private static void ShortPause()
        {
            Thread.Sleep(2000);
        }
    }
}
