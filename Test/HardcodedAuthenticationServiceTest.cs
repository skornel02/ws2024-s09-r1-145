using API.Services;
using Domain.Services;

namespace Test
{
    public class HardcodedAuthenticationServiceTest
    {
        [Theory]
        [InlineData("admin", "S3cr3t", true)]
        [InlineData("admin", "s3cr3t", false)]
        [InlineData("admin", "sajt", false)]
        [InlineData("nimda", "S3cr3t", false)]
        public void TestUsers(string username, string password, bool result)
        {
            IAuthenticationService service = new HardcodedAuthenticationService();
            Assert.Equal(result, service.ValidatePassword(username, password));
        }
    }
}