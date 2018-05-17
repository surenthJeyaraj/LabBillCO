using System.ComponentModel;

namespace Domain.Model
{
    public enum TransactionMode
    {
        [Description("Batch")]
        IsBatch = 0,
        [Description("Realtime")]
        IsRealTime = 1,
    }
}