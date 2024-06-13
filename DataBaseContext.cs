using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace QuizProgram
{
    public class DataBaseContext: DbContext
    {
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ResultScore> Results { get; set; }

        static string connectionString = ConfigurationManager.ConnectionStrings["QuizProgram.Properties.Settings.QuizProgramConnectionString"].ConnectionString;

        public DataBaseContext() : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questions>().HasKey(c => c.Id);
            modelBuilder.Entity<Answers>().HasKey(c => c.Id);
            modelBuilder.Entity<Information>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasKey(c => c.Id);
            modelBuilder.Entity<ResultScore>().HasKey(c => c.Username);
        }
    }
}
