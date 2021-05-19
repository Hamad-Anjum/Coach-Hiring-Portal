using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class Status : Common
    {
        public string StatusName { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
