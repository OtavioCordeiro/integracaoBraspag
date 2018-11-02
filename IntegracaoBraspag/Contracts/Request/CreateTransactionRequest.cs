using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegracaoBraspag.Contracts.Request
{
    public class CreateTransactionRequest
    {
        public CreateTransactionRequest()
        {
            Payment = new Payment();
            Payment.Provider = "Simulado";
        }
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}