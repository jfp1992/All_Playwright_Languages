package org.example;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.Locator;

public class LoginPage {

    static String url = "https://codility-frontend-prod.s3.amazonaws.com/media/task_static/python_selenium_login_page/9a83bda125cd7398f9f482a3d6d45ea4/static/attachments/reference_page.html";
    public Locator wrapperLoginForm;
    public Locator inputEmail;
    public Locator inputPassword;
    public Locator buttonLogin;
    public Locator errorMessages;
    public Locator successMessage;
    public LoginPage(Page page) {
        page = page;
        wrapperLoginForm = page.locator("#login-form");
        inputEmail = page.locator("#email-input");
        inputPassword = page.locator("#password-input");
        buttonLogin = page.locator("#login-button");
        errorMessages = page.locator("#messages");
        successMessage = page.locator(".message.success");
    }
}
