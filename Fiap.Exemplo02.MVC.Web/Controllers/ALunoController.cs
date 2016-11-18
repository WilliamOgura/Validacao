using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Fiap.Exemplo02.MVC.Web.UnitsOfWork;
using Fiap.Exemplo02.MVC.Web.ViewModels;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class AlunoController : Controller
    {
        //private PortalContext ctx = new PortalContext();
        private UnitOfWork _unit = new UnitOfWork();

        #region GET

        [HttpGet]
        public ActionResult Cadastrar(string msg)
        {
            var viewModel = new AlunoViewModel();
            viewModel.ListaGrupo = ListarGrupos();


            List<Professor> lista = (List<Professor>)_unit.ProfessorRepository.Listar();
            ViewBag.professores = lista;

            return View(viewModel);
        }

        private SelectList ListarGrupos()
        {
           var grupos = _unit.GrupoRepository.Listar();
           return new SelectList(grupos, "Id", "Nome");
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var viewModel = new AlunoViewModel();

            //include -> busca o relacionamento (preenche o grupo que o aluno possui), faz o join
            //var lista = ctx.Aluno.Include("Grupo").ToList();
            viewModel.Alunos = _unit.AlunoRepository.Listar();
            viewModel.ListaGrupo = ListarGrupos();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Alterar(int id)
        {
            //SelectList buscar objeto (aluno  ) no banco
           var aluno = _unit.AlunoRepository.BuscarPorId(id);
            var viewModel = new AlunoViewModel()
            {
                ListaGrupo = ListarGrupos(),
                Nome = aluno.Nome,
                Bolsa = aluno.Bolsa,
                Desconto = aluno.Desconto,
                Id = aluno.Id,
                GrupoId = aluno.GrupoId,
                DataNascimento = aluno.DataNascimento,
            };
                return View(viewModel);
        }

        [HttpGet]
        public ActionResult Buscar(int? idBusca, string nomeBusca) //permite ser Null
        {
            List<Aluno> resultado = new List<Aluno>();
            var lista = _unit.AlunoRepository.BuscarPor(a =>a.Nome.Contains(nomeBusca) && (a.GrupoId == idBusca|| idBusca == null));
        
            var viewModel = new AlunoViewModel()
            {
                ListaGrupo = ListarGrupos(),
                Alunos = lista
            };

          

            CarregarComboGrupo();

            //passo direto para a view de listar e não para a action
            return View("Listar", viewModel);
        }

        #endregion

        #region POST
        [HttpPost]
        public ActionResult Cadastrar(AlunoViewModel alunoViewModel, int[] ProfessoresId)
        {
            /*   foreach(var id in ProfessoresId)
               {
                   Professor prof = _unit.ProfessorRepository.BuscarPorId(id);
                   aluno.Professor.Add(prof);
               }
               */
            if (ModelState.IsValid)
            {
                var aluno = new Aluno()
                {
                    Id = alunoViewModel.Id,
                    Nome = alunoViewModel.Nome,
                    DataNascimento = alunoViewModel.DataNascimento,
                    Desconto = alunoViewModel.Desconto,
                    Bolsa = alunoViewModel.Bolsa,
                    GrupoId = alunoViewModel.GrupoId,


                };
                _unit.AlunoRepository.Cadastrar(aluno);
                _unit.Save();

                List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
                ViewBag.grupos = new SelectList(grupos, "Id", "Nome");
                return RedirectToAction("Cadastrar", new { msg = "ALUNO Cadastrado com sucesso" });
            }else
            {
                alunoViewModel.ListaGrupo = ListarGrupos();
                return View(alunoViewModel);
            }
            //vou adicionar os options manualmente
           /* List<Professor> lista = (List<Professor>)_unit.ProfessorRepository.Listar();
            ViewBag.professores = lista;
            
            ViewBag.msg = "Cadastrado com sucesso " + ProfessoresId.Count();
            */
            
        }

       

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            _unit.AlunoRepository.Remover(id);
            _unit.Save();
            TempData["msg"] = "Aluno excluído com sucesso";
            return RedirectToAction("Listar");
        }

       

        [HttpPost]
        public ActionResult Alterar(Aluno a)
        {
            //SelectList   
            _unit.AlunoRepository.Alterar(a);
            _unit.Save();
            ViewBag.msg = "Alterado com sucesso";
            return View(a);
        }

        #endregion

        #region PRIVATE

        private void CarregarComboGrupo()
        {
            //envia grupos para o select
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");
        }

        #endregion

        #region DISPOSE
        //sobrescrever o método do Dispose do COntroller
        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }

        #endregion

    }
}