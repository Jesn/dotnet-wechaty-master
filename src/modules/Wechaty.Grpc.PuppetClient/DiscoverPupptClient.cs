using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using static Wechaty.Puppet;

namespace Wechaty.Grpc.Client
{
    internal class DiscoverPupptClient
    {
        protected const string CHATIE_ENDPOINT = "https://api.chatie.io/v0/hosties/";

        internal static PuppetClient InitGrpcClient(GrpcPuppetOption options)
        {
            var endPoint = options.ENDPOINT;
            if (string.IsNullOrEmpty(endPoint))
            {
                var model = DiscoverHostieIp(options.Token);
                if (model.IP == "0.0.0.0" || model.Port == 0)
                {
                    throw new Exception("no endpoint");
                }
                endPoint = "https://" + model.IP + ":" + model.Port;
            }

            if (endPoint.ToUpper().StartsWith("HTTPS://"))
            {
                var credentials = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    if (!string.IsNullOrEmpty(options.Token))
                    {
                        metadata.Add("Authorization", $"Wechaty {options.Token}");
                    }
                    return Task.CompletedTask;
                });
                var channelCredentials = ChannelCredentials.Create(new SslCredentials(), credentials);

                var _channel = GrpcChannel.ForAddress(endPoint, new GrpcChannelOptions
                {
                    //HttpClient = httpClient,
                    Credentials = channelCredentials,
                    HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    },
                });

                var grpcClient = new PuppetClient(_channel);
                return grpcClient;
            }
            else
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

                var _channel = GrpcChannel.ForAddress(endPoint, new GrpcChannelOptions
                {
                    //HttpClient = httpClient,
                    Credentials = ChannelCredentials.Insecure,
                    //HttpHandler = new HttpClientHandler
                    //{
                    //    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    //},
                });
                var grpcClient = new PuppetClient(_channel);
                return grpcClient;
            }



        }

        /// <summary>
        /// 发现 hostie gateway 对应的服务是否能能访问
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected static HostieEndPoint DiscoverHostieIp(string token)
        {
            try
            {
                var model = new HostieEndPoint();

                var url = CHATIE_ENDPOINT + token;

                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        model = JsonConvert.DeserializeObject<HostieEndPoint>(response.Content.ReadAsStringAsync().Result);
                        return model;
                    }
                }
                throw new Exception("获取hostie gateway 对应的主机信息异常");
            }
            catch (Exception ex)
            {
                throw new Exception("获取hostie gateway 对应的主机信息异常");
            }
        }
    }
}
