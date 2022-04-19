using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using VR_World_API_Models.Identity;

namespace VR_World_API.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string connectionString = "server=localhost;database=vr_world_dev;username=root;password=1234";

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                string sql = "SELECT * FROM user;";
                var users = mySqlConnection.Query<User>(sql);
                mySqlConnection.Close();
                return users;
            }
        }

        [HttpPost("PostUser")]
        public int PostUser(User user)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                string sql = $"INSERT INTO USER(firstName, lastName, email, phone, address, username, password) " +
                    $"VALUES('{user.firstName}','{user.lastName}','{user.email}','{user.phone}','{user.address}','{user.username}','{user.password}')";
                var NoOfRowsInserted = mySqlConnection.ExecuteScalar<int>(sql);
                mySqlConnection.Close();
                return NoOfRowsInserted;
            }
        }
    }
}
