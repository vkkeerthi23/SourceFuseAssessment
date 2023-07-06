using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SourceFuseAssessment.Interfaces;

namespace SourceFuseAssessment.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class TokenController : Controller
    {

        public ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult GenerateToken()
        {
            var token = _tokenService.GenerateToken();
            return Ok(token);

        }
    }
}
