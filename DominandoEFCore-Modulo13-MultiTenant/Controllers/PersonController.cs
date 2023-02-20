using DominandoEFCore_Modulo13_MultiTenant.Data;
using DominandoEFCore_Modulo13_MultiTenant.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DominandoEFCore_Modulo13_MultiTenant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get([FromServices] ApplicationContext db) // passando o servico de do context na chamada Get com o [FromServices] 
        {
            var people = db.People.ToArray();

            return people;
        }
    }
}