using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Entity
{
    public class LogItem
    {
        [Key]
        public long Id  { get; set; }
        public DateTime DateOperation { get; set; }
        public string NameOperation { get; set; }
        public decimal Arg1 { get; set; }
        public decimal Arg2 { get; set; }
        public decimal? Result { get; set; }
        public string IpClient { get; set; }
        public string OperationMesage { get; set; }

        public LogItem()
        {
            DateOperation = DateTime.Now;
        }
    }
    
}