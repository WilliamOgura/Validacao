﻿using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fiap.Exemplo02.MVC.Web.Models;
using System.Data.Entity;
using Fiap.Exemplo02.MVC.Web.UnitsOfWork;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class AlunoController : Controller
    {
        //private PortalContext ctx = new PortalContext();
        private UnitOfWork _unit = new UnitOfWork();

        #region GET

        [HttpGet]
        public ActionResult Cadastrar()
        {
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");

            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            CarregarComboGrupo();

            //include -> busca o relacionamento (preenche o grupo que o aluno possui), faz o join
            //var lista = ctx.Aluno.Include("Grupo").ToList();
            var lista = _unit.AlunoRepository.Listar();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Alterar(int id)
        {
            //SelectList
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");

            Aluno a = _unit.AlunoRepository.BuscarPorId(id);
            return View(a);
        }

        [HttpGet]
        public ActionResult Buscar(int? idGrupo, string nomeBusca) //permite ser Null
        {
            List<Aluno> resultado = new List<Aluno>();

            if (idGrupo == null)
            {
                //busca o aluno no banco por parte do nome
                resultado = _unit.AlunoRepository.BuscarPor(a => a.Nome.Contains(nomeBusca)).ToList();
            }
            else
            {
                //busca o aluno no banco por nome do grupo
                resultado = _unit.AlunoRepository.BuscarPor(a => a.Grupo.Id == idGrupo && a.Nome.Contains(nomeBusca)).ToList();
            }

            CarregarComboGrupo();

            //passo direto para a view de listar e não para a action
            return View("Listar", resultado);
        }

        #endregion

        #region POST
        [HttpPost]
        public ActionResult Cadastrar(Aluno aluno)
        {
            _unit.AlunoRepository.Cadastrar(aluno);
            _unit.Save();

            ViewBag.msg = "Cadastrado com sucesso";
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");
            return View();
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
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");

            _unit.AlunoRepository.Atualizar(a);
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