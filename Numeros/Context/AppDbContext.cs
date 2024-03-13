using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Numeros.Models;

namespace Numeros.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // CONTRUCTOR IN    ICIALIZAR EL CONTEXTO A LA BASE DE DATOS Y OPERAR CON LA BASE DE DATOS INYECCION DE DEPENDENCIAS.
        {

        }
        public DbSet<SumNumber> Numbers { get; set; }
    }
}
