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
    public class DriverController : ApiController
    {
        [HttpGet]

        //login driver 
        public IHttpActionResult GetLogIn(string Password, string Email)
        {
            try
            {
                DriversDTO driver = new DriversDTO();
                driver = BL.DriverService.Log_In(Password, Email);
                if (driver == null)
                    return BadRequest("אחד מהנתונים שגויים");
                return Ok(driver);
            }
            catch (Exception)
            {
                return BadRequest("אחד מהנתונים שגויים");
            }
           
        }

        //return driver after refresh page
        public IHttpActionResult GetDriverRefresh(int Id)
        {
            try
            {
                DriversDTO Driver = new DriversDTO();
                Driver = BL.DriverService.DriverRefresh(Id);
                if (Driver == null)
                    return BadRequest("בעיה זמנית במערכת");
                return Ok(Driver);
            }
            catch (Exception)
            {
                return BadRequest("בעיה זמנית במערכת");
            }
          
        }

        [HttpPost]

        //Add new driver
        public IHttpActionResult PostSignUp(DriversDTO Driver)
        {
            try
            {
                //Check if such a username already exists in the system

                if (!DriverService.emailExists(Driver.Dr_Email))
                {
                    return BadRequest("ישנו כתובת מייל זו במערכת");
                }
                DriversDTO Driverpost = new DriversDTO();
                Driverpost = BL.DriverService.Sign_Up(Driver);
                if (Driverpost == null)
                    return BadRequest("שגיאת מערכת");
                return Ok(Driverpost);
            }
            catch (Exception)
            {
                return BadRequest("שגיאת מערכת");
            }
           
        }

        [HttpPost]
        //Finding suitable drivers
        [Route("api/PostDrivers", Name = "Order")]
        public IHttpActionResult PostDrivers(OrdersFullDTO Order)
        {
            try
            {
                int Ord_Kod = Order.Ord_Kod;
                string Source = Order.Cust_sourceAddress;
                double Distance = (double)Order.DistanceDestination;
                List<PakegToOrderDTO> Packages = new List<PakegToOrderDTO>();
                Packages = Order.packages;
                List<DriversDTO> Drivers = new List<DriversDTO>();
                Drivers = DriverService.SuitableDrivers(Ord_Kod, Packages, Source, Distance);
                if (Drivers == null)
                    return BadRequest("לא נמצאו נהגים מתאימים");
                return Ok(Drivers);
            }
            catch (Exception)
            {
                return BadRequest("לא נמצאו נהגים מתאימים");
            }
          
        }
        
        [HttpPut]

        //Update driver
        public IHttpActionResult PutUpdateDriver(DriversDTO Driver)
        {
            try
            {
                DriversDTO NewDriver = new DriversDTO();
                NewDriver = BL.DriverService.Update_Driver(Driver);
                if (NewDriver == null)
                    return BadRequest("שגיאת מערכת");
                return Ok(NewDriver);
            }
            catch (Exception)
            {
                return BadRequest("שגיאת מערכת");
            }
          
        }

        //Update status
        public IHttpActionResult PutUpdateStatus(int Id)
        {
            try
            {
                DriversDTO NewDriver = new DriversDTO();
                NewDriver = BL.DriverService.Update_Status(Id);
                if (NewDriver == null)
                    return BadRequest("שגיאה זמנית במערכת");
                return Ok(NewDriver);
            }
            catch (Exception)
            {
                return BadRequest("שגיאה זמנית במערכת");
            }
           
        }

        //Update busy driver
        [Route("api/putbusydriver", Name = "kodDriver")]
        public IHttpActionResult PutUpdatebusy(int kodDriver)
        {
            try
            {
                DriversDTO NewDriver = new DriversDTO();
                NewDriver = BL.DriverService.Update_Busy(kodDriver);
                if (NewDriver == null)
                    return BadRequest("שגיאה זמנית במערכת");
                return Ok(NewDriver);
            }
            catch (Exception)
            {
                return BadRequest("שגיאה זמנית במערכת");
            }
           
        }

        //Update location
        public IHttpActionResult PutUpdateLocation(int Id, string lat,string lon)
        {
            try
            {
                DriversDTO NewDriver = new DriversDTO();
                NewDriver = BL.DriverService.Update_Location(Id, lat, lon);
                if (NewDriver == null)
                    return BadRequest("שגיאה זמנית במערכת");
                return Ok(NewDriver);
            }
            catch (Exception)
            {
                return BadRequest("שגיאה זמנית במערכת");
            }
           
        }

        //Update duration destination in order 
        [Route("api/PutUpdateDurationOrder", Name = "DriverId")]       
        public IHttpActionResult PutUpdateDurationOrder(int DriverId)
        {
            try
            {
                OrdersFullDTO order = new OrdersFullDTO();
                order = BL.DriverService.update_duration_order(DriverId);
                if (order == null)
                    return BadRequest("אחד מהנתונים שגויים");
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest("אחד מהנתונים שגויים");
            }
            
        }

    }
}
