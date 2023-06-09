using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace APIMDS
{
    public class ChatDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21)));

            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Personnage> Personnages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Univers> Univers { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Author)
                .WithMany()
                .HasForeignKey(m => m.AuthorId);

            modelBuilder.Entity<Univers>()
                .Property(u => u.Nom)
                .IsRequired()
                .HasMaxLength(255); // Ajustez la taille en fonction de vos besoins

            modelBuilder.Entity<Personnage>()
                .HasOne(p => p.Univers)
                .WithMany(u => u.Personnages)
                .HasForeignKey(p => p.UniversId);
        }

        public ChatDbContext(DbContextOptions<ChatDbContext> options, IConfiguration configuration)
    : base(options)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    }
}
