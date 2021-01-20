using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BL;
using dto;
namespace ui.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DriverToOrderController : ApiController
    {
        //Checks and returns if there is an order request to IdDriver
        public IHttpActionResult GetOrderToDriver(int IdDriver)
        {
            try
            {
                List<OrdersFullDTO> NewOrders = new List<OrdersFullDTO>();
                NewOrders = BL.DriverToOrderService.GetOrderOfDriver(IdDriver);
                if (NewOrders == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(NewOrders);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
            
        }

        [HttpPut]
        //update selected driver in dto table
        public IHttpActionResult UpdateSelectedDriver(int IdDriver, int IdOrder)
        {
            try
            {
                DriversToOrderDTO NewDriver = new DriversToOrderDTO();
                NewDriver = BL.DriverToOrderService.SelectedDriver(IdDriver, IdOrder);
                if (NewDriver == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(NewDriver);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
            
        }

        [HttpPut]
        [Route("api/nextDriver/{IdDriver=IdDriver}/{IdOrder=IdOrder}")]
        //update the next driver in dto table
        public IHttpActionResult UpdatenextDriver(int IdDriver, int IdOrder)
        {
            try
            {
                DriversToOrderDTO NewDriver = new DriversToOrderDTO();
                NewDriver = BL.DriverToOrderService.nextDriver(IdDriver, IdOrder);
                if (NewDriver == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(NewDriver);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
          
        }
    }
}