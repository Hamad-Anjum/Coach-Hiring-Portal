using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class MemberSkill : Common
    {
        /// <summary>
        /// From 1 - 10
        /// </summary>
        public double ExpertLevel { get; set; }
        /// <summary>
        /// Number of Years Training in that skill
        /// </summary>
        public double YearsInTraining { get; set; }

        /// <summary>
        /// Number of Years being a Trainer for that skill
        /// </summary>
        public double YearsAsTrainer { get; set; }

        [ForeignKey(nameof(Skill))]
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }

        //[ForeignKey(nameof(Member))]
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
