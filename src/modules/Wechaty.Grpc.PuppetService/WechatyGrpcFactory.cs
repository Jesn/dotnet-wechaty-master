using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;
using GrpcClient = Grpc.Net.Client;

namespace Wechaty.Grpc.PuppetService
{
    public class WechatyGrpcFactory
    {
        protected static readonly Dictionary<string, WechatyGrpcFactory> WechatyGrpcFactories = new Dictionary<string, WechatyGrpcFactory>();
        protected static string instaceName;


        //Gateway 服务发现
        protected const string CHATIE_ENDPOINT = "https://api.chatie.io/v0/hosties/";
        public PuppetClient _grpcClient { get; private set; }
        protected GrpcClient.GrpcChannel _channel { get; set; }

        protected string gatewayUrl { get; set; }
        protected string token { get; set; }

        protected static PuppetOptions Options { get; set; }


        private WechatyGrpcFactory()
        {

        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WechatyGrpcFactory GetGrpcClientInstace(string name)
        {
            if (WechatyGrpcFactories?.Count(x => x.Key == name) > 0)
            {
                var instace = WechatyGrpcFactories[name];
               
                return instace;
            }
            else
            {
                //throw new ApplicationException("未发现对应GRPC实例");
                return SetInstace(name);
            }
        }



        protected static WechatyGrpcFactory SetInstace(string name)
        {
            var instace = new WechatyGrpcFactory();
            if (WechatyGrpcFactories?.Count(x => x.Key == name) > 0)
            {
                return WechatyGrpcFactories[name];
            }
            instaceName = name;
            WechatyGrpcFactories.Add(name, instace);
            return instace;
        }


        public static void IDisposable(string name)
        {
            if (WechatyGrpcFactories?.Count(x => x.Key == name) > 0)
            {
                WechatyGrpcFactories.Remove(name);
            }
        }

        public static void IDisposable() => WechatyGrpcFactories?.Clear();



        public async Task<PuppetClient> StartAsync(PuppetOptions option)
        {
            if (string.IsNullOrEmpty(option.Token) || string.IsNullOrEmpty(option.Endpoint))
            {
                throw new Exception("Token和Endpoint不能为空");
            }
            InitGrpcClient(option);
            await _grpcClient.StartAsync(new StartRequest());

            Options = option;

            _ = SetInstace(option.Name);

            return _grpcClient;
        }

        public async Task StopAsync()
        {
            if (_channel == null || _grpcClient == null)
            {
                throw new Exception("puppetClient had not initialized");
            }
            // 关闭grpc 连接
            await _channel.ShutdownAsync();

            // 注销实例
            IDisposable(instaceName);
        }


        protected PuppetClient InitGrpcClient(PuppetOptions options)
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

                _channel = GrpcChannel.ForAddress(endPoint, new GrpcChannelOptions
                {
                    //HttpClient = httpClient,
                    Credentials = channelCredentials,
                    HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    },
                });
            }
            else
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

                _channel = GrpcChannel.ForAddress(endPoint, new GrpcChannelOptions
                {
                    //HttpClient = httpClient,
                    Credentials = ChannelCredentials.Insecure,
                    //HttpHandler = new HttpClientHandler
                    //{
                    //    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    //},
                });
            }
            if (_channel == null)
            {
                throw new Exception("Grpc config error");
            }

            var grpcClient = new PuppetClient(_channel);
            _grpcClient = grpcClient;
            return grpcClient;
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
