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
            fuckJason.FirstName = "TEST";
            
            Assert.Equal("TEST", fuckJason.FirstName );
        }

    }
}
