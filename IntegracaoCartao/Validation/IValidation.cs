using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IntegracaoCartao.Validation
{
    public interface IValidation
    {
        bool ValidationModelState(ModelStateDictionary model);
    }
}
