﻿using Elearn_temp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Elearn_temp.Data
{
    public class AppDbContext:IdentityDbContext<AppUser> // IdentityDbContext noralda dbcontectden miras alir bunu yazanda 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseImage> CourseImages { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // bunu mutleq sekilde yaziriqki IdentityDbContext  de olan kodlarimizi oxusun
        }
    }
}
