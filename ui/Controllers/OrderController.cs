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
    public class OrderController : ApiController
    {
        //return list of orders
        public IHttpActionResult GetMyOrders(int Id)
        {
            try
            {
                List<OrdersFullDTO> ListOfOrders = new List<OrdersFullDTO>();
                ListOfOrders = BL.OredersServis.ListOfOrders(Id);

                if (ListOfOrders == null)
                    return BadRequest("לא בוצעו עדיין הזמנות");
                return Ok(ListOfOrders);
            }
            catch (Exception)
            {
                return BadRequest("לא בוצעו עדיין הזמנות");
            }

        }

        [HttpGet]
        [Route("api/IsOrderConfirmed", Name = "KodOrder")]
        //Checks if the order has been confirmed
        public IHttpActionResult IsOrderConfirmed(int KodOrder)
        {
            try
            {
                DriversToOrderDTO driver;
                driver = BL.OredersServis.OrderConfirmed(KodOrder);
                if (driver == null)
                    return BadRequest("לא נמצא נהג מתאים");
                return Ok(driver);
            }
            catch (Exception)
            {
                return BadRequest("לא נמצא נהג מתאים");
            }

        }

        //return order by id tracking
        [Route("api/MyOrderTrack", Name = "Kod")]
        public IHttpActionResult GetMyOrderTrack(int Kod)
        {
            try
            {
                DriversDTO driver = new DriversDTO();
                driver = OredersServis.OrderTrack(Kod);
                if (driver == null)
                    return BadRequest("לא ניתן לעקוב אחר ההזמנה");
                return Ok(driver);
            }
            catch (Exception)
            {
                return BadRequest("לא ניתן לעקוב אחר ההזמנה");
            }

        }

        [HttpPost]

        //Add new order
        [Route("api/PostOrder", Name = "PostOrder")]
        public IHttpActionResult PostOrder(OrdersFullDTO PostOrder)
        {
            try
            {
                OrdersFullDTO newOrder = BL.OredersServis.AddOrder(PostOrder);
                if (newOrder == null)
                    return BadRequest("בעיה זמנית במערכת");
                var packages = BL.PackageToOrderService.CreatePackagesToOrder(newOrder);
                if (packages == null)
                    return BadRequest("בעיה זמנית במערכת");
                newOrder.packages = packages;

                return Ok(newOrder);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }

        }

        //Calculating price to order
        [Route("api/Calculatingprice", Name = "NewOrder")]
        public IHttpActionResult Calculatingprice(OrdersFullDTO NewOrder)
        {
            try
            {
                NewOrder = BL.OredersServis.Calculatingprice(NewOrder);
                if (NewOrder == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(NewOrder);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
           
        }

        [HttpPut]

        //update order by driver
        public IHttpActionResult UpdateOrder(OrdersFullDTO NewOrder)
        {
            try
            {
                NewOrder = BL.DriverToOrderService.UpdateOrder(NewOrder);
                if (NewOrder == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(NewOrder);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
          
        }
        [HttpPut]

        [Route("api/UpdateRating/{IdOrder=IdOrder}/{num=num}")]
        //update rating of driver in order
        public IHttpActionResult UpdateRating(int IdOrder, int num)
        {
            try
            {
                OrdersFullDTO NewOrder = BL.OredersServis.Rating(IdOrder, num);
                if (NewOrder == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(NewOrder);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
          
        }
    }
}