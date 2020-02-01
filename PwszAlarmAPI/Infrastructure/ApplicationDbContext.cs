using Microsoft.AspNet.Identity.EntityFramework;
using PwszAlarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PwszAlarmAPI.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}