using System;

namespace Wikiled.Invoices.Logic
{
    public class Document
    {
        public Document(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Text { get; }
    }
}
