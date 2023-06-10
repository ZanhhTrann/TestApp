using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Threading;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
using TestStack.White;
using TestStack.White.UIItems;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace APP_DACS_QLKTX.Methods.Test
{
    [TestFixture]
    public class YourTest
    {
        [Test]
        public void YourTestMethod()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var projectBasePath = Path.GetDirectoryName(assemblyPath);
            var applicationPath = Path.Combine(projectBasePath, @"D:\File\APP_DACS_QLKTX\APP_DACS_QLKTX\obj\Debug", "APP_DACS_QLKTX.exe");

            var application = Application.Launch(applicationPath);
            var login = application.GetWindow(SearchCriteria.ByText("Login"), InitializeOption.NoCache);

            // Kiểm tra sự hiện diện của các thành phần trên cửa sổ đăng nhập
            var usernameTextBox = login.Get<TextBox>(SearchCriteria.ByAutomationId("tb_id"));
            var passwordTextBox = login.Get<TextBox>(SearchCriteria.ByAutomationId("_tb_pass"));
            var loginButton = login.Get<Button>(SearchCriteria.ByAutomationId("_bt_Login"));

            Assert.NotNull(usernameTextBox);
            Assert.NotNull(passwordTextBox);
            Assert.NotNull(loginButton);

            // Thực hiện kiểm tra đăng nhập
            usernameTextBox.Text = "testuser";
            passwordTextBox.Text = "testpassword";
            loginButton.Click();
            System.Threading.Thread.Sleep(1000);
            // Đợi cho cửa sổ thông báo xuất hiện
            var messageBoxWindow = login.ModalWindow("Thông báo");
            Assert.NotNull(messageBoxWindow);

            // Tìm nút "OK" trong cửa sổ thông báo
            var okButton = messageBoxWindow.Get<Button>(SearchCriteria.ByText("OK"));

            // Nhấn nút "OK"
            okButton.Click();


            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            loginButton.Click();
            System.Threading.Thread.Sleep(1000);
            messageBoxWindow = login.ModalWindow("Thông báo");
            Assert.NotNull(messageBoxWindow);
            okButton = messageBoxWindow.Get<Button>(SearchCriteria.ByText("OK"));
            okButton.Click();

            usernameTextBox.Text = "nv03";
            passwordTextBox.Text = "";
            loginButton.Click();
            System.Threading.Thread.Sleep(1000);
            messageBoxWindow = login.ModalWindow("Thông báo");
            Assert.NotNull(messageBoxWindow);
            okButton = messageBoxWindow.Get<Button>(SearchCriteria.ByText("OK"));
            okButton.Click();

            // kiem tra so lan nhap sai mat khau 
            for (int i = 0; i < 5; i++)
            {
                usernameTextBox.Text = "nv02";
                passwordTextBox.Text = "1";
                loginButton.Click();
                System.Threading.Thread.Sleep(1000);
                messageBoxWindow = login.ModalWindow("Thông báo");
                Assert.NotNull(messageBoxWindow);
                okButton = messageBoxWindow.Get<Button>(SearchCriteria.ByText("OK"));
                okButton.Click();
            }

            usernameTextBox.Text = "nv01";
            passwordTextBox.Text = "0";
            loginButton.Click();
            Thread.Sleep(10000);
            messageBoxWindow = login.ModalWindow("Thông báo");
            Assert.NotNull(messageBoxWindow);
            okButton = messageBoxWindow.Get<Button>(SearchCriteria.ByText("OK"));
            okButton.Click();
            application.Close();
        }
    }
}
