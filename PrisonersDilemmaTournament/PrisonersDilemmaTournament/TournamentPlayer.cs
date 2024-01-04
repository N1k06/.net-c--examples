using System;
namespace PrisonersDilemmaTournament
{
    public class TournamentPlayer
    {
        public IPrisoner prisoner;
        private int _score;

        public TournamentPlayer(IPrisoner prisoner)
        {
            this.prisoner = prisoner;
            _score = 0;
        }

        public int GetScore()
        {
            return _score;
        }

        public void AddScore(int reward)
        {
            _score += reward;
        }
    }
}
