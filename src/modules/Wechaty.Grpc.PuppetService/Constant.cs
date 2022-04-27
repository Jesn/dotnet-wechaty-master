using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechaty.Grpc.PuppetService
{
    public class Constant
    {
        /// <summary>
        /// 客户端Grpc实例
        /// TODO: 后续可以多实例，该参数应该变成动态获取
        /// </summary>
        public static readonly string GrpcInstaceName = "CsharpWechaty";
    }
}
