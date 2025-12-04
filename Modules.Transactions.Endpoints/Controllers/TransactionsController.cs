using Microsoft.AspNetCore.Mvc;

namespace Modules.Transactions.Endpoints.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetTrans()
        {
            return Ok();
        }
    }
}
