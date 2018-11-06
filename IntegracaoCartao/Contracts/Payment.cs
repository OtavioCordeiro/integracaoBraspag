using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegracaoCartao.Contracts
{
    public class Payment
    {
        public Payment()
        {
            this.CreditCard = new CreditCard();
        }

        public string Provider { get; set; }
        public string Type { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        [Display(Name = "Valor da transação")]
        public int Amount { get; set; }
        public bool Capture { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "A parcela deve ser maior que zero")]
        [Display(Name = "Parcelas")]
        public int Installments { get; set; }
        public CreditCard CreditCard { get; set; }
        public int ServiceTaxAmount { get; set; }
        public string Interest { get; set; }
        public bool Authenticate { get; set; }
        public bool Recurrent { get; set; }
        public string ProofOfSale { get; set; }
        public string AcquirerTransactionId { get; set; }
        public string AuthorizationCode { get; set; }
        [Display(Name = "Identificador da transação")]
        public Guid PaymentId { get; set; }
        public string ReceivedDate { get; set; }
        public int CapturedAmount { get; set; }
        public string CapturedDate { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public int Status { get; set; }
        public string ProviderReturnCode { get; set; }
        public string ProviderReturnMessage { get; set; }
        public List<Link> Links { get; set; }
    }
}