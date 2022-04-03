using FlightAgency.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var instanceConnectionName = Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");
        if (instanceConnectionName is not null)
        {
            var dbSocketDir = "/cloudsql";
            var connection = new MySqlConnectionStringBuilder()
            {
                SslMode = MySqlSslMode.None,
                Server = String.Format("{0}/{1}", dbSocketDir, instanceConnectionName),
                UserID = Environment.GetEnvironmentVariable("DB_USER"),
                Password = Environment.GetEnvironmentVariable("DB_PASS"),
                Database = Environment.GetEnvironmentVariable("DB_NAME"),
                ConnectionProtocol = MySqlConnectionProtocol.UnixSocket,
                Pooling = true,
            };

            var connectionString = connection.ConnectionString;
            var version = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, version)
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
        }
        else
        {
            optionsBuilder
                .UseSqlite("Data Source=FlightAgency.db")
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}