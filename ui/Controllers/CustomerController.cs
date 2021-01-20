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
    public class CustomerController : ApiController
    {
        //login customer 
        public IHttpActionResult GetLogIn(string Password, string Email)
        {
            try
            {
                CustomerDTO customer = new CustomerDTO();
                customer = BL.CustomersServis.Log_In(Password, Email);
                if (customer == null)
                    return BadRequest("אחד  מהנתונים שגויים");
                return Ok(customer);
            }
            catch
            {
                return BadRequest("אחד  מהנתונים שגויים");
            }
        }

        //return customer after refresh page
        public IHttpActionResult GetCustomerRefresh(int Id)
        {
            try
            {
                CustomerDTO customer = new CustomerDTO();
                customer = BL.CustomersServis.CustomerRefresh(Id);
                if (customer == null)
                    return BadRequest("אחד מהנתונים שגויים");
                return Ok(customer);
            }
            catch
            {
                return BadRequest("בעיה זמנית במערכת");
            }
        }

        [HttpPost]
        //Add new customer
        public IHttpActionResult PostSignUp(CustomerDTO customer)
        {
            try
            {
                //Check if such a username already exists in the system
                if (!CustomersServis.emailExists(customer.Cust_Email))
                {
                    return BadRequest(" ישנו כתובת מייל זו במערכת");
                }
                CustomerDTO customerPost = new CustomerDTO();
                customerPost = BL.CustomersServis.Sign_Up(customer);
                if (customerPost == null)
                    return BadRequest("שגיאת מערכת");
                return Ok(customerPost);
            }
            catch (Exception)
            {
                return BadRequest("שגיאת מערכת");
            }

        }

        [HttpPut]
        //Update customer
        public IHttpActionResult PutUpdateCustomer(CustomerDTO Customer)
        {
            try
            {
                CustomerDTO NewCustomer = new CustomerDTO();
                NewCustomer = BL.CustomersServis.Update_Customer(Customer);
                if (NewCustomer == null)
                    return BadRequest("שגיאת מערכת");
                return Ok(NewCustomer);
            }
            catch (Exception)
            {

                return BadRequest("שגיאת מערכת");
            }

        }
    }
}
