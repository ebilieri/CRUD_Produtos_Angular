using Microsoft.AspNetCore.Mvc;

namespace QuickBuy.Angular.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController: ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody]Usuario usuario)
        {
            if(usuario.Username == "usuario" && usuario.Password == "senha")
            {
                var result = new
                {
                    success = true
                };

                return Ok(result);

            }
            else
            {
                var result = new
                {
                    success = false,
                    error = "username or password incorrect"
                };
                return Ok(result);
            }
        }
    }

    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
