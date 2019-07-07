namespace Wikiled.Invoices.Logic.Preparation
{
    public class LowerCaseStep : IPreparationStep
    {
        public string Prepare(string text)
        {
            return text.ToLower();
        }
    }
}
