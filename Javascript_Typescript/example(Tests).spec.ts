import { test, expect, Page } from '@playwright/test';
import { LoginPage } from "../objects/login_page";

test.beforeEach(async ({ page }) => {
  await page.goto('https://playwright.dev/');
})

test('Login form state from load', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await page.goto(loginPage.url);
  await expect(loginPage.wrapperLoginForm).toBeVisible();

  await expect(loginPage.inputEmail).toBeVisible();
  await expect(loginPage.inputEmail).toHaveAttribute("placeholder", "e-mail");

  await expect(loginPage.inputPassword).toBeVisible();
  await expect(loginPage.inputPassword).toHaveAttribute("placeholder", "password");

  await expect(loginPage.buttonLogin).toBeVisible();
  await expect(loginPage.buttonLogin).toHaveText("Login");
});

test('Invalid login - empty fields', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await page.goto(loginPage.url);

  await loginPage.buttonLogin.click();

  await expect(loginPage.errorMessages).toHaveText("Email is required");
  await expect(loginPage.errorMessages).toContainText("Password is required");
});

test('Invalid login - incorrect but valid email only', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await page.goto(loginPage.url);

  await loginPage.inputEmail.fill("fake.email@fakeemail.com");

  await loginPage.buttonLogin.click();

  await expect(loginPage.errorMessages).toHaveText("Password is required");
  await expect(loginPage.errorMessages).not.toContainText("Email is required");
});

test('Invalid login - correct email only', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await page.goto(loginPage.url);

  await loginPage.inputEmail.fill("login@codility.com");

  await loginPage.buttonLogin.click();

  await expect(loginPage.errorMessages).toContainText("Password is required");
  await expect(loginPage.errorMessages).not.toContainText("Email is required");
});

test('Invalid login - correct email with incorrect password', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await page.goto(loginPage.url);

  await loginPage.inputEmail.fill("login@codility.com");
  await loginPage.inputPassword.fill("p4ssword");

  await loginPage.buttonLogin.click();

  await expect(loginPage.errorMessages).toContainText("You shall not pass! Arr!");
  await expect(loginPage.errorMessages).not.toContainText("Password is required");
  await expect(loginPage.errorMessages).not.toContainText("Email is required");
});

test('Valid login - correct email with correct password', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await page.goto(loginPage.url);

  await loginPage.inputEmail.fill("login@codility.com");
  await loginPage.inputPassword.fill("password");

  await loginPage.buttonLogin.click();

  await expect(loginPage.inputEmail).not.toBeVisible();
  await expect(loginPage.inputPassword).not.toBeVisible();
  await expect(loginPage.buttonLogin).not.toBeVisible();

  await expect(loginPage.successMessage).toContainText("Welcome to Codility");
});