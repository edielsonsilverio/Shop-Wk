using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shop.WebApi.Data;

public class ApplicationDbContext : IdentityDbContext
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //var conn = "Server = 127.0.0.1;Port=3306; Database = shop_test; Uid = root; Pwd = estadao";
        //optionsBuilder.UseMySql(ServerVersion.AutoDetect(conn));
    }
}

/*
    Add-Migration TabelaIniciais -Project src\Web\Shop.WebApi -Context ApplicationDbContext -StartupProject src\Web\Shop.WebApi
    Add-Migration TabelaIniciais -Project src\Web\Shop.WebApi -Context ApplicationDbContext -StartupProject src\Web\Shop.WebApi

    Update-Database -StartupProject src\Web\Shop.WebApi -Project src\Web\Shop.WebApi -Context ApplicationDbContext
*/