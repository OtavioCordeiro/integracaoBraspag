using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using IntegracaoCartao.Contracts.Request;
using IntegracaoCartao.Contracts.Response;
using Newtonsoft.Json;

namespace IntegracaoCartao.Service
{
    public class EcommerceService : IEcommerceService
    {
        public void CaptureTransaction(OperationTransactionRequest operationTransactionRequest)
        {
            throw new NotImplementedException();
        }

        public bool CreateTransaction(CreateTransactionRequest createTransactionRequest, dynamic viewBag)
        {
            bool success = false;

            string jsonRequest = JsonConvert.SerializeObject(createTransactionRequest);
            StringContent stringContent = new StringContent(jsonRequest, UnicodeEncoding.UTF8, "application/json");

            string transactionUrl = ConfigurationManager.AppSettings["TransactionUrl"];
            string requestUri = string.Concat(transactionUrl, "/v2/sales");

            var response = RequestHttp("Post", requestUri, stringContent);

            var contents = response.Content?.ReadAsStringAsync().Result;

            viewBag.ServiceResponse = contents;

            if (response?.IsSuccessStatusCode == true)
            {
                CreateTransactionResponse createTransactionResponse = JsonConvert.DeserializeObject<CreateTransactionResponse>(contents);

                viewBag.PaymentId = createTransactionResponse.Payment.PaymentId;
                viewBag.ReturnMessage = "Transação criada com sucesso";

                success = true;
            }

            return success;
        }

        public void QueryTransaction(OperationTransactionRequest operationTransactionRequest)
        {
            throw new NotImplementedException();
        }

        public void VoidTransaction(OperationTransactionRequest operationTransactionRequest)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage RequestHttp(string method, string requestUri, StringContent stringContent = null)
        {

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("MerchantId", "a44734dd-92e1-4031-8479-c07d4b299474");
            httpClient.DefaultRequestHeaders.Add("MerchantKey", "NSPAUODNTWWRUTDBOKQJXLIBPWBROCQMUDYIRMHW");

            switch (method.ToUpper())
            {
                case "POST":
                    httpResponseMessage = httpClient.PostAsync(requestUri, stringContent).Result;
                    break;

                case "GET":
                    httpResponseMessage = httpClient.GetAsync(requestUri).Result;
                    break;

                case "PUT":
                    httpResponseMessage = httpClient.PutAsync(requestUri, stringContent).Result;
                    break;

                default:
                    throw new NotImplementedException("Metodo http não implementado");
            }

            return httpResponseMessage;

        }
    }
}