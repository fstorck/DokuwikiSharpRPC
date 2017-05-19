using System;
using System.IO;
using System.Threading;
using CookComputing.XmlRpc;

using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using System.Diagnostics;

namespace DokuwikiSharpRPC
{
    public interface IDokuWiki : IXmlRpcProxy
    {
        #region Dokuwiki

        [XmlRpcMethod("dokuwiki.getPagelist")]
        XmlRpcStruct[] GetPageList(string NameSpace, XmlRpcStruct options);

        [XmlRpcMethod("dokuwiki.login")]
        bool Login(string User, string Password);

        [XmlRpcMethod("dokuwiki.getTime")]
        int GetTime();

        [XmlRpcMethod("dokuwiki.search")]
        XmlRpcStruct[] search(string query);

        [XmlRpcMethod("dokuwiki.appendPage")]
        bool appendPage(string pageUrl, string wikiMarkup, XmlRpcStruct options);



        #endregion

        [XmlRpcMethod("wiki.listLinks")]
        object[] listLinks(string pageUrl);

        [XmlRpcMethod("wiki.getPage")]
        string getPage(string pageUrl);

        [XmlRpcMethod("wiki.getPageVersion")]
        string getPageVersion(string pageUrl, int Timestamp);

        [XmlRpcMethod("wiki.getPageVersions")]
        XmlRpcStruct[] getPageVersions(string pageUrl, int offset);

        [XmlRpcMethod("wiki.getPageInfo")]
        XmlRpcStruct getPageInfo(string pageUrl);

        [XmlRpcMethod("wiki.getPageInfoVersion")]
        XmlRpcStruct getPageInfoVersion(string pageUrl, int Timestamp);

        [XmlRpcMethod("wiki.getPageHTML")]
        string getPageHTMLVersion(string pageUrl);

        [XmlRpcMethod("wiki.getPageHTMLVersion")]
        string getPageHTMLVersion(string pageUrl, int Timestamp);

        [XmlRpcMethod("wiki.putPage")]
        bool putPage(string pageUrl, string wikiMarkup, XmlRpcStruct options);

        [XmlRpcMethod("wiki.getAllPages")]
        XmlRpcStruct[] getAllPages();

        [XmlRpcMethod("wiki.getBackLinks")]
        XmlRpcStruct[] getBackLinks(string pageUrl);

        [XmlRpcMethod("wiki.getRecentChanges")]
        XmlRpcStruct[] getRecentChanges(int Timestamp);

        [XmlRpcMethod("wiki.getRecentMediaChanges")]
        XmlRpcStruct[] getRecentMediaChanges(int Timestamp);

        [XmlRpcMethod("wiki.getAttachments")]
        XmlRpcStruct[] getAttachments(string Namespace, XmlRpcStruct options);

        [XmlRpcMethod("wiki.getAttachment")]
        string getAttachment(string ID);

        [XmlRpcMethod("wiki.getAttachmentInfo")]
        XmlRpcStruct getAttachmentInfo(string ID);

        [XmlRpcMethod("wiki.putAttachment")]
        void putAttachment(string ID, byte[] Data, XmlRpcStruct options);

        [XmlRpcMethod("wiki.deleteAttachment")]
        void deleteAttachment(string ID);

        #region RPC Errors

              
        public enum Errors
        {
            //    100 → Page errors
            //        110 → Page access errors
            //            111 → User is not allowed to read the requested page
            //            112 → User is not allowed to edit the page
            //            113 → manager permission is required
            //            114 → superuser permission is required
            //        120 → Page existence errors
            //            121 → The requested page does not exist
            //        130 → Page edit errors
            //            131 → Empty page id
            //            132 → Empty page content
            //            133 → Page is locked
            //            134 → Positive wordblock check
            //    200 → Media errors
            //        210 → Media access errors
            //            211 → User is not allowed to read the requested media
            //            212 → User is not allowed to delete media
            //            215 → User is not allowed to list media
            //        220 → Media existence errors
            //            221 → The requested media does not exist
            //        230 → Media edit errors
            //            231 → Filename not given
            //            232 → File is still referenced
            //            233 → Could not delete file
            //    300 → Search errors
            //        310 → Argument errors
            //            311 → The provided value is not a valid timestamp
            //        320 → Search result errors
            //            321 → No changes in specified timeframe

            Page_Error = 100,
            Page_Access_Error = 110,
            Page_Access_Error_User_Disallow_Read  = 111,
            Page_Access_Error_User_Disallow_Edit = 112,
            Page_Access_Error_User_Manager_Permission_Reqired = 113,
            Page_Access_Error_User_Superuser_Permission_Reqired = 114,
            Page_Existence = 120,
            Page_Existence_NotExist = 121,
            Page_Edit = 130,
            Page_Edit_EmptyID = 131,
            Page_Edit_EmptyContent = 132,
            Page_Edit_Locked = 133,
            Page_Edit_WordblockCheck = 134,
            Media_Error = 200,
            Media_Access = 210,
            Media_Access_Error_User_Disallow_Read = 211,
            Media_Access_Error_User_Disallow_Edit = 212,
            Media_Access_Error_User_Disallow_List = 215,
            Media_Existence = 220,
            Media_Existence_NotExist = 221,
            Media_Edit = 230,
            Media_Edit_NoFilename = 231,
            Media_Edit_FilenameStillReferenced = 232,
            Media_Edit_CouldNotDelete = 233,
            Search_Error = 300,
            Search_Error_ArgumentErr_Timestamp_invalid = 311,
            Search_Error_Result = 320,
            Search_Error_Result_NoChangesFound = 321,
            //Additionally there are some server error codes that indicate some kind of server or XML-RPC failure. The codes are the following:

            //    -32600 → Invalid XML-RPC request. Not conforming to specification.
            //    -32601 → Requested method does not exist.
            //    -32602 → Wrong number of parameters or invalid method parameters.
            //    -32603 → Not authorized to call the requested method (No login or invalid login data was given).
            //    -32604 → Forbidden to call the requested method (but a valid login was given).
            //    -32605 → The XML-RPC API has not been enabled in the configuration
            //    -32700 → Parse Error. Request not well formed.
            //    -32800 → Recursive calls to system.multicall are forbidden.
            //    -99999 → Unknown server error.


            Xmlrpc_InvalidRequest = -32600,
            Xmlrpc_MethodNotExist = -32601,
            Xmlrpc_InvalidParameters = -32602,
            Xmlrpc_NotAuthorized = -32603,
            Xmlrpc_NoCallPermission = -32604,
            Xmlrpc_ApiDisabled = -32605,
            Xmlrpc_ParseError = -32700,
            Xmlrpc_SystemRecursiveCallsForbidden = -32800,
            Xmlrpc_UnknownServerError = -99999
        }

        #endregion 

    }

   

    public class DokuWikiConnector
    {
        public const int RPC_TIMEOUT = 5000;

        IDokuWiki _RPCProxy;
        bool _bConnected = false;

        public IDokuWiki DWInstance { get { return _RPCProxy; } }

        bool _bAcceptInvalidCert = true;
        public bool AcceptInvalidCert
        {
            get { return _bAcceptInvalidCert; }
            set { _bAcceptInvalidCert = value; }
        }

        public bool IsConnected { get { return _bConnected; } }

        public DokuWikiConnector()
        {

            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(CertValidate);


            _RPCProxy = XmlRpcProxyGen.Create<IDokuWiki>();
#if DEBUG
            _RPCProxy.AttachLogger(new XmlRpcDebugLogger());
#endif
            _RPCProxy.RequestEvent += new XmlRpcRequestEventHandler(_RPCProxy_RequestEvent);
            _RPCProxy.ResponseEvent += new XmlRpcResponseEventHandler(_RPCProxy_ResponseEvent);

            _RPCProxy.EnableCompression = true;
        }

        void _RPCProxy_ResponseEvent(object sender, XmlRpcResponseEventArgs args)
        {
            DumpStream(args.ResponseStream);
        }

        void _RPCProxy_RequestEvent(object sender, XmlRpcRequestEventArgs args)
        {
            DumpStream(args.RequestStream);
        }

        private void DumpStream(Stream stm)
        {
            stm.Position = 0;
            TextReader trdr = new StreamReader(stm);
            String s = trdr.ReadLine();
            while (s != null)
            {
                Trace.WriteLine(s);
                s = trdr.ReadLine();
            }
        }

        public bool Connect(string URL, string User, string Password)
        {
            try
            {
                _RPCProxy.Url = URL;
                _RPCProxy.Timeout = RPC_TIMEOUT;
                _bConnected = _RPCProxy.Login(User, Password);
            }
            catch (System.Exception ex)
            {

            }
            return
                _bConnected;

        }

        public void Disconnect()
        {
            _bConnected = false;
        }

        bool CertValidate(Object o, X509Certificate Cert, X509Chain Chain, SslPolicyErrors SslErr)
        {
            string sCertSerial = Cert.GetSerialNumberString();
            string sFingerprint = Cert.GetCertHashString();

            if (SslErr != SslPolicyErrors.None)
            {
                //_Log.WarnFormat("Certificate Error: {0}", Cert.ToString());

                if (_bAcceptInvalidCert)
                {
                    //_Log.WarnFormat("Certificate accepted by override");
                    return
                        true;
                }
            }
            else
                return
                    true;

            return
                false;
        }
    }
}

