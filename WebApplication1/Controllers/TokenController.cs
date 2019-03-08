using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    [Route("oauth2/token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        public class AuthModel
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
        }
        
        [HttpPost]
        public IActionResult Create([FromForm]AuthModel inputModel)
        {
            if (inputModel.client_id != "james" && inputModel.client_secret != "bond")
                return Unauthorized();

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("oPWBUWt3EwqdgKWZzsitLi+JsU+Vw/dFIg/NYSOiWbI="))
                                .AddSubject("james bond")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "123")
                                .AddExpiry(1)
                                .Build();
            
            return new JsonResult(new { access_token = token.Value, validTo = token.ValidTo.ToUniversalTime() });
        }
    }
}
