using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LoginDAL;

namespace LoginViewModels
{
    public class UserViewModel
    {
        private userModel _model;
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string timer { get; set; }

        public UserViewModel()
        {
            _model = new userModel();
        }

        //set employee object data to the data returned from the database
        public void GetByUserName()
        {
            try
            {
                //create new employee object and set it to return of getByEmail
                users emp = _model.GetByUserName(UserName);

                //setting data attributes
                UserName = emp.username;
                FirstName = emp.firstname;
                LastName = emp.lastname;
                PassWord = emp.pass;
                Dob = emp.dob;
                Email = emp.email;
                Id = emp.id;
                timer = Convert.ToBase64String(emp.timer);
            }

            //exception if the employee is not found
            catch (NullReferenceException nex)
            {
                //email is "not found" if employee 
                UserName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
            }
            catch (Exception ex)
            {
                UserName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }


        //set employee object data to the data returned from the database
        public void GetById()
        {
            try
            {
                //create new employee object and set it to return of getById
                users emp = _model.GetByid(Id);

                //setting data attributes
                UserName = emp.username;
                FirstName = emp.firstname;
                LastName = emp.lastname;
                PassWord = emp.pass;
                Dob = emp.dob;
                Id = emp.id;
                timer = Convert.ToBase64String(emp.timer);
            }

            //exception if the employee is not found
            catch (NullReferenceException nex)
            {
                UserName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
            }
            catch (Exception ex)
            {
                UserName = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //get all users from the database
        public List<UserViewModel> getAll()
        {
            //a list to store all employee view models returned from database
            List<UserViewModel> allVms = new List<UserViewModel>();
            try
            {
                //list to store all users returned from getAll
                List<users> allusers = _model.GetAll();

                //for every employee in allusers create employeeviewmodel and add to allVms
                foreach (users emp in allusers)
                {
                    //creating new employee view model
                    UserViewModel empVm = new UserViewModel();
                    //setting view model attributes
                    empVm.UserName = emp.username;
                    empVm.FirstName = emp.firstname;
                    empVm.LastName = emp.lastname;
                    empVm.PassWord = emp.pass;
                    Email = emp.email;
                    empVm.Dob = emp.dob;
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
                users emp = new users();

                //setting employee attributes
                emp.username = UserName;
                emp.pass = PassWord;
                emp.firstname = FirstName;
                emp.lastname = LastName;
                emp.email = Email;
                emp.dob = Dob;

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
            UpdateStatus usersUpdated = UpdateStatus.Failed;
            try
            {
                //creating a new employee object
                users emp = new users();

                //setting data attributes
                emp.username = UserName;
                emp.pass = PassWord;
                emp.firstname = FirstName;
                emp.lastname = LastName;
                emp.email = Email;
                emp.dob = Dob;
                emp.id = Id;
                emp.timer = Convert.FromBase64String(timer);

                //setting update status to the return of update
                //updating the employee in the database
                usersUpdated = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(usersUpdated);
        }

        //method for deleting an employee from the database
        public int Delete()
        {
            //number of users deleted, default to -1
            int usersDeleted = -1;
            try
            {
                //setting usersdelete to return of delete
                //deleting employee from database
                usersDeleted = _model.Delete(Id);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            //return the number of users
            return usersDeleted;
        }
    }

}
