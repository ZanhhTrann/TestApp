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
using NUnit.Framework.Constraints;

namespace APP_DACS_QLKTX.Methods.Test.Menu
{
    [TestFixture]
    public class TestMenu
    {
        delegate void MyDelegate(IUIItem tab, String[] list);
        [Test]
        public void testMenu()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var projectBasePath = Path.GetDirectoryName(assemblyPath);
            var applicationPath = Path.Combine(projectBasePath, @"D:\File\Test_App_QLKTX\APP_DACS_QLKTX\obj\Debug\APP_DACS_QLKTX.exe");
            var application = Application.Launch(applicationPath);
            YourTest test = new YourTest();

            //Tim kiem
            test.Login(application, "nv01", "0");
            //
            Search(application, "sv", "2002");
            Search(application, "sv", "0");
            //
            Search(application, "k", "cntt");
            Search(application, "k", "test");
            //
            Search(application, "tktx", "a");
            Search(application, "tktx", "test");
            //
            Search(application, "pktx", "a101");
            Search(application, "pktx", "test");
            //
            Search(application, "hopd", "hd0001");
            Search(application, "hopd", "test");
            //
            Search(application, "hoad", "hd00001");
            Search(application, "hoad", "test");
            //
            Search(application, "csvc", "csvc01");
            Search(application, "csvc", "test");
            //
            Search(application, "dgdv", "dv01");
            Search(application, "dgdv", "test");
            //
            Search(application, "pcsvc", "a101");
            Search(application, "pcsvc", "test");
            //
            Search(application, "hddg", "hd00025");
            Search(application, "hddg", "test");
            //
            //
            String []svList = {"02","danh","","","","","","","","",""};
            String []hoadList = {"test","test","","","","","","","","",""};
            String []hopdList = {"test","2002","a101","1/1/2020","1/1/2020"};
            String []csvcList = {"test","test","","","","","","","","",""};
            String []dgdvList = {"test","test","10000","","","","","","","",""};
            String []pcsvcList = {"a101","csbc01","10","","","","","","","",""};
            String []hddvList = {"hd00001","dv01","","","","","","","","",""};
            String[] kList = { "ctest", "test"};
            String[] tList = { "ctest", "10",""};
            String[] pList = { "ctest","a", "0",""};
            
            try
            {
                //Sinh vien
                Update(application,"sv", "2002",true,svList);
                //Khoa
                Update(application, "k", "2002", true, kList);
                //Toa ktx
                Update(application, "tktx", "2002", true, tList);
                //Phong ktx
                Update(application, "pktx", "2002", true, pList);
                
                hopdList[4] = "";
            Update(application, "hopd", "2002", true, hopdList);
            Update(application, "hoad", "2002", true, hoadList);
            Update(application, "csvc", "2002", true, csvcList);
            Update(application, "dgdv", "2002", true, dgdvList);
            }
            catch (Exception e)
            {
                Thread.Sleep(3000);
                application.Close();
            }


        }
        public void Search(Application application,String tabitem,String search)
        {
            var windows = application.GetWindows();
            var menuWindow = windows.Find(x => x.Title.Contains("Menu"));
            Assert.NotNull(menuWindow);
            // Lấy TabControl từ cửa sổ menu
            // Tìm TabItem trong TabControl
            var tabItemToSelect = menuWindow.Get(SearchCriteria.ByAutomationId("ct_tk"));
            var tabSearch = menuWindow.Get(SearchCriteria.ByAutomationId("tv_"+tabitem));
            // Chọn TabItem "Hệ thống"
            tabItemToSelect.Click();
            Thread.Sleep(1000);
            tabSearch.Click();
            Thread.Sleep(1000);
            // Chọn TabItem mong muốn
            var textSearch = tabSearch.Get<TextBox>(SearchCriteria.ByAutomationId(tabitem+"_search"));
            Assert.NotNull(textSearch);
            textSearch.Text = search;
            Thread.Sleep(1000);
        }
        public void Update(Application application, String tabitem, String search,
            bool add,String []list
            )
        {
            var windows = application.GetWindows();
            var menuWindow = windows.Find(x => x.Title.Contains("Menu"));
            Assert.NotNull(menuWindow);
            // Tìm TabItem trong TabControl
            var tabItemToSelect = menuWindow.Get(SearchCriteria.ByAutomationId("ct_cn"));
            Assert.NotNull(tabItemToSelect);
            tabItemToSelect.Click();
            var tabSearch = menuWindow.Get(SearchCriteria.ByAutomationId("cn_" + tabitem));
            Assert.NotNull(tabSearch);
            // Chọn TabItem "Hệ thống"
            
            Thread.Sleep(1000);
            tabSearch.Click();
            Thread.Sleep(1000);
            // Chọn TabItem mong muốn
            var textSearch = tabSearch.Get<TextBox>(SearchCriteria.ByAutomationId("ud_"+tabitem + "_search"));
            var btSearch = tabSearch.Get<Button>(SearchCriteria.ByAutomationId("ud_" + tabitem + "_search_bt"));
            var btChange = tabSearch.Get<Button>(SearchCriteria.ByAutomationId("ud_" + tabitem + "_change"));
            var btAdd = tabSearch.Get<Button>(SearchCriteria.ByAutomationId("ud_" + tabitem + "_add"));
            var btClearn = tabSearch.Get<Button>(SearchCriteria.ByAutomationId("ud_" + tabitem + "_clearn"));
            var btDelete = tabSearch.Get<Button>(SearchCriteria.ByAutomationId("ud_" + tabitem + "_delete"));
            Assert.NotNull(textSearch);
            Assert.NotNull(btSearch);
            Assert.NotNull(btChange);
            Assert.NotNull(btAdd);
            Assert.NotNull(btDelete);
            Assert.NotNull(btClearn);
            if (add == false)
            {
                textSearch.Text = search;
                btSearch.Click();
            }
            else
            {
                MyDelegate myDelegate=null;
                switch (tabitem)
                {
                    case "sv":
                        myDelegate = SinhVien;
                        break;
                    case "k":
                        myDelegate = Khoa;
                        break;
                    case "tktx":
                        myDelegate = Toa;
                        break;
                    case "pktx":
                        myDelegate = Phong;
                        break;
                    case "hopd":
                        myDelegate = HopDong;
                        break;
                    case "hoad":
                        myDelegate = HoaDon;
                        break;
                    case "csvc":
                        myDelegate = SCVC;
                        break;
                    case "dgdv":
                        myDelegate = DGDV;
                        break;
                        default:
                        myDelegate = null;
                        break;
                }
                myDelegate(tabSearch, list);
                Thread.Sleep(1000);
                btAdd.Click();
                Thread.Sleep(1000);
                CloseMessageBox(application);
                Thread.Sleep(1000);
                btClearn.Click();
                Thread.Sleep(1000);
                textSearch.Text = list[0];
                Thread.Sleep(1000);
                btSearch.Click();
                Thread.Sleep(1000);
                switch (tabitem)
                {
                    case "sv":
                        list[2] = "10/07/2002";
                        break;
                    case "k":
                        list[1] = "changeTest";
                        break;
                    case "tktx":
                        list[2] = "changeTest";
                        break;
                    case "pktx":
                        list[3] = "changeTest";
                        break;
                    case "hopd":
                        list[3] = "10/02/2023";
                        list[4] = "10/02/2023";
                        break;
                    case "hoad":

                        list[6] = "10/02/2023";
                        break;
                    case "csvc":
                        list[1] = "changeTest";
                        break;
                    case "dgdv":
                        list[1] = "changeTest";
                        break;
                    default:
                        break;
                }
                myDelegate(tabSearch, list);
                Thread.Sleep(1000);
                btChange.Click();
                Thread.Sleep(1000);
                CloseMessageBox(application);
                Thread.Sleep(1000);
                btDelete.Click();
                Thread.Sleep(1000);
                CloseMessageBox(application);
            }
            Thread.Sleep(1000);
        }
       
        public void CloseMessageBox(Application application)
        {
            var windows = application.GetWindows();
            var ThongBao = windows.Find(x => x.Title.Contains("Thông báo"));
            Assert.NotNull(ThongBao);
            // Tìm nút "OK" trong cửa sổ thông báo
            var okButton = ThongBao.Get<Button>(SearchCriteria.ByText("OK"));
            // Nhấn nút "OK"
            okButton.Click();
        }

        
        void SinhVien(IUIItem tab,String[]list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_msv"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_name"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_dob"));
            var textGender = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_g"));
            var textDc = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_dc"));
            var textSDT = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_sdt"));
            var textkhoa = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_mk"));
            var textNameTn = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_pname"));
            var textSDTTn = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_sdt_p"));
            var textQh = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_qh"));
            var textSLKL = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sv_slkl"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            Assert.NotNull(textGender);
            Assert.NotNull(textDc);
            Assert.NotNull(textSDT);
            Assert.NotNull(textkhoa);
            Assert.NotNull(textNameTn);
            Assert.NotNull(textSDTTn);
            Assert.NotNull(textQh);
            Assert.NotNull(textSLKL);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];
            textGender.Text = list[3];
            textDc.Text = list[4];
            textSDT.Text = list[5];
            textkhoa.Text = list[6];
            textNameTn.Text = list[7];
            textSDTTn.Text = list[8];
            textQh.Text = list[9];
            textSLKL.Text = list[10];

        }

        void Khoa(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_k_mk"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_k_name"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];

        }

        void Toa(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_tktx_mt"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_tktx_slp"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_tktx_tt"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];

        }

        void Phong(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pktx_mp"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pktx_mt"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pktx_slsv"));
            var textGender = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pktx_tt"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            Assert.NotNull(textGender);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];
            textGender.Text = list[3];

        }

        void HopDong(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hopd_mhd"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hopd_msv"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hopd_mp"));
            var textGender = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hopd_nlhd"));
            var textDc = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hopd_hhd"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            Assert.NotNull(textGender);
            Assert.NotNull(textDc);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];
            textGender.Text = list[3];
            textDc.Text = list[4];

        }

        void HoaDon(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_mhd"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_mp"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_sdd"));
            var textGender = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_sdc"));
            var textDc = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_snd"));
            var textSDT = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_snc"));
            var textkhoa = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_nlhd"));
            var textNameTn = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hoad_tt"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            Assert.NotNull(textGender);
            Assert.NotNull(textDc);
            Assert.NotNull(textSDT);
            Assert.NotNull(textkhoa);
            Assert.NotNull(textNameTn);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];
            textGender.Text = list[3];
            textDc.Text = list[4];
            textSDT.Text = list[5];
            textkhoa.Text = list[6];
            textNameTn.Text = list[7];

        }

        void SCVC(IUIItem tab, String[] list)
        {
            var textM = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_csvc_mcsvc"));
            var textTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_csvc_name"));
            Assert.NotNull(textM);
            Assert.NotNull(textTen);
            textM.Text = list[0];
            textTen.Text = list[1];

        }

        void DGDV(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_dgdv_mdg"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_dgdv_name"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_dgdv_pdg"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];

        }

        void PCSVC(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pcsvc_mp_search"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pcsvc_mcsvc_search"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pcsvc_mp"));
            var textGender = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pcsvc_mcsvc"));
            var textDc = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pcsvc_sl"));
            var textSDT = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_pcsvc_mp_tt"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            Assert.NotNull(textGender);
            Assert.NotNull(textDc);
            Assert.NotNull(textSDT);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];
            textGender.Text = list[3];
            textDc.Text = list[4];
            textSDT.Text = list[5];

        }

        void HDDV(IUIItem tab, String[] list)
        {
            var textMsv = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hddg_mhd_search"));
            var textHoTen = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hddg_mdv_search"));
            var textDoB = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hddg_mhd"));
            var textGender = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hddg_mhd"));
            var textDc = tab.Get<TextBox>(SearchCriteria.ByAutomationId("ud_hddg_name"));
            Assert.NotNull(textMsv);
            Assert.NotNull(textHoTen);
            Assert.NotNull(textDoB);
            Assert.NotNull(textGender);
            Assert.NotNull(textDc);
            textMsv.Text = list[0];
            textHoTen.Text = list[1];
            textDoB.Text = list[2];
            textGender.Text = list[3];
            textDc.Text = list[4];

        }
    }
}
