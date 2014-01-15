using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace TaskDemo
{
  public  class HttpDemo
    {
        /// <summary>
        /// 测试IP地址
        /// </summary>
        public static void Test6()
        {
            IPAddress a1 = new IPAddress(new byte[] { 101, 102, 103, 104 });
            IPAddress a2 = IPAddress.Parse("101.102.103.104");
            Console.WriteLine(a1.Equals(a2));
            Console.WriteLine(a1.AddressFamily);
            IPAddress a3 = IPAddress.Parse
  ("[3EA0:FFFF:198A:E4A3:4FF2:54fA:41BC:8D31]");

            IPEndPoint ep = new IPEndPoint(a1, 222);

            Console.WriteLine(a3.AddressFamily);

            Console.WriteLine(ep.ToString());
        }

        /// <summary>
        /// 测试WebClient
        /// </summary>
        public async static void Test7()
        {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            //WebProxy wp = new WebProxy(new Uri("http://127.0.0.1:8888"));
            //wc.Proxy = wp;

            //var credients =new NetworkCredential()

            wc.Headers.Add("Referer", "www.baidu.com");
            wc.Headers.Add("CustomHeader", "JustPlaying/1.0");



            //加上进度报告
            wc.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage + "% complete");
            await wc.DownloadFileTaskAsync("http://www.baidu.com", "code.htm");
            foreach (string name in wc.ResponseHeaders.Keys)
            {
                Console.WriteLine(name + "=" + wc.ResponseHeaders[name]);
            }
            System.Diagnostics.Process.Start("code.htm");
        }

        /// <summary>
        /// 测试cookies
        /// </summary>
        public static void Test8()
        {
            var cc = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");
            request.CookieContainer = cc;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie c in response.Cookies)
                {
                    Console.WriteLine(" Name:   " + c.Name);
                    Console.WriteLine(" Value:  " + c.Value);
                    Console.WriteLine(" Path:   " + c.Path);
                    Console.WriteLine(" Domain: " + c.Domain);
                }
            }
            foreach (Cookie c in cc.GetCookies(new Uri("http://www.baidu.com")))
            {
                Console.WriteLine(" Name:   " + c.Name);
                Console.WriteLine(" Value:  " + c.Value);
                Console.WriteLine(" Path:   " + c.Path);
                Console.WriteLine(" Domain: " + c.Domain);
            }

        }

        /// <summary>
        /// 测试登录保存cookies,并使用https
        /// </summary>
        public static void Test9()
        {
            string url = "https://passport.csdn.net/ajax/accounthandler.ashx?t=log&u=newlifeinsc&p=sxf123456&remember=0&f=http%3A%2F%2Fwww.csdn.com%2F&rand=0.4897006042301655";


            var cc = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cc;
            request.UserAgent = " Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            request.Referer = "www.baidu.com";
            //request.Headers.Add("Referer:http://www.baidu.com");
            using (var response = (HttpWebResponse)request.GetResponse())
                foreach (Cookie c in response.Cookies)
                    Console.WriteLine(c.Name + " = " + c.Value);
            // string postData = "staticpage=http%3A%2F%2Fwww.baidu.com%2Fcache%2Fuser%2Fhtml%2Fv3Jump.html&charset=UTF-8&tpl=mn&apiver=v3&tt=1389771363136&codestring=&safeflg=0&u=http%3A%2F%2Fwww.baidu.com%2F&isPhone=false&quick_user=0&loginmerge=true&logintype=dialogLogin&username=newlifeinsc&password=sxf123456&verifycode=&mem_pass=on&ppui_logintime=14399&callback=parent.bd__pcbs__jgzx03";
        }

        async static void ListenAsync()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:51111/MyApp/");
            listener.Start();

            //Await a client request:
            HttpListenerContext context = await listener.GetContextAsync();

            //Respond to the request
            string msg = "Your asked for: " + context.Request.RawUrl;
            context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            using (Stream s = context.Response.OutputStream)
            using (StreamWriter writer = new StreamWriter(s))
            {
                await writer.WriteAsync(msg);
            }
            listener.Stop();

        }

        /// <summary>
        /// 测试ListnerAsync
        /// </summary>
        public static void Test10()
        {
            ListenAsync();                           // Start server
            WebClient wc = new WebClient();          // Make a client request.
            Console.WriteLine(wc.DownloadString
              ("http://localhost:51111/MyApp/Request.txt"));
        }

        public static void Test11()
        {
            var server = new WebServer("http://localhost:51111/", @"E:\codes_practice");
            try
            {
                server.Start();
                Console.WriteLine("Server running... press Enter to stop");
                Console.ReadLine();
            }
            finally
            {
                server.Stop();
            }
        }

    }
}
