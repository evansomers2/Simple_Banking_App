using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LoginViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserLoginSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //creating a logger to handle messages from server
        private readonly ILogger _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                UserViewModel viewmodel = new UserViewModel();
                //setting a list of employee view models from the return of get all
                List<UserViewModel> allemployees = viewmodel.getAll();
                //return ok status to the client
                return Ok(allemployees);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Route("/api/HashPassword/{pw}")]
        public ActionResult HashPassword(string pw)
        {
            UserViewModel viewmodel = new UserViewModel();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pw);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            viewmodel.PassWord = hash;

            return Ok(viewmodel);
        }

        [Route("/api/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            UserViewModel viewmodel = new UserViewModel();
            viewmodel.Id = id;
            //setting the view model object to the return of getByEmail
            viewmodel.GetById();

           
            return Ok(viewmodel);
        }

        [Route("/api/ResetPassword/{un}/{key}")]
        public IActionResult ResetPassword(string un, string key)
        {
            UserViewModel viewmodel = new UserViewModel();
            viewmodel.UserName = un;
            //setting the view model object to the return of getByEmail
            viewmodel.GetByUserName();

            System.Diagnostics.Process process1;

            process1 = new System.Diagnostics.Process();

            //Do not receive an event when the process exits.

            process1.EnableRaisingEvents = false;

            //The "/C" Tells Windows to Run The Command then Terminate

            string strCmdLine;

            strCmdLine = "/C python C:\\$info3070\\UserLogin\\resetpassword.py " + viewmodel.Email + " " + key;

            System.Diagnostics.Process.Start("CMD.exe", strCmdLine);

            process1.Close();
            return Ok(viewmodel);
        }

        [HttpGet("{un}")]
        public IActionResult GetByUserName(string un)
        {
            try
            {
                UserViewModel viewmodel = new UserViewModel();
                viewmodel.UserName = un;
                //setting the view model object to the return of getByEmail
                viewmodel.GetByUserName();
                //return Ok status to the client
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); //something went wrong
            }
        }


        //get employee data from database by employee email
        //[HttpGet("{email}")]
        //public IActionResult GetByEmail(string email)
        //{
        //    try
        //    {
        //        UserViewModel viewmodel = new UserViewModel();
        //        viewmodel.Email = email;
        //        //setting the view model object to the return of getByEmail
        //        viewmodel.GetByEmail();
        //        //return Ok status to the client
        //        return Ok(viewmodel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError); //something went wrong
        //    }
        //}

        //update employee information in the database
        [HttpPut]
        public IActionResult Put([FromBody] UserViewModel viewModel)
        {
            try
            {
                //update the employee
                int retVal = viewModel.Update();
                //checking for status of update from return of update
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "User " + viewModel.UserName + " updated!" });
                    case -1:
                        return Ok(new { msg = "User " + viewModel.UserName + " not updated!" });
                    case -2:
                        return Ok(new { msg = "Data is stale for " + viewModel.UserName + ", User not updated!" });
                    default:
                        return Ok(new { msg = "User " + viewModel.UserName + " not updated!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //get info of all employees from database


        //add a new employee to the database
        [HttpPost]
        public IActionResult Post(UserViewModel viewmodel)
        {
            try
            {
                //add the view model to the database
                viewmodel.Add();
                //return a status to the client based on the id return of employee
                return viewmodel.Id > 1 ? Ok(new { msg = "User " + viewmodel.LastName + " added!" })
                    : Ok(new { msg = "User " + viewmodel.LastName + " not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        //delete an employee from the database
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                UserViewModel viewmodel = new UserViewModel();
                viewmodel.Id = id;
                //if the number of employess deleted is one return ok message to client
                return viewmodel.Delete() == 1
                    ? Ok(new { msg = "User " + id + " deleted!" })
                    : Ok(new { msg = "User " + id + " not deleted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}