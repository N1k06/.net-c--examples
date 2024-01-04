using System;
using System.Collections.Generic;

namespace PrisonersDilemmaTournament
{
    public class TournamentHandler
    {
        private int _n_rounds = 10;
        private List<TournamentPlayer> _players = new List<TournamentPlayer>();

        private int[,,] _payoffs = { { {1,1},{5,0} },
                                     { {0,5},{3,3} } };

        public void SetNRounds(int n_rounds)
        {
            if (n_rounds > 0)
                _n_rounds = n_rounds;
        }

        public void SetRewards(int[,,] payoffs)
        {
            if (payoffs.GetLength(0) == 2 && payoffs.GetLength(1) == 2 && payoffs.GetLength(2) == 2)
                _payoffs = payoffs;
        }

        public void SetRewards(int reward, int punishment, int sucker, int temptation)
        {
            _payoffs = new int [,,] { { {punishment,punishment},{temptation,sucker} },
                                      { {sucker,    temptation},{reward,    reward} } };
        }

        public void AddPlayer(IPrisoner prisoner)
        {
            TournamentPlayer player = new TournamentPlayer(prisoner);
            _players.Add(player);
        }


        public void Challenge(TournamentPlayer p1, TournamentPlayer p2)
        {
            bool choice1 = p1.prisoner.MakeChoice();
            bool choice2 = p2.prisoner.MakeChoice();

            p1.prisoner.InformChoice(choice2);
            p2.prisoner.InformChoice(choice1);

            int i = Convert.ToInt32(choice1);
            int j = Convert.ToInt32(choice2);

            int reward1 = _payoffs[i, j, 0];
            int reward2 = _payoffs[i, j, 1];

            p1.AddScore(reward1);
            p2.AddScore(reward2);
        }

        public void Play()
        {
            foreach (TournamentPlayer p1  in _players)
            {
                foreach (TournamentPlayer p2 in _players)
                {
                    for (int i = 0; i < _n_rounds; i++)
                    {
                        if (p1 != p2)
                        {
                            Challenge(p1, p2);
                            Console.WriteLine("{0}:{1} - {2}:{3}", p1.prisoner.GetType().Name, p1.GetScore(), p2.prisoner.GetType().Name, p2.GetScore());
                        }
                    }

                    //richiama il costruttore di prisoner senza conoscere in anticipo
                    //la classe dell'istanza. in questa maniera si resetta la memoria del 
                    //prigioniero prima di altre sfide.
                    p1.prisoner = Activator.CreateInstance(p1.prisoner.GetType()) as IPrisoner;
                    p2.prisoner = Activator.CreateInstance(p2.prisoner.GetType()) as IPrisoner;
                }
            }
        }

        public void PrintResults()
        {
            // ordina la lista per punteggio in ordine decrescente
            _players.Sort((p1, p2) => p2.GetScore().CompareTo(p1.GetScore()));

            // stampa i risultati
            Console.WriteLine("Risultati del torneo:");
            foreach (var player in _players)
            {
                Console.WriteLine("{0}:{1}", player.prisoner.GetType().Name, player.GetScore());
            }
        }
    }
}
