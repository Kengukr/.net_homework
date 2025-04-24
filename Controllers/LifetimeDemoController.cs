using Microsoft.AspNetCore.Mvc;
using task_new.Services.LifetimeExamples;

namespace task_new.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifetimeDemoController : ControllerBase
    {
        private readonly ITransientService _transient1;
        private readonly ITransientService _transient2;
        private readonly IScopedService _scoped1;
        private readonly IScopedService _scoped2;
        private readonly ISingletonService _singleton1;
        private readonly ISingletonService _singleton2;

        public LifetimeDemoController(
            ITransientService transient1,
            ITransientService transient2,
            IScopedService scoped1,
            IScopedService scoped2,
            ISingletonService singleton1,
            ISingletonService singleton2)
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new
            {
                Transient1Id = _transient1.Id,
                Transient2Id = _transient2.Id,
                TransientAreSame = _transient1.Id == _transient2.Id,

                Scoped1Id = _scoped1.Id,
                Scoped2Id = _scoped2.Id,
                ScopedAreSame = _scoped1.Id == _scoped2.Id,

                Singleton1Id = _singleton1.Id,
                Singleton2Id = _singleton2.Id,
                SingletonAreSame = _singleton1.Id == _singleton2.Id
            };

            return Ok(result);
        }
    }
}