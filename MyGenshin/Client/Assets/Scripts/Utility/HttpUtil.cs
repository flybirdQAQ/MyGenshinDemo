using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;



public class DownloadEnum
{
    public string Url;
    public string Path;
    public Action callback;
}
public static class HttpUtil
{

    public static void Downloader(object obj)
    {
        FileStream fs = null;
        Stream stream = null;
        try
        {
            DownloadEnum download = (DownloadEnum)obj;
            Debug.Log($"开始下载{download.Url}");
            if (File.Exists(download.Path))
            {
                File.Delete(download.Path);
            }
            string tempFile = download.Path + ".temp";
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }

            fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

            HttpWebRequest request = WebRequest.CreateHttp(download.Url);
            request.Method = "GET";
            request.Timeout = 1000;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            stream = response.GetResponseStream();


            byte[] data = new byte[1024];
            int size = stream.Read(data, 0, data.Length);
            while (size > 0)
            {
                fs.Write(data, 0, size);
                size = stream.Read(data, 0, data.Length);

            }
            fs.Close();
            stream.Close();
            File.Move(tempFile, download.Path);
            download.callback();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
            }
            if (stream != null)
            {
                stream.Close();
            }
        }

    }


}

