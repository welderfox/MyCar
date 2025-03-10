using Microsoft.EntityFrameworkCore;

namespace MyCar.Context.Configurations
{
    public class MyCarContext
    {
        //public string ConnectionString { get; set; } = null!;

        //public string DatabaseName { get; set; } = null!;

        //public string BooksCollectionName { get; set; } = null!;

        public MyCarContext(DbContextOptions<MyCarContext> options) : base(options)
        {
        }

        #region TABELAS

        //TODO: ADICIONAR NOVAS TABELAS

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new CadCategoriaItemConfiguration());

            //Habilita o comportamento Legacy Timestamp para gravar sem UTC
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
