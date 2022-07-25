using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive.Mvc;

namespace Controllers
{
    [Route("Factor")]
    public class FactorController : BaseController
    {
        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult> Index(FactorViewModel info)
        {
            return View("Factor/Index",info);
        }
    }

    public partial class FactorViewModel : IViewModel
    {
        [ValidateNever]
        public Order order { get; set; }

    }
}