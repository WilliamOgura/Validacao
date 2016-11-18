using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fiap.Exemplo02.MVC.Web.Models;
using System.Data.Entity;

namespace Fiap.Exemplo02.MVC.Web.Repositories
{
    class AlunoRepository : IRepository<Aluno>
    {

        private PortalContext _context;

        public AlunoRepository(PortalContext context)
        {
            _context = context;
        }

        public void Alterar(Aluno aluno)
        {
           _context.Entry(aluno).State = EntityState.Modified;
        }

        public ICollection<Aluno> BuscarPor(Expression<Func<Aluno, bool>> filtro)
        {
            List<Aluno> resultado = _context.Aluno.Where(filtro).ToList();
            return resultado;
        }

        public Aluno BuscarPorId(int id)
        {
            return _context.Aluno.Find(id);
        }

        public void Cadastrar(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
        }

        public ICollection<Aluno> Listar()
        {
            return _context.Aluno.Include("Grupo").ToList();
        }

        public void Remover(int id)
        {
            _context.Aluno.Remove(_context.Aluno.Find(id));
        }
    }
}
