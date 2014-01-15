using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
namespace TaskDemo
{
    class WebServer
    {
        HttpListener _listener;
        string _baseFolder;

        public WebServer(string uriPrefix, string baseFolder)
        {
            _listener =new HttpListener();
            _listener.Prefixes.Add(uriPrefix);
            _baseFolder = baseFolder;
        }

        public async void Start()
        {
            _listener.Start();
            while (true)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    Task.Run(() => ProcessRequestAsync(context));
                }
                catch (HttpListenerException) { break; }   // Listener stopped.
                catch (InvalidOperationException) { break; }   // Listener stopped.
            }
        }



        public void Stop() { _listener.Stop(); }

        private async void ProcessRequestAsync(HttpListenerContext context)
        {
 	       try
 	       {
               string filename ;

               //用于创建允许读取有路径和带"?"的url指定的文件
               //string filename = Path.GetFileName(context.Request.RawUrl);
               //var endIndex = context.Request.RawUrl.IndexOf("?");
               //var filenameLength = 0;
               // if (endIndex>0)
               // {
               //     filenameLength = endIndex - 1;
               // }
               // else
               // {
               //     filenameLength = context.Request.RawUrl.Length - 1;
               // }

                filename = context.Request.Url.LocalPath.Substring(1);
               

               string path = Path.Combine(_baseFolder, filename);
               byte[] msg;
               if (!File.Exists(path))
               {
                   Console.WriteLine("Resource not found: " + path);
                   context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                   msg = Encoding.UTF8.GetBytes("Sorry, that page does not exist");
               }
               else
               {
                   context.Response.StatusCode = (int)HttpStatusCode.OK;
                   msg = File.ReadAllBytes(path);
               }

               context.Response.ContentLength64 = msg.Length;
               using (Stream s = context.Response.OutputStream)
                   await s.WriteAsync(msg, 0, msg.Length);
 	       }
 	       catch (System.Exception ex)
 	       {
               Console.WriteLine("Request error: " + ex); 
 	       }
        }
    }
}
