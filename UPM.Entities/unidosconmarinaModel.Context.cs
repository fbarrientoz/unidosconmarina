﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UPM.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UnidosconmarinaEntities : DbContext
    {
        public UnidosconmarinaEntities()
            : base("name=UnidosconmarinaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<HistorialContacto> HistorialContactoes { get; set; }
        public virtual DbSet<Noticia> Noticias { get; set; }
        public virtual DbSet<Padron> Padrons { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<TipoVotante> TipoVotantes { get; set; }
    }
}
