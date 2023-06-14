using Microsoft.Playwright;
namespace PlaywrightDemo.Pages;

public class LoginPage
{
    private IPage _page;
    public readonly string Url;
    public readonly ILocator WrapperLoginForm;
    public readonly ILocator InputEmail;
    public readonly ILocator InputPassword;
    public readonly ILocator ButtonLogin;
    public readonly ILocator ErrorMessages;
    public readonly ILocator SuccessMessage;
    
    public LoginPage(IPage page)
    {
        _page = page;
        Url = "https://codility-frontend-prod.s3.amazonaws.com/media/task_static/python_selenium_login_page/9a83bda125cd7398f9f482a3d6d45ea4/static/attachments/reference_page.html";
        WrapperLoginForm = page.Locator("#login-form");
        InputEmail = page.Locator("#email-input");
        InputPassword = page.Locator("#password-input");
        ButtonLogin = page.Locator("#login-button");
        ErrorMessages = page.Locator("#messages");
        SuccessMessage = page.Locator(".message.success");
    }
}
