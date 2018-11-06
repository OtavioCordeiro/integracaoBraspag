using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegracaoBraspag.Contracts
{
    public class Payment
    {
        public Payment()
        {
            this.CreditCard = new CreditCard();
        }

        public string Provider { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public bool Capture { get; set; }
        public int Installments { get; set; }
        public CreditCard CreditCard { get; set; }
        public int ServiceTaxAmount { get; set; }
        public string Interest { get; set; }
        public bool Authenticate { get; set; }
        public bool Recurrent { get; set; }
        public string ProofOfSale { get; set; }
        public string AcquirerTransactionId { get; set; }
        public string AuthorizationCode { get; set; }
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