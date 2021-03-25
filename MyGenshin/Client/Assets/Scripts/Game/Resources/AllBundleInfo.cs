using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class AllBundleInfo
{
    public static string Name = "AssetInfo.json";
    public Dictionary<string, SingleBundleInfo> Infos = new Dictionary<string, SingleBundleInfo>();
}





[Serializable]
public class SingleBundleInfo
{
    public string Name;

    public string MD5;

    public long Size;
}

