using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;

namespace Wechaty.Grpc.PuppetService
{
    public class WechatyPuppetService
    {
        internal PuppetClient _grpcClient;

        //Gateway 服务发现
        protected const string CHATIE_ENDPOINT = "https://api.chatie.io/v0/hosties/";

        public WechatyPuppetService()
        {
            var option = new PuppetOptions()
            {
                Endpoint = "https://117.68.179.10:9001",
                Token = "insecure_4ad64522-e86d-4a18-afc9-986a9e2078ef"
            };
            if (_grpcClient==null)
            {
                _grpcClient = InitGrpcClient(option);
            }
        }


        public PuppetClient InitGrpcClient(PuppetOptions options)
        {
            if (_grpcClient != null)
            {
                return _grpcClient;
            }
            var endPoint = options.Endpoint;
            if (string.IsNullOrEmpty(endPoint))
            {
                var model = DiscoverHostieIp(options.Token);
                if (model.IP == "0.0.0.0" || model.Port == "0")
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

                var channel = GrpcChannel.ForAddress(endPoint, new GrpcChannelOptions
                {
                    //HttpClient = httpClient,
                    Credentials = channelCredentials,
                    HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    },
                });

                var grpcClient = new PuppetClient(channel);
                return grpcClient;
            }
            else
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

                var channel = GrpcChannel.ForAddress(endPoint, new GrpcChannelOptions
                {
                    //HttpClient = httpClient,
                    Credentials = ChannelCredentials.Insecure,
                    //HttpHandler = new HttpClientHandler
                    //{
                    //    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    //},
                });
                var grpcClient = new PuppetClient(channel);
                return grpcClient;
            }
        }

        /// <summary>
        /// 发现 hostie gateway 对应的服务是否能能访问
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected HostieEndPoint DiscoverHostieIp(string token)
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
