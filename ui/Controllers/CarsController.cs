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
    public class CarsController : ApiController
    {
        //return type list of cars
        public IHttpActionResult GetCarType()
        {
            try
            {
                List<CarsDTO> ListCarsDTO = new List<CarsDTO>();
                ListCarsDTO = BL.CarsService.CarsType();
                if (ListCarsDTO == null)
                    return BadRequest("שגיאה זמנית במערכת");
                return Ok(ListCarsDTO);
            }
            catch (Exception)
            {

                return BadRequest("שגיאה זמנית במערכת");
            }

        }
    }
}