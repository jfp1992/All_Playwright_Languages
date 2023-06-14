// playwright-dev-page.ts
import { expect, Locator, Page } from '@playwright/test';

export class LoginPage {
    readonly page: Page;
    readonly url: string;
    readonly wrapperLoginForm:Locator;
    readonly inputEmail:Locator;
    readonly inputPassword:Locator;
    readonly buttonLogin:Locator;
    readonly errorMessages:Locator;
    readonly successMessage:Locator;
  
    constructor(page: Page) {
        this.page = page;
        this.url = "https://codility-frontend-prod.s3.amazonaws.com/media/task_static/python_selenium_login_page/9a83bda125cd7398f9f482a3d6d45ea4/static/attachments/reference_page.html";
        this.wrapperLoginForm = page.locator("#login-form");
        this.inputEmail = page.locator("#email-input");
        this.inputPassword = page.locator("#password-input");
        this.buttonLogin = page.locator("#login-button");
        this.errorMessages = page.locator("#messages");
        this.successMessage = page.locator(".message.success");
    }
  }
  