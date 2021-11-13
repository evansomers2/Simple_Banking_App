using System;
using System.Collections.Generic;
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
    public class BankAccountController : ControllerBase
    {
        //creating a logger to handle messages from server
        private readonly ILogger _logger;
        public BankAccountController(ILogger<BankAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                BankAccountViewModel viewmodel = new BankAccountViewModel();
                //setting a list of employee view models from the return of get all
                List<BankAccountViewModel> allemployees = viewmodel.getAll();
                //return ok status to the client
                return Ok(allemployees);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                BankAccountViewModel viewmodel = new BankAccountViewModel();
                viewmodel.CustomerId = id;
                //setting the view model object to the return of getByEmail
                viewmodel.GetById();
                //return Ok status to the client
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); //something went wrong
            }
        }

        //update employee information in the database
        [HttpPut]
        public IActionResult Put([FromBody] BankAccountViewModel viewModel)
        {
            try
            {
                //update the employee
                int retVal = viewModel.Update();
                //checking for status of update from return of update
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "Account " + viewModel.Id + " updated!" });
                    case -1:
                        return Ok(new { msg = "Account " + viewModel.Id + " not updated!" });
                    case -2:
                        return Ok(new { msg = "Data is stale for " + viewModel.Id + ", Account not updated!" });
                    default:
                        return Ok(new { msg = "Account " + viewModel.Id + " not updated!" });
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
        public IActionResult Post(BankAccountViewModel viewmodel)
        {
            try
            {
                //add the view model to the database
                viewmodel.Add();
                //return a status to the client based on the id return of employee
                return viewmodel.Id > 1 ? Ok(new { msg = "User " + viewmodel.Id + " added!" })
                    : Ok(new { msg = "User " + viewmodel.Id + " not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("/api/MoneyTransfer/{account}/{accountNumber}/{amount}")]
        public ActionResult HashPassword(int account, int accountNumber, decimal amount)
        {
            BankAccountViewModel viewmodel = new BankAccountViewModel();
            viewmodel.Id = accountNumber;
            viewmodel.GetByAccountNumber();
            viewmodel.Balance += amount;
            viewmodel.Update();

            viewmodel = new BankAccountViewModel();
            viewmodel.Id = account;
            viewmodel.GetByAccountNumber();
            viewmodel.Balance -= amount;
            viewmodel.Update();

            return Ok(viewmodel);
        }


        //delete an employee from the database
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                BankAccountViewModel viewmodel = new BankAccountViewModel();
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