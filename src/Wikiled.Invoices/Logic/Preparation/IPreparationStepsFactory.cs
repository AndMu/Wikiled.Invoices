using System.Collections.Generic;
using Wikiled.Invoices.Yaml.Data;

namespace Wikiled.Invoices.Logic.Preparation
{
    public interface IPreparationStepsFactory
    {
        IEnumerable<IPreparationStep> Construct(InvoiceTemplate template);
    }
}