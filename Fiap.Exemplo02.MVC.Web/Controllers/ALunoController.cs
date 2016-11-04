using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fiap.Exemplo02.MVC.Web.Models;
using System.Data.Entity;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class AlunoController : Controller
    {
        private PortalContext ctx = new PortalContext();

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Aluno aluno)
        {
            ctx.Aluno.Add(aluno);
            ctx.SaveChanges();
            ViewBag.msg = "Cadastrado com sucesso";
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var lista = ctx.Aluno.ToList();
            return View(lista);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            Aluno a = ctx.Aluno.Find(id);
            ctx.Aluno.Remove(a);
            ctx.SaveChanges();
            TempData["msg"] = "Aluno excluído com sucesso";
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public ActionResult Alterar(int id)
        {
            Aluno a = ctx.Aluno.Find(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult Alterar(Aluno a)
        {
            ctx.Entry(a).State = EntityState.Modified;
            ctx.SaveChanges();
            ViewBag.msg = "Alterado com sucesso";
            return View(a);
        }

        [HttpGet]
        public ActionResult Buscar(string nomeBusca)
        {
            //busca o aluno no banco por parte do nome
            List<Aluno> resultado = ctx.Aluno.Where(a => a.Nome.Contains(nomeBusca)).ToList();
            //passo direto para a view de listar e não para a action
            return View("Listar",resultado);
        }

    }
}