using Akka.Actor;

using MovieStreaming.Common.Messages;
using System;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {        

        private string _currentlyWatching;

        public UserActor(int userId)
        {
            //Console.WriteLine("Creating a UserActor");
            //ColorConsole.WriteLineYellow("Setting the initial behaviour to stopped");
            Stopped();
        }      
                       

        private void Playing()
        {
            Receive<PlayMovieMessage>(message =>
                ColorConsole.WriteLineRed("Error: A movie is already playing"));
            Receive<StopMovieMessage>
                (message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow("UserActor has now become playing");

        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>
                (message =>  ColorConsole.WriteLineRed("Cannot stop if nothing is playing"));

            ColorConsole.WriteLineYellow("UserActor has now become stopped");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLineYellow(string.Format("User currently playing movie: {0}", movieTitle));

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(movieTitle));

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
            ColorConsole.WriteLineYellow("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow(string.Format("UserActor {0} PostStop",_currentlyWatching));
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow("UserActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
