using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegracaoBraspag.Contracts
{
    public class CreditCard
    {
        [Required(ErrorMessage = "O numero do cartão deve ser preenchido")]
        [StringLength(16, MinimumLength = 14, ErrorMessage = "O numero do cartão deve ter 14 ou 16 dígitos")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "O nome do cartão deve ser preenchido")]
        public string Holder { get; set; }
        [RegularExpression("([0-9]{2}[/][0-9]{4})", ErrorMessage = "A data de expiração deve esta no seguinte padrão MM/YYYY")]
        [StringLength(7)]
        [Required(ErrorMessage = "A data de expiração é obrigatória")]
        public string ExpirationDate { get; set; }
        [Required(ErrorMessage = "O código de segurança do cartão deve ser preenchido")]
        [StringLength(3, ErrorMessage = "A código de segurança deve ter no máximo 3 caracteres")]
        public string SecurityCode { get; set; }
        public string Brand { get; set; }
    }
}