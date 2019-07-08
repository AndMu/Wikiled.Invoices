using System;

namespace Wikiled.Invoices.Logic.Fields
{
    public class FieldResult
    {
        public FieldResult(string key, string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Key { get; }

        public string Value { get; }

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}
