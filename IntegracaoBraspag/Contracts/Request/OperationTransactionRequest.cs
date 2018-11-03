using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegracaoBraspag.Contracts.Request
{
    public class OperationTransactionRequest
    {
        public OperationTransaction Operation { get; set; }

        public Guid PaymentId { get; set; }
    }

    public enum OperationTransaction
    {
        Query,
        Capture,
        Void
    }
}