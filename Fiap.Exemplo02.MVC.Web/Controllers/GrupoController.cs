using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
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
    }

   
}