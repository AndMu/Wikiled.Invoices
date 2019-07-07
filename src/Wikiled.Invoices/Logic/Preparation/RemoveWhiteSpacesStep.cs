using Wikiled.Common.Extensions;

namespace Wikiled.Invoices.Logic.Preparation
{
    public class RemoveWhiteSpacesStep : IPreparationStep
    {
        public string Prepare(string text)
        {
            return text.RemoveCharacters(false, ' ');
        }
    }
}
