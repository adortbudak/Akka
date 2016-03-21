using Akka.Actor;
using MovieScreaming.Messages;
using System;
using System.Threading.Tasks;

namespace MovieScreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        

        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");
            ColorConsole.WriteLineCyan("Setting the initial behaviour to stopped");
            Stopped();
        }      
                       

        private void Playing()
        {
            Receive<PlayMovieMessage>(message =>
                ColorConsole.WriteLineRed("Error: A movie is already playing"));
            Receive<StopMovieMessage>
                (message => StopPlayingCurrentMovie());

        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>
                (message =>  ColorConsole.WriteLineRed("Cannot stop if nothing is playing"));

            ColorConsole.WriteLineCyan("UserActor has now become stopped");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLineYellow(string.Format("User currently playing movie: {0}", movieTitle));

            Become(Playing);

        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow(string.Format("User has stopped watching: {0}", _currentlyWatching));
            _currentlyWatching = null;

            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {

            ColorConsole.WriteLineGreen("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
