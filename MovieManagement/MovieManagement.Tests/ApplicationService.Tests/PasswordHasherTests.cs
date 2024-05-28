using MovieManagement.ApplicationServices.Components.PassworHasher;

namespace MovieManagement.Tests.ApplicationService.Tests;

[TestClass]
public class PasswordHasherTests : TestsBase
{
    [TestMethod]
    public void SuccessVerifiedShouldReturnTrue()
    {
        // arrange
        var passwordHash = "NhcpF6OC/X5tkn34moAHSw==;TWA+eqpdasedhq1Jz67WmTPtUfyCcnad0gB9/dE04uc=";
        var inputPassword = "Simple123Pass";

        // act
        TestContext!.WriteLine("Checking password - happy path.");
        var result = new PasswordHasher().Verify(passwordHash, inputPassword);

        // arrange
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void FailedVerifiedShouldReturnFalse()
    {
        // arrange
        var passwordHash = "NhcpF6OC/X5tkn34moAHSw==;TWA+eqpdasedhq1Jz67WmTPtUfyCcnad0gB9/dE04uc=";
        var inputPassword = "simple123Pass";

        // act
        TestContext!.WriteLine("Checking password - fail path.");
        var result = new PasswordHasher().Verify(passwordHash, inputPassword);

        // arrange
        Assert.IsFalse(result);
    }
}
