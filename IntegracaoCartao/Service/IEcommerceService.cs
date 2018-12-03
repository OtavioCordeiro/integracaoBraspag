using IntegracaoCartao.Contracts.Request;
using IntegracaoCartao.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoCartao.Service
{
    public interface IEcommerceService
    {
        bool CreateTransaction(CreateTransactionRequest createTransactionRequest, dynamic viewBag);

        void CaptureTransaction(OperationTransactionRequest operationTransactionRequest, dynamic viewBag);

        void VoidTransaction(OperationTransactionRequest operationTransactionRequest, dynamic viewBag);

        void QueryTransaction(OperationTransactionRequest operationTransactionRequest, dynamic viewBag);
    }
}
