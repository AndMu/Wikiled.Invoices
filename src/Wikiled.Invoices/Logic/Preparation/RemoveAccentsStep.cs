using Wikiled.Common.Extensions;

namespace Wikiled.Invoices.Logic.Preparation
{
    public class RemoveAccentsStep : IPreparationStep
    {
        public string Prepare(string text)
        {
            return text.RemoveDiacritics();
        }
    }
}
