using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Preparation
{
    public class PreparationStepsFactory : IPreparationStepsFactory
    {
        private readonly LowerCaseStep lowerCase = new LowerCaseStep();

        private readonly RemoveWhiteSpacesStep removeWhiteSpaces = new RemoveWhiteSpacesStep();

        private readonly RemoveAccentsStep removeAccents = new RemoveAccentsStep();

        private ILogger<PreparationStepsFactory> logger;

        public PreparationStepsFactory(ILogger<PreparationStepsFactory> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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

            if (template.Options.Replace == null)
            {
                yield break;
            }

            foreach (var pair in template.Options.Replace)
            {
                if (pair.Length == 2)
                {
                    yield return new ReplaceStep(pair[0], pair[1]);
                }
                else
                {
                    logger.LogWarning("Failed parsing replace for {0}", template);
                }
            }
        }
    }
}
