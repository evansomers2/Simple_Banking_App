using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoginDAL
{
    public class bankAccountModel
    {
        //making a repository object of bankaccounts
        IRepository<bankaccounts> repository;

        //constructor for employee model
        public bankAccountModel()
        {
            repository = new LoginRepository<bankaccounts>();
        }


        //get an employee object by id
        public bankaccounts GetByid(int id)
        {
            //making an employee list for the return of getByid
            List<bankaccounts> selectedAccount = null;

            try
            {
                LoginContext _db = new LoginContext();
                //calling the repository method and setting the return list to selectedAccount
                selectedAccount = repository.GetByExpression(emp => emp.customerid == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            //return the first element of the list
            return selectedAccount.FirstOrDefault();
        }

        public bankaccounts GetByAccountNumber(int id)
        {
            //making an employee list for the return of getByid
            List<bankaccounts> selectedAccount = null;

            try
            {
                LoginContext _db = new LoginContext();
                //calling the repository method and setting the return list to selectedAccount
                selectedAccount = repository.GetByExpression(emp => emp.id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            //return the first element of the list
            return selectedAccount.FirstOrDefault();
        }


        //get all bankaccounts from the database
        public List<bankaccounts> GetAll()
        {
            //making an employee list for all bankaccounts
            List<bankaccounts> allaccounts = new List<bankaccounts>();

            try
            {
                //setting allaccounts to the return of getAll, storing all bankaccounts from the database
                allaccounts = repository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            //return the full employee list
            return allaccounts;
        }

        //add an employee to the database
        public int Add(bankaccounts newAccount)
        {
            try
            {
                repository.Add(newAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newAccount.id;
        }

        //update an bankaccounts information in the database
        public UpdateStatus Update(bankaccounts updatedAccount)
        {
            //update status is failed by default
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                //setting the update status to the return of Update
                operationStatus = repository.Update(updatedAccount);
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
            int accountsDeleted = -1;

            try
            {
                //set int value accountsDeleted to the number of bankaccounts delete
                //and the return value of repo.delete
                accountsDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            //return the number of bankaccounts deleted
            return accountsDeleted;
        }
    }
}
