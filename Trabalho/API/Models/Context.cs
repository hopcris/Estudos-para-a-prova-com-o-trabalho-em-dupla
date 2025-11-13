using System;
using Microsoft.EntityFrameworkCore;

namespace Larissa.Models;

public class Context : DbContext
{
    public DbSet<FolhaPagamento> FolhasPagamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Cristiano_Larissa.db");
    }
}
