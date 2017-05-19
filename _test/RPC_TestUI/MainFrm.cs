using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

using DokuwikiSharpRPC;

using CookComputing.XmlRpc;

namespace RPC_TestUI
{
    public partial class MainFrm : Form
    {
        public const string FMT_TIMESTAMP_HIGHRES = "yyyyMMdd_HHmmss_fff";
        DokuWikiConnector _Connector;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _Connector = new DokuWikiConnector();

        }

        private void BTN_Connect_Click(object sender, EventArgs e)
        {

            if (_Connector.Connect(TBX_Url.Text, TBX_User.Text, TBX_Password.Text))
            {
                // object   info = _Connector.DWInstance.getPageInfo("intern:index");
                // object[]   pg = _Connector.DWInstance.getPageVersions("intern:index", 0);

                XmlRpcStruct xp = new XmlRpcStruct();
                xp.Add("depth", 0);
                xp.Add("listdirs", true);
                object[] pges = _Connector.DWInstance.GetPageList("lictracking", xp);

                Console.WriteLine("Test");
            }
        }

        private void BTN_PutPages_Click(object sender, EventArgs e)
        {
            if (_Connector.Connect(TBX_Url.Text, TBX_User.Text, TBX_Password.Text))
            {
                XmlRpcStruct xp = new XmlRpcStruct();
                xp.Add("sum", "");
                xp.Add("minor", "");

                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string url = string.Format("rpctest:{0}", DateTime.Now.ToString(FMT_TIMESTAMP_HIGHRES));
                        string pg = string.Format("===== Test {0} =====", i);

                        _Connector.DWInstance.putPage(url, pg, xp);

                    }
                }
                catch (System.Exception ex)
                {

                }   
            }
        }

       
        private void BTN_UploadTest_Click(object sender, EventArgs e)
        {
            if(OFD_Upload.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(OFD_Upload.FileName);
                if(fi.Length < 20 * 1024 * 1024)
                {
                    byte[] buf = File.ReadAllBytes(OFD_Upload.FileName);
                    if (_Connector.Connect(TBX_Url.Text, TBX_User.Text, TBX_Password.Text))
                    {
                        XmlRpcStruct xp = new XmlRpcStruct();
                        xp.Add("ow", true);
                        string ID      = string.Format("rpctest:{0}", OFD_Upload.SafeFileName);
                       
 
                        try
                        {
                            _Connector.DWInstance.putAttachment(ID, buf, xp);
                        }
                        catch (System.Exception ex)
                        {

                        }
                    }
                }

            }
        }
    }
}
