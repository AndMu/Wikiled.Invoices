﻿using System;
using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Fields
{
    public class StaticFieldExtractor
    {
        public IEnumerable<FieldResult> Extract(InvoiceField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (!field.Key.StartsWith("static_", StringComparison.OrdinalIgnoreCase))
            {
                yield break;
            }

            var key = field.Key.Substring(6);
            foreach (var value in field.Values)
            {
                yield return new FieldResult(key, value);
            }
        }
    }
}
