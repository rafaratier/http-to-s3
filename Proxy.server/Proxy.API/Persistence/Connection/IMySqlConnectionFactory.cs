using MySql.Data.MySqlClient;

namespace Proxy.API.Persistence;

public interface IMySqlConnectionFactory
{
    MySqlConnection Create();
}