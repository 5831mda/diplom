using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

using PersonalSystemContol.Models;

namespace PersonalSystemContol.DataBase
{
    /// <summary>
    /// Контекст данных.
    /// </summary>
    public class BaseContext : DbContext
    {
        /// <summary>
        /// Таблица с инженерами.
        /// </summary>
        public DbSet<Engineer> Engineers { get; set; }

        /// <summary>
        /// Таблица с фотографиями.
        /// </summary>
        public DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// Таблица с отчетами.
        /// </summary>
        public DbSet<Report> Reports { get; set; }

        /// <summary>
        /// Таблица с заданиями.
        /// </summary>
        public DbSet<Task> Tasks { get; set; }


        public BaseContext(DbContextOptions<BaseContext> options) 
            :base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
