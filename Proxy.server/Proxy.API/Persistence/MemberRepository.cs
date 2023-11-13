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
    
    public async Task<Member?> GetMemberByEmailAsync(string email)
    {
        await using MySqlConnection mySqlConnection = _connectionFactory
            .Create();

        Member member = await mySqlConnection.QueryFirstOrDefaultAsync<Member>(
            "SELECT Email From Members WHERE Email = @email",
            new
            {
                email
            });

        return member;
    }

    public async Task<Credentials?> GetLoginCredentialsByEmailAsync(string email)
    {
        await using MySqlConnection mySqlConnection = _connectionFactory
            .Create();

        Credentials? loginCredentials = await mySqlConnection.QueryFirstOrDefaultAsync<Credentials>(
            @"SELECT Email, Password FROM Members WHERE Email = @email",
            new
            {
                email
            });
        
        return loginCredentials;
    }

    public async Task<bool> Save(string email, string password)
    {
        await using MySqlConnection mySqlConnection = _connectionFactory
            .Create();

        var affectedRows = await mySqlConnection.ExecuteAsync("""
                                           INSERT INTO Members (email, password) VALUES (@email, @password)
                                           """, new
        {
            email, password
        });

        return affectedRows == 1 ? true : false;
    }
}