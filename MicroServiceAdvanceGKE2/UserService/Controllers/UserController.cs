using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            SeedValues();
            string cs = Startup.connectionstring;
            object user;
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

            return user;
        }

        private  void SeedValues()
        {
            try
            {
                string cs = Startup.connectionstring;
                using (var con = new MySqlConnection(cs) 
                {
                    
                })
                {
                    con.Open();
                    var stm = "SELECT table_name FROM information_schema.tables WHERE table_name = 'POCusers'; ";
                    var cmd = new MySqlCommand(stm, con);
                    var data = cmd.ExecuteReader();
                    data.Read();
                    if (!data.HasRows)
                    {
                        data.Close();
                        cmd.CommandText =
                            @$"CREATE TABLE `POCusers` (
                        `id` int PRIMARY KEY,
                        `name` varchar(45),
                        `age` int,
                        `email` varchar(100)
                        );
                        INSERT INTO `sys`.`POCusers`
                        (`id`, `name`, `age`, `email`) 
                        VALUES ('1', 'Kunal Verma', '25', 'kunal.verma@nagarro.com');";
                        cmd.ExecuteNonQuery();
                    };
                    con.Close();
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
