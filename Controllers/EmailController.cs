using EmailProject.DataLayer.Entities;
using EmailProject.DataLayer.Model;
using EmailProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmailProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("GetEmails")]
        public IActionResult Get()
        {
            var list = _emailService.Get();

            return Ok(list);
        }

        [HttpPost("AddEmail")]
        public IActionResult Post(EmailModel model)
        {
            var list = _emailService.Post(model);

            return StatusCode((int)list.StatusCode, list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var list = _emailService.Delete(id);

            return Ok(list);
        }
    }
}
