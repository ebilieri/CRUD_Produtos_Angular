using Microsoft.AspNetCore.Mvc;

namespace QuickBuy.Angular.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class UsuarioController: ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }
}
