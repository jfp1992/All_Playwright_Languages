package org.example;

import com.microsoft.playwright.Browser;
import com.microsoft.playwright.BrowserContext;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.Playwright;
import org.junit.jupiter.api.*;

import static com.microsoft.playwright.assertions.PlaywrightAssertions.assertThat;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
public class App {
    // Shared between all tests in this class.
    static Playwright playwright;
    static Browser browser;

    // New instance for each test method.
    BrowserContext context;
    Page page;

    LoginPage loginPage;

    @BeforeAll
    static void launchBrowser() {
        playwright = Playwright.create();
        browser = playwright.chromium().launch();
    }

    @AfterAll
    static void closeBrowser() {
        playwright.close();
    }

    @BeforeEach
    void createContextAndPage() {
        context = browser.newContext();
        page = context.newPage();
        page.navigate(LoginPage.url);
        this.loginPage = new LoginPage(page);
    }

    @AfterEach
    void closeContext() {
        context.close();
    }

    @Test
    void TestLoginFormStableState() {
        assertThat(loginPage.inputEmail).hasAttribute("placeholder", "e-mail");

        assertThat(loginPage.inputPassword).isVisible();
        assertThat(loginPage.inputPassword).hasAttribute("placeholder", "password");

        assertThat(loginPage.buttonLogin).isVisible();
        assertThat(loginPage.buttonLogin).hasText("Login");
    }

    @Test
    void testInvalidLoginEmpty() {
        loginPage.buttonLogin.click();

        assertThat(loginPage.errorMessages).containsText("Email is required");
        assertThat(loginPage.errorMessages).containsText("Password is required");
    }

    @Test
    void testInvalidLoginIncorrectValidEmailOnly() {
        loginPage.inputEmail.fill("fake.email@fakeemail.com");

        loginPage.buttonLogin.click();

        assertThat(loginPage.errorMessages).hasText("Password is required");
        assertThat(loginPage.errorMessages).not().containsText("Email is required");
    }

    @Test
    void testInvalidLoginCorrectValidEmailOnly() {
        loginPage.inputEmail.fill("login@codility.com");

        loginPage.buttonLogin.click();

        assertThat(loginPage.errorMessages).hasText("Password is required");
        assertThat(loginPage.errorMessages).not().containsText("Email is required");
    }

    @Test
    void testInvalidLoginCorrectValidEmailWithIncorrectPassword() {
        loginPage.inputEmail.fill("login@codility.com");
        loginPage.inputPassword.fill("p4ssword");

        loginPage.buttonLogin.click();

        assertThat(loginPage.errorMessages).hasText("You shall not pass! Arr!");
        assertThat(loginPage.errorMessages).not().containsText("Password is required");
        assertThat(loginPage.errorMessages).not().containsText("Email is required");
    }

    @Test
    void testValidLoginWithCorrectCredentials() {
        loginPage.inputEmail.fill("login@codility.com");
        loginPage.inputPassword.fill("password");

        loginPage.buttonLogin.click();

        assertThat(loginPage.inputEmail).not().isVisible();
        assertThat(loginPage.inputPassword).not().isVisible();
        assertThat(loginPage.buttonLogin).not().isVisible();

        assertThat(loginPage.successMessage).containsText("Welcome to Codility");
    }
}
