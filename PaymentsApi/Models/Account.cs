namespace PaymentsApi.Models
{
    public class Account
    {
        public Account(ulong accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public ulong accountNumber { get; set; } = 0;

        public string accountName { get; set; } = string.Empty;

        public string accountCategory { get; set; } = string.Empty;

        public string accountProvider { get; set; } = string.Empty;

        public string bankCode { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public int outstandingBalance { get; set; } = 0;
    }
}
