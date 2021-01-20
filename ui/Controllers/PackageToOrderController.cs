using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BL;
using dto;

namespace ui.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PackageToOrderController : ApiController
    {
        //List of package types to order
        public IHttpActionResult GetPackagesToOrder(OrdersFullDTO Order)
        {
            try
            {
                List<packagesDTO> ListPackages = new List<packagesDTO>();
                ListPackages = BL.PackageToOrderService.PackagesToOrder(Order);
                if (ListPackages == null)
                    return BadRequest("שגיאה זמנית במערכת");
                return Ok(ListPackages);

            }
            catch (Exception)
            {
                return BadRequest("שגיאה זמנית במערכת");
            }

        }

    }
}