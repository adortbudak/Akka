using Akka.Actor;
using MovieScreaming.Messages;
using System;

namespace MovieScreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart");            
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("Playback PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {

            ColorConsole.WriteLineGreen("PlaybackActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }

        private void HandlePlayMovieMessage(PlayMovieMessage m)
        {
            ColorConsole.WriteLineYellow(
                string.Format("PlayMovieMessage '{0}' for user {1}",
                    m.MovieTitle,
                    m.UserId));

            
        }
        

                
    }
}
