using LoginDAL;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Student_GetByUserName()
        {
            userModel model = new userModel();
            users user = model.GetByUserName("Armoxx2");
            Assert.NotNull(user);
        }

        [Fact]
        public void User_Update()
        {
            userModel model = new userModel();
            users employee = model.GetByUserName("Armoxx2");
            employee.email = "es@google.com";
            Assert.True(model.Update(employee) == UpdateStatus.Ok);
        }

        [Fact]
        public void Employee_Add()
        {
            userModel model = new userModel();
            users newEmployee = new users();
            newEmployee.firstname = "John";
            newEmployee.lastname = "Doe";
            newEmployee.username = "test";
            newEmployee.pass = "test";
            newEmployee.email = "test@test.test";
            newEmployee.dob = Convert.ToDateTime("1960-06-06");
            model.Add(newEmployee);
            Assert.True(newEmployee.id > 1);
        }
    }
}
