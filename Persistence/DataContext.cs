using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Activity> Activities { get; set; } //set nhieu hang cua cung 1 table
        public DbSet<ActivityAttendee> ActivityAttendees {get;set;}

        // create many to many relationship between Activity and Attendee tables
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new {aa.AppUserId, aa.ActivityId}));  //form a primary key in table
            builder.Entity<ActivityAttendee>()
                .HasOne( u => u.AppUser)
                .WithMany(a => a.Activities )
                .HasForeignKey(aa => aa.AppUserId );
            builder.Entity<ActivityAttendee>()
                .HasOne( u => u.Activity)
                .WithMany(a => a.Attendees )
                .HasForeignKey(aa => aa.ActivityId );
        }
    }
}