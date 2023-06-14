using Microsoft.Playwright.NUnit;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [SetUp]
    public void Setup()
    {
        
    }
    [Test]
    public async Task TestLoginFormStableState()
    {
        LoginPage loginPage = new LoginPage(Page);

        await Page.GotoAsync(loginPage.Url);
        await Expect(loginPage.WrapperLoginForm).ToBeVisibleAsync();

        await Expect(loginPage.InputEmail).ToBeVisibleAsync();
        await Expect(loginPage.InputEmail).ToHaveAttributeAsync("placeholder", "e-mail");

        await Expect(loginPage.InputPassword).ToBeVisibleAsync();
        await Expect(loginPage.InputPassword).ToHaveAttributeAsync("placeholder", "password");

        await Expect(loginPage.ButtonLogin).ToBeVisibleAsync();
        await Expect(loginPage.ButtonLogin).ToHaveTextAsync("Login");
    }
    [Test]
    public async Task TestInvalidLoginEmpty()
    {
        LoginPage loginPage = new LoginPage(Page);

        await Page.GotoAsync(loginPage.Url);

        await loginPage.ButtonLogin.ClickAsync();

        await Expect(loginPage.ErrorMessages).ToContainTextAsync("Email is required");
        await Expect(loginPage.ErrorMessages).ToContainTextAsync("Password is required");
    }
    [Test]
    public async Task TestInvalidLoginIncorrectValidEmailOnly()
    {
        LoginPage loginPage = new LoginPage(Page);

        await Page.GotoAsync(loginPage.Url);

        await loginPage.InputEmail.FillAsync("fake.email@fakeemail.com");

        await loginPage.ButtonLogin.ClickAsync();

        await Expect(loginPage.ErrorMessages).ToHaveTextAsync("Password is required");
        await Expect(loginPage.ErrorMessages).Not.ToContainTextAsync("Email is required");
    }
    [Test]
    public async Task TestInvalidLoginCorrectValidEmailOnly()
    {
        LoginPage loginPage = new LoginPage(Page);

        await Page.GotoAsync(loginPage.Url);

        await loginPage.InputEmail.FillAsync("login@codility.com");

        await loginPage.ButtonLogin.ClickAsync();

        await Expect(loginPage.ErrorMessages).ToHaveTextAsync("Password is required");
        await Expect(loginPage.ErrorMessages).Not.ToContainTextAsync("Email is required");
    }
    [Test]
        public async Task TestInvalidLoginCorrectValidEmailWithIncorrectPassword()
        {
            LoginPage loginPage = new LoginPage(Page);
    
            await Page.GotoAsync(loginPage.Url);

            await loginPage.InputEmail.FillAsync("login@codility.com");
            await loginPage.InputPassword.FillAsync("p4ssword");

            await loginPage.ButtonLogin.ClickAsync();

            await Expect(loginPage.ErrorMessages).ToHaveTextAsync("You shall not pass! Arr!");
            await Expect(loginPage.ErrorMessages).Not.ToContainTextAsync("Password is required");
            await Expect(loginPage.ErrorMessages).Not.ToContainTextAsync("Email is required");
        }
        [Test]
        public async Task TestValidLoginWithCorrectCredentials()
        {
            LoginPage loginPage = new LoginPage(Page);
    
            await Page.GotoAsync(loginPage.Url);

            await loginPage.InputEmail.FillAsync("login@codility.com");
            await loginPage.InputPassword.FillAsync("password");

            await loginPage.ButtonLogin.ClickAsync();

            await Expect(loginPage.InputEmail).Not.ToBeVisibleAsync();
            await Expect(loginPage.InputPassword).Not.ToBeVisibleAsync();
            await Expect(loginPage.ButtonLogin).Not.ToBeVisibleAsync();

            await Expect(loginPage.SuccessMessage).ToContainTextAsync("Welcome to Codility");
        }
}
