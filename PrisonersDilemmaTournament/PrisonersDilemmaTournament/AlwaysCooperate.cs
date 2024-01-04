namespace PrisonersDilemmaTournament
{
    public class AlwaysCooperate : IPrisoner
    {
        public bool MakeChoice()
        {
            // collabora sempre
            return true;
        }

        public void InformChoice(bool otherPrisonersChoice)
        {
            //non utilizza l'informazione ricevuta
        }
    }
}