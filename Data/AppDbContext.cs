using Microsoft.EntityFrameworkCore;

namespace ProjetoFoodCom.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
}