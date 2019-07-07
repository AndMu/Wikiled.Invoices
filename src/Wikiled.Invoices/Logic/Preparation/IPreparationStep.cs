namespace Wikiled.Invoices.Logic.Preparation
{
    public interface IPreparationStep
    {
        string Prepare(string text);
    }
}