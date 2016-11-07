using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class GrupoController : Controller
    {
        private PortalContext ctx = new PortalContext();

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Grupo grupo)
        {
            ctx.Grupo.Add(grupo);
            //ctx.Projeto.Add(projeto);
            ctx.SaveChanges();
            TempData["msg"] = "Cadastro efetuado com sucesso";
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            List<Grupo> grupos = ctx.Grupo.ToList();
            return View(grupos);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Grupo grupo = ctx.Grupo.Find(id);
            return View(grupo);
        }

        [HttpPost]
        public ActionResult Editar(Grupo g)
        {
            ctx.Entry(g).State = EntityState.Modified;
            ctx.Entry(g.Projeto).State = EntityState.Modified;
            ctx.SaveChanges();
            TempData["msg"] = "Alterado com sucesso";
            return View(g);
        }

        public ActionResult Excluir(int idGrupo)
        {
            Grupo g = ctx.Grupo.Find(idGrupo);
            ctx.Projeto.Remove(g.Projeto);
            ctx.Grupo.Remove(g);
            ctx.SaveChanges();
            return RedirectToAction("Listar");
        }
    }

   
}