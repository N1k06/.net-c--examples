using System;

namespace PrisonersDilemmaTournament
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            TournamentHandler tournament = new TournamentHandler();

            tournament.SetNRounds(1000);
            //tournament.SetRewards(3,1,0,5);

            // good - non tradiscono mai per primi
            tournament.AddPlayer(new AlwaysCooperate());

            // nasty - sono i primi a tradire
            tournament.AddPlayer(new AlwaysBetray());

            tournament.Play();
            tournament.PrintResults();
        }
    }
}
