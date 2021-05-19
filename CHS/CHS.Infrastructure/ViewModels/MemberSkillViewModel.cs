using System;

namespace CHS.Infrastructure.ViewModels
{
    public class MemberSkillViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double ExpertLevel { get; set; }
        public double YearsInTraining { get; set; }
        public double YearsAsTrainer { get; set; }
        public string Designation { get; set; }
    }
}
