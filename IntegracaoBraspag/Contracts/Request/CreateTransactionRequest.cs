using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegracaoBraspag.Contracts.Request
{
    public class CreateTransactionRequest
    {
        public CreateTransactionRequest()
        {
            this.Payment = new Payment();
            this.Payment.Provider = "Simulado";
            this.Customer = new Customer();

        }

        [Required(ErrorMessage = "O numero do pedido é obrigatório")]
        [Display(Name = "Numero do pedido")]
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}