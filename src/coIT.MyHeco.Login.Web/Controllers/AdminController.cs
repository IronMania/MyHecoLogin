using System.Linq;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IMyHecoRepository _myHecoRepository;

        public AdminController(IMyHecoRepository myHecoRepository)
        {
            _myHecoRepository = myHecoRepository;
        }

        // GET api/values
        [HttpGet("Users")]
        public IActionResult GetAll()
        {    
            var benutzer = _myHecoRepository.All().OfType<NichtAktivierterBenutzer>();
            return Json(benutzer.Select(siren => new {email = siren.Email, code = siren.AktivierungsCode}));
            
        }
    }
}