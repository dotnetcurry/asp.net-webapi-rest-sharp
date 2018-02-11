using REST_Api.Models;
using REST_Api.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.Http.Cors;

namespace REST_Api.Controllers
{
    [EnableCors("*","*","*")]
    public class EmployeeInfoAPIController : ApiController
    {
        IRepository<EmployeeInfo, int> repository;
        public EmployeeInfoAPIController(IRepository<EmployeeInfo, int> repository)
        {
            this.repository = repository;
        }
        // GET: api/EmployeeInfoAPI
        [ResponseType(typeof(IEnumerable<EmployeeInfo>))]
        public IHttpActionResult Get()
        {
            return Ok(repository.Get());
        }

        // GET: api/EmployeeInfoAPI/5
        [ResponseType(typeof(EmployeeInfo))]
        public IHttpActionResult Get(int id)
        {
            var Emp = repository.Get(id);
            if (Emp == null)
            {
                //throw new Exception($"{ HttpStatusCode.NotFound} Record is Not Available");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format($"No Employee with ID = {id}")),
                    ReasonPhrase = "Employee ID Not Found"
                });
            }
            //  return new ResponseMessageResult(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found"));
            return Ok(Emp);
        }

        // POST: api/EmployeeInfoAPI
        [ResponseType(typeof(EmployeeInfo))]
        public IHttpActionResult Post([FromBody]EmployeeInfo emp)
        {
            if (ModelState.IsValid)
            {
                return Ok(repository.Create(emp));
            }
            else
            {
                // return new ResponseMessageResult(Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,ModelState));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent(string.Format($"Error with Posted Data \n {GetModelErrorMessagesHelper(ModelState)}")),
                    ReasonPhrase = "Employee Data is Invalid"
                });
            }
        }

        // PUT: api/EmployeeInfoAPI/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Put(int id, [FromBody]EmployeeInfo emp)
        {
            if (ModelState.IsValid)
            {
                var res = repository.Update(id, emp);
                if (!res)
                {
                    // return new ResponseMessageResult(Request.CreateErrorResponse(HttpStatusCode.NotModified, "Update Failed"));
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format($"No Employee with ID = {id}")),
                        ReasonPhrase = "Employee ID Not Found"
                    });
                }
                else
                {
                    return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK, "Update Successful"));
                    // return Ok(true);
                }
            }
            else
            {
                //return new ResponseMessageResult(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found"));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent(string.Format($"Error with Posted Data \n {GetModelErrorMessagesHelper(ModelState)}")),
                    ReasonPhrase = "Employee Data is Invalid"
                });
            }
        }

        // DELETE: api/EmployeeInfoAPI/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            var res = repository.Delete(id);
            if (res)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK, "Record Deleted Successfully"));
                //return Ok(true);
            }
            //return new ResponseMessageResult(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found"));
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(string.Format($"No Employee with ID = {id}")),
                ReasonPhrase = " Employee ID Not Found"
            });
        }
       
        /// <summary>
        /// Helper method for reading the ModelstateDictionary for errors
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private string GetModelErrorMessagesHelper(ModelStateDictionary errors)
        {
            string messages = "";
            foreach (var item in errors)
            {
                    for (int j = 0; j < item.Value.Errors.Count; j++)
                    {
                        messages += $"{item.Key.ToString()} \t {item.Value.Errors[j].ErrorMessage} \n";
                    }
            }
            return messages;
        }
    }
}
