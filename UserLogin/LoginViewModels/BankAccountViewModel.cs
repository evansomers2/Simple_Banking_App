using LoginDAL;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LoginViewModels
{
   public class BankAccountViewModel
    {
        private bankAccountModel _model;
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public string timer { get; set; }

        public BankAccountViewModel()
        {
            _model = new bankAccountModel();
        }

        //set employee object data to the data returned from the database
        public void GetById()
        {
            try
            {
                //create new employee object and set it to return of getById
                bankaccounts emp = _model.GetByid(CustomerId);

                //setting data attributes
                Balance = emp.balance;
                CustomerId = emp.customerid;
                Id = emp.id;
                timer = Convert.ToBase64String(emp.timer);
            }

            //exception if the employee is not found
            catch (NullReferenceException nex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public void GetByAccountNumber()
        {
            try
            {
                //create new employee object and set it to return of getById
                bankaccounts emp = _model.GetByAccountNumber(Id);

                //setting data attributes
                Balance = emp.balance;
                CustomerId = emp.customerid;
                Id = emp.id;
                timer = Convert.ToBase64String(emp.timer);
            }

            //exception if the employee is not found
            catch (NullReferenceException nex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //get all bankaccounts from the database
        public List<BankAccountViewModel> getAll()
        {
            //a list to store all employee view models returned from database
            List<BankAccountViewModel> allVms = new List<BankAccountViewModel>();
            try
            {
                //list to store all bankaccounts returned from getAll
                List<bankaccounts> allbankaccounts = _model.GetAll();

                //for every employee in allbankaccounts create employeeviewmodel and add to allVms
                foreach (bankaccounts emp in allbankaccounts)
                {
                    //creating new employee view model
                    BankAccountViewModel empVm = new BankAccountViewModel();
                    //setting view model attributes
                    //setting data attributes
                    empVm.Balance = emp.balance;
                    empVm.CustomerId = emp.customerid;
                    empVm.Id = emp.id;
                    empVm.timer = Convert.ToBase64String(emp.timer);
                    //add view model to allvms
                    allVms.Add(empVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            //return the list of all employeeViewModels
            return allVms;
        }

        //method for adding employee to the database
        public void Add()
        {
            //default id is -1 so it doesnt reference a database object
            Id = -1;
            try
            {
                //create new employee object
                bankaccounts emp = new bankaccounts();
                UserViewModel usr = new UserViewModel();
                users user = new users();
                user.id = usr.Id;
                user.firstname = usr.FirstName;
                user.lastname = usr.LastName;
                user.username = usr.UserName;
                user.pass = usr.PassWord;
                user.dob = usr.Dob;
                user.timer = null;
                usr.Id = CustomerId;
                usr.GetById();
                //setting employee attributes
                emp.customerid = CustomerId;
                emp.balance = Balance;
                emp.customer = user; 
                //setting id to the return of add
                //adding employee to the database
                Id = _model.Add(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //method for updating the employee
        public int Update()
        {
            //update status is default failed
            UpdateStatus bankaccountsUpdated = UpdateStatus.Failed;
            try
            {
                //creating a new employee object
                bankaccounts emp = new bankaccounts();

                //setting data attributes
                emp.id = Id;
                emp.customerid = CustomerId;
                emp.balance = Balance;
                emp.timer = Convert.FromBase64String(timer);

                //setting update status to the return of update
                //updating the employee in the database
                bankaccountsUpdated = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(bankaccountsUpdated);
        }

        //method for deleting an employee from the database
        public int Delete()
        {
            //number of bankaccounts deleted, default to -1
            int bankaccountsDeleted = -1;
            try
            {
                //setting bankaccountsdelete to return of delete
                //deleting employee from database
                bankaccountsDeleted = _model.Delete(Id);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            //return the number of bankaccounts
            return bankaccountsDeleted;
        }
    }
}

