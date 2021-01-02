using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Used_cars.Business;

namespace CarUnitTest
{
    [TestClass]
    public class User_test
    {
        //WrongPass method to see whether expected result false come out, having entered wrong password  
        [TestMethod]
        public void WrongPass()
        {
            string un="admin";
            string up="0009";       // wrong password
            Boolean expected = false;
            User u=new User();
            Boolean result=u.Login(un,up);

            Assert.AreEqual(expected,result);
        }

        //WrongUname method to see whether expected result false come out, having entered wrong user name
        [TestMethod]
        public void WrongUname()
        {
            string un = "sdfggg";    // wrong username
            string up = "123";       
            Boolean expected = false;
            User u = new User();
            Boolean result = u.Login(un, up);

            Assert.AreEqual(expected, result);
        }

        //Correct method to see whether expected result true come out, having entered correct user name & password
        [TestMethod]
        public void Correct()
        {
            string un = "admin";    //correct username
            string up = "123";      //correct password    
            Boolean expected = true;
            User u = new User();
            Boolean result = u.Login(un, up);

            Assert.AreEqual(expected, result);
        }

        //Empty_pass method to see whether expected result false come out, having entered correct user name & empty password
        [TestMethod]
        public void Empty_pass()
        {
            string un = "admin";
            string up = "";     //empty password
            Boolean expected = false;
            User u = new User();
            Boolean result = u.Login(un, up);

            Assert.AreEqual(expected, result);
        }
    }
}
