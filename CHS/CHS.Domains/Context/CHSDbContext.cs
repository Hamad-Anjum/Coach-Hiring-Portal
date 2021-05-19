using System;

using CHS.Domains.Models;
using CHS.Domains.Models.Chat;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CHS.Domains.Context
{
    public class CHSDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public CHSDbContext(DbContextOptions<CHSDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<AlertType> AlertTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Certification> Certifications { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponType> CouponTypes { get; set; }

        public DbSet<Designation> Designations { get; set; }
        public DbSet<District> Districts { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Following> Followings { get; set; }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymMember> GymMembers { get; set; }
        public DbSet<GymSubscription> GymSubscriptions { get; set; }
        //public DbSet<Member> Members { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MemberAlert> MemberAlerts { get; set; }
        public DbSet<MemberCertification> MemberCertifications { get; set; }
        public DbSet<MemberLanguage> MemberLanguages { get; set; }
        public DbSet<MemberNote> MemberNotes { get; set; }
        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageLike> MessageLikes { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostVideo> PostVideos { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<MemberSkill> MemberSkills { get; set; }
        public DbSet<State> States { get; set; }
        //public DbSet<Specialties> Specialties { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionTiming> SubscriptionTimings { get; set; }
        public DbSet<Timing> Timings { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<ApplicationRole> AspNetRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<City>()
            //        .HasOne(x => x.Member)
            //        .WithOne(x => x.City)
            //        .HasForeignKey<Member>(x=>x.CityId);
            //builder.Entity<Contact>()
            //        .HasOne(c => c.Member).WithMany(x => x.Contacts)
            //        .OnDelete(DeleteBehavior.);
            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            builder.Entity<ApplicationUserRole>(a =>
            {
                a.HasKey(r => new { r.UserId, r.RoleId });

                a.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).IsRequired();
                a.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId).IsRequired();
            });
            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                //// Each Role can have many associated RoleClaims
                //b.HasMany(e => e.RoleClaims)
                //    .WithOne(e => e.Role)
                //    .HasForeignKey(rc => rc.RoleId)
                //    .IsRequired();
            });
            //base.OnModelCreating(builder);
        }
    }
}
