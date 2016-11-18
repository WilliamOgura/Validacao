using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fiap.Exemplo02.MVC.Web.ViewModels
{
    public class AlunoViewModel
    {
        //Opções do Select
        public SelectList ListaGrupo { get; set; }

        public string Mensagem { get; set; }

        #region Aluno Properties

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Nullable<double> Desconto { get; set; }
        public bool Bolsa { get; set; }
        [Display(Name="Grupo")]
        public int GrupoId { get; set; }
        #endregion
    }

}
