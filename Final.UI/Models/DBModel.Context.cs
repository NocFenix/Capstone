﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final.UI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CapstoneEntities : DbContext
    {
        public CapstoneEntities()
            : base("name=CapstoneEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Availibility> Availibilities { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<UserShift> UserShifts { get; set; }
        public virtual DbSet<DayShift> DayShifts { get; set; }
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
    }
}