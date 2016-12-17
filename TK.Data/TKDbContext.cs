using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TK.Model.Models;

namespace TK.Data
{
    public class TKDbContext : IdentityDbContext<ApplicationUser>
    {
        public TKDbContext() : base("TKConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<SituationCategory> SituationCategories { set; get; }
        public DbSet<Situation> Situations { set; get; }   
        public DbSet<Statistic> Statistics { set; get; }
        public DbSet<StatisticCategory> StatisticCategories { set; get; }       
        public DbSet<Error> Errors { set; get; }
        public DbSet<ResolvedSituation> ResolvedSituations { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SettleBody> SettleBodies { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }       
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }

        public static TKDbContext Create()
        {
            return new TKDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}