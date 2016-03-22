using Akka.Actor;
using MovieScreaming.Messages;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace MovieScreaming.Actors
{
    public class MoviePlayCounterActor :ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));

        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            if (_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (message.MovieTitle == "ALPER")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteLineMagenta(string.Format("MoviePlayCounterActor '{0}' has been watched {1} times",
                            message.MovieTitle, _moviePlayCounts[message.MovieTitle]));
        }


        protected override void PreStart()
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {

            ColorConsole.WriteLineMagenta("MoviePlayCounterActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}