using System.ComponentModel;

namespace Domain.Model
{
    public enum TransactionType
    {
        [Description("Testing")]
        IsTesting = 0,
        [Description("Production")]
        IsProduction = 1,
    }
}