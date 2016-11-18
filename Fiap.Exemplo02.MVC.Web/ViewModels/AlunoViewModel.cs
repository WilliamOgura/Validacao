using Fiap.Exemplo02.MVC.Web.Models;
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

        #region Lista properties
        public ICollection<Aluno> Alunos { get; set; }
        public string NomeBusca { get; set; }
        public int IdBusca { get; set; }
        #endregion

        #region Aluno Properties

        public int Id { get; set; }

        [Required(ErrorMessage ="Nome é Obrigatório!")]
        [StringLength(100, ErrorMessage ="Máximo de 100 caracteres")]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Nullable<double> Desconto { get; set; }
        public bool Bolsa { get; set; }
        [Display(Name="Grupo")]
        public int GrupoId { get; set; }
        #endregion
    }

}
