using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Threading;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using System;
using TestStack.White.UIItems.WPFUIItems;
using APP_DACS_QLKTX.Views;

namespace APP_DACS_QLKTX.Methods.Test
{
    [TestFixture]
    public class YourTest
    {
        public YourTest(){

        }
        [Test] 
        public void YourTestMethod()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var projectBasePath = Path.GetDirectoryName(assemblyPath);
            var applicationPath = Path.Combine(projectBasePath, @"D:\File\Test_App_QLKTX\APP_DACS_QLKTX\obj\Debug\APP_DACS_QLKTX.exe");
            var application = Application.Launch(applicationPath);
            var login = application.GetWindow(SearchCriteria.ByText("Login"), InitializeOption.NoCache);
            Login(application, "test", "testpass");
            CloseMessageBox(login);

            //Để chống thông tin đăng nhập 
            Login(application, "", "");
            CloseMessageBox(login);
            //Để chống mật khẩu 
            Login(application, "nv03", "");
            CloseMessageBox(login);
            //Đăng nhập với mật khẩu sai cho tới khi tài khoản bị khóa 
            // kiem tra so lan nhap sai mat khau 
            for (int i = 0; i < 6; i++)
            {
                Login(application, "nv03", "wronngpass");
                CloseMessageBox(login);
            }
            Thread.Sleep(1000);
            Login(application, "nv01", "0");
            Logout(application);

            // Đăng nhập với tài khoản mới cấp và thay đổi mật khẩu
            Thread.Sleep(3000);
            Login(application, "nv07", "&0b3");
            changePassWord(application, "", "", true);

            changePassWord(application, "aaa", "", true);
            changePassWord(application, "aaaaaaaaa", "", true);
            changePassWord(application, "aaaaaaaaa", "a", true);
            changePassWord(application, "aaaaaaaaa", "aaaaaaaaa", true);
            Logout(application);
            Login(application, "nv07", "aaaaaaaaa");
            Logout(application);
            //changePassWord(application, "", "", false);

            Login(application, "nv01", "0");
            Logout(application);

            application.Close();
        }

        public void CloseMessageBox(Window window)
        {
            var messageBoxWindow = window.ModalWindow("Thông báo");
            Assert.NotNull(messageBoxWindow);

            // Tìm nút "OK" trong cửa sổ thông báo
            var okButton = messageBoxWindow.Get<Button>(SearchCriteria.ByText("OK"));

            // Nhấn nút "OK"
            okButton.Click();

            // Đợi cho cửa sổ thông báo đóng hoàn toàn
            window.WaitWhileBusy();
        }

        public void Login(Application application, String id,String pass) {
            var login = application.GetWindow(SearchCriteria.ByText("Login"), InitializeOption.NoCache);
            var usernameTextBox = login.Get<TextBox>(SearchCriteria.ByAutomationId("tb_id"));
            var passwordTextBox = login.Get<TextBox>(SearchCriteria.ByAutomationId("_tb_pass"));
            var loginButton = login.Get<Button>(SearchCriteria.ByAutomationId("_bt_Login"));
            Assert.NotNull(usernameTextBox);
            Assert.NotNull(passwordTextBox);
            Assert.NotNull(loginButton);
            usernameTextBox.Text = id;
            passwordTextBox.Text = pass;
            loginButton.Click();
            Thread.Sleep(1000);
        }
        public void Logout(Application application) {
            var windows = application.GetWindows();
            var menuWindow = windows.Find(x => x.Title.Contains("Menu"));
            Assert.NotNull(menuWindow);
            // Lấy TabControl từ cửa sổ menu
            var tabControl = menuWindow.Get(SearchCriteria.ByAutomationId("control"));
            // Tìm TabItem trong TabControl
            var tabItemToSelect = tabControl.Get(SearchCriteria.ByAutomationId("ct_ht"));
            // Chọn TabItem "Hệ thống"
            tabItemToSelect.Click();
            // Chọn TabItem mong muốn
            var logoutbt = tabItemToSelect.Get<Button>(SearchCriteria.ByAutomationId("bt_logout"));
            Assert.NotNull(logoutbt);
            logoutbt.Click();
        }

        private void changePassWord(Application application,String pass,String repass,bool change)
        {
            var windows = application.GetWindows();
            var cpw = windows.Find(x => x.Title.Contains("ChangePassword"));
            Assert.NotNull(cpw);
            var newPassTextBox = cpw.Get<TextBox>(SearchCriteria.ByAutomationId("pbl_np"));
            var rePass = cpw.Get<TextBox>(SearchCriteria.ByAutomationId("pbl_rnp"));
            var loginButton = cpw.Get<Button>(SearchCriteria.ByAutomationId("bt_accept"));
            var Cancel_Button = cpw.Get<Button>(SearchCriteria.ByAutomationId("bt_cancel"));
            Assert.NotNull(newPassTextBox);
            Assert.NotNull(rePass);
            Assert.NotNull(loginButton);
            Assert.NotNull(Cancel_Button);
            if (change == true)
            {
                newPassTextBox.Text = pass;
                rePass.Text = repass;
                loginButton.Click();
                Thread.Sleep(2000);
                if ((pass != repass)||pass==""||repass=="")
                {
                    CloseMessageBox(cpw);
                }
            }
            else
            {
                Cancel_Button.Click();
            }
            
        }

    }
}
