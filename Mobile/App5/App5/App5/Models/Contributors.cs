using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App5.Models
{
    public class Contributors
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Date { get; set; }
    }
}
