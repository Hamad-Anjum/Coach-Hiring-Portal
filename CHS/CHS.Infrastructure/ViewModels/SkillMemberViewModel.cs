using System;

namespace CHS.Infrastructure.ViewModels
{
    public class SkillMemberViewModel
    {
        public Guid SkillId { get; set; }
        public string SkillName { get; set; }
        public int YearsInTraining { get; set; }
        public int YearsAsTrainer { get; set; }
        public Guid MemberId { get; set; }
        public double ExpertLevel { get; set; }
    }
}
