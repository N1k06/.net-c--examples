namespace PrisonersDilemmaTournament
{
    public class AlwaysCooperate : IPrisoner
    {
        public bool MakeChoice()
        {
            return true; // Collabora sempre
        }

        public void InformChoice(bool otherPrisonersChoice)
        {
            // Non fa nulla con l'informazione ricevuta
        }
    }
}