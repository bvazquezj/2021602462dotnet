using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApplication1.utils;

namespace WebApplication1;

public class SchoolDataContextFactory : IDesignTimeDbContextFactory<SchoolDataContext>
{
    public SchoolDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SchoolDataContext>();
        var connectionString = "Server=localhost;Database=school;User=root;Password=Dayamoon12;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new SchoolDataContext(optionsBuilder.Options);
    }
}