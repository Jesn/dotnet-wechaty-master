using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wechaty.GateWay;

namespace wechaty_grpc_webapi.Controllers
{
    public class GateWayController : WechatyApiController
    {
        public readonly IConfiguration _configuration;
        public readonly IGateWayService _gateWayService;

        public GateWayController(IConfiguration configuration, IGateWayService gateWayService)
        {
            _configuration = configuration;
            _gateWayService = gateWayService;
        }

        [HttpPut]
        public async Task<ActionResult> Start()
        {
            var puppetOptions = new Wechaty.Module.Puppet.Schemas.PuppetOptions()
            {
                Token = _configuration["WECHATY_PUPPET_SERVICE_TOKEN"],
                Endpoint = _configuration["WECHATY_PUPPET_SERVICE_ENDPOINT"],
                Name = "CsharpWechaty"
            };

            var response = await _gateWayService.StartAsync(puppetOptions);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Stop()
        {
            await _gateWayService.StopAsync();
            return Ok();
        }


    }
}
