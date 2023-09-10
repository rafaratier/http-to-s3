using MySql.Data.MySqlClient;

namespace Proxy.API.Persistence.Connection;

public class MySqlConnectionFactory : IMySqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public MySqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MySqlConnection Create()
    {
        return new MySqlConnection(
            _configuration.GetConnectionString("Mysql.Connection.String"));
    }
}