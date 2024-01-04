namespace PrisonersDilemmaTournament
{
    public class AlwaysBetray : IPrisoner
    {
        public bool MakeChoice()
        {
            return false; // Tradisci sempre
        }

        public void InformChoice(bool otherPrisonersChoice)
        {
            // Non fa nulla con l'informazione ricevuta
        }
    }
}