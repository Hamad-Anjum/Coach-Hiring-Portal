using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class ContactType : Common
    {
        public string Type { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
