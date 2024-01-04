namespace PrisonersDilemmaTournament
{
    public class AlwaysBetray : IPrisoner
    {
        public bool MakeChoice()
        {
            //non collabora mai
            return false;
        }

        public void InformChoice(bool otherPrisonersChoice)
        {
            //non utilizza l'informazione ricevuta
        }
    }
}