using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Newtonsoft.Json;
using APIContracts;
using Olive;
using System.Net;

namespace Website.API
{
    [ApiController]
    [Route("api/v1/")]
    public class ShipmentController : BaseApiController
    {
        IShipmentService ShipmentService;
        IFileProcessorService FileProcessorService;

        public ShipmentController(IShipmentService shipmentService, IFileProcessorService fileProcessorService)
        {
            ShipmentService = shipmentService;
            FileProcessorService = fileProcessorService;
        }


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost, Route("gettoken")]
        public async Task<IActionResult> GetToken([FromBody] LoginContract loginContract)
        {
            try
            {
                var result = await ShipmentService.Login(loginContract.Username, loginContract.Password);
                return Ok(new LoginResponse { Token = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost, Route("createshipment")]
        [ShipmentAuthorizeAtribute]
        [ValidateModel]
        public async Task<IActionResult> CreateEADShipment([FromBody] ShipmentContract shipment)
        {
            try
            {
                await ShipmentService.Log(shipment.ToJsonString(), LogType.EadShipmentAPIRequest);
                var username = Context.Current.Http().Items["api-user"].ToString();
                var result = await ShipmentService.CreateEADShipment(shipment, username);
                await ShipmentService.Log(result.ToJsonString(), LogType.EadShipmentAPIResponse);

                return Ok(new { TrackingNumber = result });
            }
            catch (Exception ex)
            {
                Olive.Log.For<ShipmentContract>().Error(ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
