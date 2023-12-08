namespace PaymentsApi.Models
{
    public class Transaction
    {
        public Transaction(string transactionId)
        {
            this.transactionId = transactionId;
        }

        public ulong accountNumber { get; set; } = 0;

        public string accountName { get; set; } = string.Empty;

        public string accountCategory { get; set; } = string.Empty;

        public string accountProvider { get; set; } = string.Empty;

        public string bankCode { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public int tranAmount { get; set; } = 0;
        public string tranType { get; set; } = string.Empty;

        public string tranCategory { get; set; } = string.Empty;

        public string channel { get; set; } = string.Empty;

        public string currency { get; set; } = string.Empty;

        public DateTime paymentDate { get; set; }  = DateTime.UtcNow;

        public string tranSignature { get; set; } = string.Empty;
        public string transactionId { get; set; }
        public string narration { get; set; } = string.Empty;

        
    }
}
