using IntegracaoBraspag.Contracts.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using IntegracaoBraspag.Contracts.Response;

namespace IntegracaoBraspag.Controllers
{
    public class EcommerceController : Controller
    {
        // GET: Ecommerce
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTransaction()
        {
            CreateTransactionRequest request = new CreateTransactionRequest();
            request.Customer.Name = "Otavio teste";
            request.MerchantOrderId = "12333";
            request.Payment.Amount = 1565;
            request.Payment.Capture = false;
            request.Payment.Installments = 1;
            request.Payment.Type = "CreditCard";
            request.Payment.CreditCard.SecurityCode = "123";
            request.Payment.CreditCard.CardNumber = "1234123412341234";
            request.Payment.CreditCard.Holder = "Otavio Lopes";
            request.Payment.CreditCard.Brand = "Visa";
            request.Payment.CreditCard.ExpirationDate = "02/2019";

            return View(request);
        }

        [HttpPost]
        public ActionResult CreateTransaction(CreateTransactionRequest request)
        {
            if (ModelState.IsValid)
            {

                string jsonRequest = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonRequest, UnicodeEncoding.UTF8, "application/json");

                string transactionUrl = ConfigurationManager.AppSettings["TransactionUrl"];
                string requestUri = string.Concat(transactionUrl, "/v2/sales");

                var response = RequestHttp("Post", requestUri, stringContent);

                if (response?.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    CreateTransactionResponse createTransactionResponse = JsonConvert.DeserializeObject<CreateTransactionResponse>(contents);

                    ViewBag.PaymentId = createTransactionResponse.Payment.PaymentId;
                    ViewBag.ReturnMessage = "Transação criada com sucesso";

                    return View("OperationTransaction");

                }
                else
                {
                    return View("Falha");
                }
            }

            return View(request);
        }

        [HttpGet]
        public ActionResult OperationTransaction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OperationTransaction(OperationTransactionRequest request)
        {

            switch (request.Operation)
            {
                case Contracts.Request.OperationTransaction.Query:
                    string queryUrl = ConfigurationManager.AppSettings["QueryUrl"];
                    string queryUrn = string.Format("/v2/sales/{0}", request.PaymentId);
                    string queryUri = string.Concat(queryUrl, queryUrn);
                    var queryResponse = RequestHttp("Get", queryUri);
                    if (queryResponse.IsSuccessStatusCode)
                    {
                        ViewBag.ReturnMessage = "Transação consultada com sucesso";
                    }
                    break;

                case Contracts.Request.OperationTransaction.Capture:
                    string captureUrl = ConfigurationManager.AppSettings["TransactionUrl"];
                    string captureUrn = string.Format("/v2/sales/{0}/capture", request.PaymentId);
                    string captureUri = string.Concat(captureUrl, captureUrn);
                    var captureResponse = RequestHttp("Put", captureUri);
                    if (captureResponse.IsSuccessStatusCode)
                    {
                        ViewBag.ReturnMessage = "Transação capturada com sucesso";
                    }
                    break;

                case Contracts.Request.OperationTransaction.Void:
                    string voidUrl = ConfigurationManager.AppSettings["TransactionUrl"];
                    string voidUrn = string.Format("/v2/sales/{0}/void", request.PaymentId);
                    string voidUri = string.Concat(voidUrl, voidUrn);
                    var voidResponse = RequestHttp("Put", voidUri);
                    if (voidResponse.IsSuccessStatusCode)
                    {
                        ViewBag.ReturnMessage = "Transação cancelada com sucesso";
                    }
                    break;

                default:
                    break;
            }

            ViewBag.PaymentId = request.PaymentId;

            return View();
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