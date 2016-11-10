using Fiap.Exemplo02.MVC.Web.Models;
using Fiap.Exemplo02.MVC.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Exemplo02.MVC.Web.UnitsOfWork
{
    public class UnitOfWork : IDisposable
    {
        private PortalContext _context = new PortalContext();

        private IRepository<Aluno> _alunoRepository;
        private IRepository<Grupo> _grupoRepository;

        public IRepository<Grupo> GrupoRepository
        {
            get{
                if (_grupoRepository == null)
                {
                    _grupoRepository = new GrupoRepository(_context);
                }
                return _grupoRepository;
            }  
        }


        public IRepository<Aluno> AlunoRepository
        {
            get {
                if(_alunoRepository == null)
                {
                    _alunoRepository = new AlunoRepository(_context);
                }
                return _alunoRepository;
            }
        }


        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);//manda o GarbageCollector destruir esse objeto
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
