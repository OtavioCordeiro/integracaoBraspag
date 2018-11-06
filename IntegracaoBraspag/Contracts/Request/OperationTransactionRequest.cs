using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegracaoBraspag.Contracts.Request
{
    public class OperationTransactionRequest
    {
        public OperationTransaction Operation { get; set; }

        [Display(Name = "Identificador da transação")]
        public Guid PaymentId { get; set; }
    }

    public enum OperationTransaction
    {
        Query,
        Capture,
        Void
    }
}