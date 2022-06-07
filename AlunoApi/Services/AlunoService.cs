using AlunoApi.Context;
using AlunoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;
        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {//AsNoTracking
            var result = await _context.Alunos.ToListAsync();
            return result;
        }
        public async Task<Aluno> GetAluno(int id)
        {
            var result = await _context.Alunos.FindAsync(id);
            return result;
        }
        public async Task<IEnumerable<Aluno>> GetAlunoByNome(string Nome)
        {
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrEmpty(Nome))
            {
                alunos = await _context.Alunos.Where(x => x.Nome.Contains(Nome)).ToListAsync();

            }
            else
            {
                alunos = await GetAlunos();
            }
            return alunos;
        }
        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            if(aluno != null)
            {
                _context.Remove(aluno);
            }
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
