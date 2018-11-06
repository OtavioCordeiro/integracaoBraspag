using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegracaoCartao.Contracts
{
    public class Customer
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [Display(Name = "Cliente")]
        public string Name { get; set; }
    }
}