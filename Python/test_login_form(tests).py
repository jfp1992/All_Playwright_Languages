from playwright.sync_api import Page, expect
from objects import LoginForm


def test_login_form_stable_state(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    expect(login_form.login_form).to_be_visible()

    expect(login_form.input_email).to_be_visible()
    expect(login_form.input_email).to_have_attribute("placeholder", "e-mail")

    expect(login_form.input_password).to_be_visible()
    expect(login_form.input_password).to_have_attribute("placeholder", "password")

    expect(login_form.button_login).to_be_visible()
    expect(login_form.button_login).to_have_text("Login")


def test_invalid_login_empty(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("Email is required")
    expect(login_form.error_messages).to_contain_text("Password is required")


def test_invalid_login_incorrect_valid_email_only(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("fake.email@fakeemail.com")

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("Password is required")
    expect(login_form.error_messages).not_to_contain_text("Email is required")


def test_invalid_login_correct_valid_email_only(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("login@codility.com")

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("Password is required")
    expect(login_form.error_messages).not_to_contain_text("Email is required")


def test_invalid_login_correct_valid_email_with_incorrect_password(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("login@codility.com")
    login_form.input_password.fill("p4ssword")

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("You shall not pass! Arr!")
    expect(login_form.error_messages).not_to_contain_text("Password is required")
    expect(login_form.error_messages).not_to_contain_text("Email is required")


def test_valid_login_with_correct_credentials(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("login@codility.com")
    login_form.input_password.fill("password")

    login_form.button_login.click()

    expect(login_form.input_email).not_to_be_visible()
    expect(login_form.input_password).not_to_be_visible()
    expect(login_form.button_login).not_to_be_visible()

    expect(login_form.success_message).to_contain_text("Welcome to Codility")


def test_invalid_login_incorrect_email_format(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("fakeemail@fakeemailcom")

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("Invalid email format")
    expect(login_form.error_messages).not_to_contain_text("Email is required")
    expect(login_form.error_messages).not_to_contain_text("Password is required")


def test_invalid_login_incorrect_password_format(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("fake.email@fakeemail.com")
    login_form.input_password.fill("password")

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("Incorrect password format")
    expect(login_form.error_messages).not_to_contain_text("Email is required")
    expect(login_form.error_messages).not_to_contain_text("Password is required")


def test_invalid_login_incorrect_credentials(page: Page):
    login_form = LoginForm(page)

    page.goto(login_form.url)

    login_form.input_email.fill("fake.email@fakeemail.com")
    login_form.input_password.fill("fakepassword")

    login_form.button_login.click()

    expect(login_form.error_messages).to_contain_text("Invalid email or password")
    expect(login_form.error_messages).not_to_contain_text("Email is required")
    expect(login_form.error_messages).not_to_contain_text("Password is required")
