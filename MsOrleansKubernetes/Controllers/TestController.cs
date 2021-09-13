using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsOrleansKubernetes.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IClusterClient client;

        public TestController(IClusterClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpGet("nic")]
        public async Task<JsonResult> Nic()
        {
            var guid = Guid.Parse("18134781-0797-44e8-ad22-51d089b5dc7b");
            var grain = client.GetGrain<ITestGrain>(guid);
            await grain.Nic();
            return new JsonResult("ok");
        }

        [HttpGet("test")]
        public async Task<JsonResult> Test()
        {
            var guid = Guid.Parse("18134781-0797-44e8-ad22-51d089b5dc7b");
            var grain = client.GetGrain<ITestGrain>(guid);
            await grain.Test();
            return new JsonResult("ok");
        }

        [HttpGet("nic2")]
        public async Task<JsonResult> Nic2()
        {
            var guid = Guid.Parse("24a3ab5f-1251-406f-9c36-40952b873df2");
            var grain = client.GetGrain<ITestGrain>(guid);
            await grain.Nic();
            return new JsonResult("ok");
        }

        [HttpGet("test2")]
        public async Task<JsonResult> Test2()
        {
            var guid = Guid.Parse("24a3ab5f-1251-406f-9c36-40952b873df2");
            var grain = client.GetGrain<ITestGrain>(guid);
            await grain.Test();
            return new JsonResult("ok");
        }
    }
}
