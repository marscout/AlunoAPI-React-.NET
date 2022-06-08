using AlunoApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunoApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Aluno> Alunos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Aluno>().HasData(new Aluno
        //    {
        //        Id = 1,
        //        Nome = "Mario",
        //        Email = "mario@alunoApi.com",
        //        Idade = 22
        //    }, new Aluno
        //    {
        //        Id = 2,
        //        Nome = "Roberto",
        //        Email = "roberto@alunoApi.com",
        //        Idade = 25
        //    });
        //}
    }
}
