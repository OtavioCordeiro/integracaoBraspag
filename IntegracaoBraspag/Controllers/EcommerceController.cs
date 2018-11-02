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
            return View();
        }

        [HttpPost]
        public ActionResult CreateTransaction(CreateTransactionRequest request)
        {
            string jsonRequest = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonRequest, UnicodeEncoding.UTF8, "application/json");

            string transactionUrl = ConfigurationManager.AppSettings["TransactionUrl"];
            string requestUri = string.Concat(transactionUrl, "/v2/sales");

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("MerchantId", "a44734dd-92e1-4031-8479-c07d4b299474");
            httpClient.DefaultRequestHeaders.Add("MerchantKey", "NSPAUODNTWWRUTDBOKQJXLIBPWBROCQMUDYIRMHW");

            var response = httpClient.PostAsync(requestUri, stringContent).Result;
            var contents = response.Content.ReadAsStringAsync().Result;
            CreateTransactionResponse createTransactionResponse = JsonConvert.DeserializeObject<CreateTransactionResponse>(contents);

            return View();
        }

        [HttpGet]
        public ActionResult Teste()
        {
            return View();
        }
    }
}