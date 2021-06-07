using System;

namespace ConsoleApplication1.Entity
{
    public class Transaction
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }

        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Status { get; set; } // 1. Giao dịnh thành công 0. Giao dịch thất bại //
        public string Message { get; set; }
        public string Content { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }

        public Transaction()
        {
            
        }
        public Transaction(string status, string message, string type, DateTime createAt, string accountNumber)
        {
            AccountNumber = accountNumber;
            Status = status;
            Message = message;
            Type = type;
            CreateAt = createAt;
        }
        

        public override string ToString()
        {
            return $"Time :{CreateAt} |Message: {Message} | Type: {Type} | Status: {Status}\n";
        }
    }
}