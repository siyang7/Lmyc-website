using System;
using Xunit;
using Lmyc;
using Lmyc.Models;

namespace LmycTests
{
    public class ApplicationUserTest
    {
        [Fact]
        public void MaxLengthForUserFirstNameName()
        {
            ApplicationUser fuckJason= new ApplicationUser();
            fuckJason.FirstName = "Fuck Jason . Com";
            
            Assert.Equal("Fuck Jason . Com", fuckJason.FirstName );
        }

    }
}
