namespace PrisonersDilemmaTournament
{
    public interface IPrisoner
    {
        //Ritorna true (il prigioniero collabora) o 
        //ritorna false (il prigioniero non collabora)
        //in base ad una strategia decisa in fase di implementazione.
        bool MakeChoice();

        //Informa il prigioniero della scelta fatta dall'altro.
        //Tale informazione è utile per la strategia. Un prigioniero
        //decide se collaborare o meno in base alle scelte passate
        //dell'altro prigioniero.
        void InformChoice(bool otherPrisonersChoice);
    }
}

