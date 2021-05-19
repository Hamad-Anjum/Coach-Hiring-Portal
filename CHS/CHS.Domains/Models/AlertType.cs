using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class AlertType : Common
    {
        public string Name { get; set; }
        public ICollection<MemberAlert> MemberAlerts { get; set; }
    }
}
