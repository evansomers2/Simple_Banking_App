using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoginDAL
{
    public class userModel
    {
        //making a repository object of users
        IRepository<users> repository;

        //constructor for employee model
        public userModel()
        {
            repository = new LoginRepository<users>();
        }
        

        //get an employee object by id
        public users GetByid(int id)
        {
            //making an employee list for the return of getByid
            List<users> selectedUser = null;

            try
            {
                LoginContext _db = new LoginContext();
                //calling the repository method and setting the return list to selectedUser
                selectedUser = repository.GetByExpression(emp => emp.id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            //return the first element of the list
            return selectedUser.FirstOrDefault();
        }

        //get an employee object by id
        public users GetByUserName(string un)
        {
            //making an employee list for the return of getByid
            List<users> selectedUser = null;

            try
            {
                LoginContext _db = new LoginContext();
                //calling the repository method and setting the return list to selectedUser
                selectedUser = repository.GetByExpression(emp => emp.username == un);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            //return the first element of the list
            return selectedUser.FirstOrDefault();
        }

        //get all users from the database
        public List<users> GetAll()
        {
            //making an employee list for all users
            List<users> allusers = new List<users>();

            try
            {
                //setting allusers to the return of getAll, storing all users from the database
                allusers = repository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            //return the full employee list
            return allusers;
        }

        //add an employee to the database
        public int Add(users newEmployee)
        {
            try
            {
                repository.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newEmployee.id;
        }

        //update an users information in the database
        public UpdateStatus Update(users updatedEmployee)
        {
            //update status is failed by default
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                //setting the update status to the return of Update
                operationStatus = repository.Update(updatedEmployee);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if the data is stale in the database this exception is thrown
                operationStatus = UpdateStatus.Stale;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            //return the status of the update
            return operationStatus;
        }

        //delete an employee from the database
        public int Delete(int id)
        {
            int usersDeleted = -1;

            try
            {
                //set int value usersDeleted to the number of users delete
                //and the return value of repo.delete
                usersDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            //return the number of users deleted
            return usersDeleted;
        }
    }
}
