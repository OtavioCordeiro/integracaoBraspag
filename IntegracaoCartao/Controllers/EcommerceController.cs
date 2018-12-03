using IntegracaoCartao.Contracts.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using IntegracaoCartao.Contracts.Response;
using IntegracaoCartao.Validation;
using IntegracaoCartao.Service;

namespace IntegracaoCartao.Controllers
{
    public class EcommerceController : Controller
    {
        IValidation _validation;
        IEcommerceService _ecommerceService;

        public EcommerceController()
        {
            _validation = new ValidationModel();
            _ecommerceService = new EcommerceService();
        }

        // GET: Ecommerce
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTransaction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTransaction(CreateTransactionRequest request)
        {
            if (_validation.ValidationModelState(ModelState))
            {

                bool success = _ecommerceService.CreateTransaction(request, ViewBag);

                if (success)
                {
                    return View("OperationTransaction");
                }
                else
                {
                    return View("Falha");
                }
            }
            else
            {
                return View(request);
            }
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
                    _ecommerceService.QueryTransaction(request, ModelState);
                    break;

                case Contracts.Request.OperationTransaction.Capture:
                    _ecommerceService.CaptureTransaction(request, ModelState);
                    break;

                case Contracts.Request.OperationTransaction.Void:
                    _ecommerceService.VoidTransaction(request, ModelState);
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