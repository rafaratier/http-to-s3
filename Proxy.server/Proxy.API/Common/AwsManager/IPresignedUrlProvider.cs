namespace Proxy.API.Common.AwsManager;

public interface IPresignedUrlProvider
{
    string Generate(string objectKey);
}