
class LoginForm:
    url = "https://codility-frontend-prod.s3.amazonaws.com/media/task_static/python_selenium_login_page/9a83bda125cd7398f9f482a3d6d45ea4/static/attachments/reference_page.html"

    login_form = None
    input_email = None
    input_password = None
    button_login = None
    error_messages = None
    success_message = None

    def __init__(self, page):
        self.login_form = page.locator("#login-form")
        self.input_email = page.locator("#email-input")
        self.input_password = page.locator("#password-input")
        self.button_login = page.locator("#login-button")
        self.error_messages = page.locator("#messages")
        self.success_message = page.locator(".message.success")
