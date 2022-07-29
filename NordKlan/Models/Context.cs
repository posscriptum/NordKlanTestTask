using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NordKlan.Models
{
    /// <summary>
    /// Class <c>Context</c> this is context to db.
    /// </summary>
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BookingEvent> BookingEvents { get; set; }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        /// <summary>
        /// Method <c>OnModelCreating</c> add working with property List type.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookingEvent>().Property(p => p.Participants)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
        }
    }
}
