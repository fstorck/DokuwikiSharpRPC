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

    }

    public class DokuWikiConnector
    {
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
                _RPCProxy.Timeout = 5000;
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

