﻿using System;
using Microsoft.Data.Entity;

namespace KatanaMUD.Models
{
	public class GameContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptions options)
		{
			options.UseSqlServer(@"Server=localhost;Database=KatanaMUD;integrated security=True;");
		}

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<ArmorType> ArmorTypes { get; set; }
        public virtual DbSet<ClassTemplate> ClassTemplates { get; set; }
        public virtual DbSet<ClassTemplateArmorType> ClassTemplateArmorTypes { get; set; }
        public virtual DbSet<ClassTemplateWeaponType> ClassTemplateWeaponTypes { get; set; }
        public virtual DbSet<RaceTemplate> RaceTemplates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeaponType> WeaponTypes { get; set; }
    }
}