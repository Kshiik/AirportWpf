﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirportWpf.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AIRPORTEntities1 : DbContext
    {
        public AIRPORTEntities1()
            : base("name=AIRPORTEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Airline> Airline { get; set; }
        public virtual DbSet<Airport> Airport { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Ticket_Passenger> Ticket_Passenger { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
