using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDataContext : DbContext {
       
        #region  Explicação da estrutura do DataContext
        /* Construtor da classe BlogDataContext que recebe as opções de configuração do DbContext.
         Utiliza o construtor da classe base (DbContext) para inicializar o contexto com essas opções.
        Isso permite configurar a conexão com o banco de dados, como o tipo de provedor (SQL Server, SQLite, etc),
        string de conexão e outras opções definidas externamente, geralmente no Startup.cs ou appsettings.json. */
        public BlogDataContext(DbContextOptions<BlogDataContext> options): base(options) {}
        #endregion
        
        #region Passando Entidades Para o DB 
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        #endregion
        
        #region Mapeamento Entidades do DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }
        #endregion
    }
}