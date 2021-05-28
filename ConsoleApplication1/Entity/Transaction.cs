using System;

namespace ConsoleApplication1.Entity
{
    public class Transaction
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Status { get; set; } // 1. Giao dịnh thành công 0. Giao dịch thất bại //
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }

        public Transaction()
        {
            
        }
        public Transaction(string status, string content, string type, DateTime createAt, string accountName)
        {
            AccountName = accountName;
            Status = status;
            Content = content;
            Type = type;
            CreateAt = createAt;
        }
        

        public override string ToString()
        {
            return $"Time :{CreateAt} |Content: {Content} | Type: {Type} | Status: {Status}\n";
        }
    }
}