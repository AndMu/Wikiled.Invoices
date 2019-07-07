using System;
using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Preparation
{
    public class PreparationStepsFactory : IPreparationStepsFactory
    {
        private readonly LowerCaseStep lowerCase = new LowerCaseStep();

        private readonly RemoveWhiteSpacesStep removeWhiteSpaces = new RemoveWhiteSpacesStep();

        private readonly RemoveAccentsStep removeAccents = new RemoveAccentsStep();

        public IEnumerable<IPreparationStep> Construct(InvoiceTemplate template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (template.Options.IsLowercase)
            {
                yield return lowerCase;
            }

            if (template.Options.RemoveWhitespaces)
            {
                yield return removeWhiteSpaces;
            }

            if (template.Options.RemoveAccents)
            {
                yield return removeAccents;
            }

            if (template.Options.Replace != null)
            {
                foreach (var pair in template.Options.Replace)
                {
                    yield return new ReplaceStep(pair.Key, pair.Value);
                }
            }
        }
    }
}
