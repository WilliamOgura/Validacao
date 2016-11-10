using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Exemplo02.MVC.Web.Repositories
{
    public class GrupoRepository : IRepository<Grupo>
    {
        private PortalContext _context;

        public GrupoRepository(PortalContext context)
        {
            _context = context;
        }

        public void Atualizar(Grupo grupo)
        {
            _context.Entry(grupo).State = EntityState.Modified;
            _context.Entry(grupo.Projeto).State = EntityState.Modified;
        }

        public ICollection<Grupo> BuscarPor(Expression<Func<Grupo, bool>> filtro)
        {
            List<Grupo> resultado = _context.Grupo.Where(filtro).ToList();
            return resultado;
        }

        public Grupo BuscarPorId(int id)
        {
            return _context.Grupo.Find(id);
        }

        public void Cadastrar(Grupo grupo)
        {
            _context.Grupo.Add(grupo);
        }

        public ICollection<Grupo> Listar()
        {
            return _context.Grupo.ToList();
        }

        public void Remover(int id)
        {
            Grupo g = _context.Grupo.Find(id);
            _context.Projeto.Remove(g.Projeto);
            _context.Grupo.Remove(g);
        }
    }
}
