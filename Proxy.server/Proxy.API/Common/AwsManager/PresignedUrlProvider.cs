using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace Proxy.API.Common.AwsManager;

public class PresignedUrlProvider : IPresignedUrlProvider
{
    private const string BucketName = "insurancetestbucket";
    private const int TimeoutDurationInMinutes = 10;
    
    private readonly IConfiguration _configuration;

    public PresignedUrlProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(string objectKey)
    {
        var accessKey = _configuration.GetValue<string>("AWS:AccessKey");
        var secretKey = _configuration.GetValue<string>("AWS:SecretKey");
        
        var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.SAEast1);
        
        string urlString = string.Empty;
        try
        {
            var request = new GetPreSignedUrlRequest()
            {
                BucketName = BucketName,
                Key = objectKey,
                Expires = DateTime.UtcNow.AddMinutes(TimeoutDurationInMinutes),
            };
            urlString = s3Client.GetPreSignedURL(request);
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error:'{ex.Message}'");
        }

        return urlString;
    }
}