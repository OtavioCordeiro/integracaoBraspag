using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntegracaoCartao.Validation
{
    public class ValidationModel : IValidation
    {
        public bool ValidationModelState(ModelStateDictionary model)
        {
            return model.IsValid;
        }
    }
}