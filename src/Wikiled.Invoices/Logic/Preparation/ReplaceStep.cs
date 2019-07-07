using System;
using Wikiled.Common.Extensions;

namespace Wikiled.Invoices.Logic.Preparation
{
    public class ReplaceStep : IPreparationStep
    {
        private readonly string source;

        private readonly string replacement;

        public ReplaceStep(string source, string replacement)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.replacement = replacement ?? throw new ArgumentNullException(nameof(replacement));
        }

        public string Prepare(string text)
        {
            return text.ReplaceString(source, replacement, ReplacementOption.None);
        }
    }
}
