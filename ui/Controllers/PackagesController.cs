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
    public class PackagesController : ApiController
    {
        //List of package types
        public IHttpActionResult GetPackageType()
        {
            try
            {
                List<packagesDTO> ListPackagesDTO = new List<packagesDTO>();
                ListPackagesDTO = BL.PackageServis.PackageType();
                if (ListPackagesDTO == null)
                    return BadRequest("שגיאה זמנית במערכת");
                return Ok(ListPackagesDTO);
            }
            catch (Exception)
            {
                return BadRequest("שגיאה זמנית במערכת");
            }

        }
    }
}


