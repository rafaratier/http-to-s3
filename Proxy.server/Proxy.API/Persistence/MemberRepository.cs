using Dapper;
using MySql.Data.MySqlClient;
using Proxy.API.Models;

namespace Proxy.API.Persistence;

public class MemberRepository : IMemberRepository
{
    private readonly IMySqlConnectionFactory _connectionFactory;
    public MemberRepository(IMySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public Task<Member> GetMemberByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<LoginCredentials?> GetLoginCredentialsByEmailAsync(string email)
    {
        await using MySqlConnection mySqlConnection = _connectionFactory
            .Create();

        LoginCredentials? loginCredentials = await mySqlConnection.QueryFirstOrDefaultAsync<LoginCredentials>(
            @"SELECT Email, Password FROM Members WHERE Email = @email",
            new
            {
                email
            });
        
        return loginCredentials;
    }
}