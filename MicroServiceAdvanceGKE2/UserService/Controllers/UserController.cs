using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("/")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("/user/{id}")]
        public object Get(string id = "1")
        {
            object user;
            try
            {
                string cs = Startup.connectionstring;
                using (var con = new MySqlConnection(cs))
                {
                    con.Open();
                    var stm = "SELECT * from sys.POCusers";
                    var cmd = new MySqlCommand(stm, con);
                    var data = cmd.ExecuteReader();
                    data.Read();
                    user = new
                    {
                        id = data[0],
                        name = data[1],
                        age = data[2],
                        email = data[3]
                    };
                    con.Close();
                }
            }
            catch (System.Exception e)
            {
                string err = "DB Error or no user ..";
                if (e != null && !string.IsNullOrEmpty(e.Message))
                {
                    err += e.Message;
                }
                user = new { error = err };
            }

            return user;
        }

        [HttpGet]
        public string Get()
        {
            return $"{nameof(UserController)}.{nameof(Get)} Healthy \n cv:{Startup.configVersion}";
        }

    }
}
