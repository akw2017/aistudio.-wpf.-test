using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AIStudio.Wpf.Svg2XamlExtension
{
    internal static class PackSvgDataFactory
    {
        internal static Func<IDictionary<Tuple<string, string>, string>> Create
        {
            get
            {
                return new Func<IDictionary<Tuple<string, string>, string>>(() =>
                {
                    Dictionary<Tuple<string, string>, string> dic = new Dictionary<Tuple<string, string>, string>();
                    try
                    {
                        var files = AddDirFiles("svg", true);
                        foreach (var file in files)
                        {                          
                            if ((file.Item1 == "fill" || file.Item1 == "outline" || file.Item1 == "twotone") && !dic.ContainsKey(new Tuple<string, string>(file.Item1, file.Item2)))
                                dic.Add(new Tuple<string, string>(file.Item1, file.Item2), file.Item3);
                            else if (!dic.ContainsKey(new Tuple<string, string>("", file.Item2)))
                                dic.Add(new Tuple<string, string>("", file.Item2), file.Item3);
                        }
                    }
                    catch { }
                    return dic;
                });
            }
        }          
        

        private static List<Tuple<string, string, string>> AddDirFiles(string dir, bool checkRecusive)
        {
            List<Tuple<string, string, string>> files = new List<Tuple<string, string, string>>();
            DirectoryInfo di = new DirectoryInfo(dir);

            FileInfo[] fis = di.GetFiles($"*.svg");//文件类型
            foreach (FileInfo fi in fis)
                files.Add(new Tuple<string, string, string>(di.Name, System.IO.Path.GetFileNameWithoutExtension(fi.FullName), fi.FullName));

            if (checkRecusive)//如果选择遍历所有文件
            {
                DirectoryInfo[] dis = di.GetDirectories();//目录下的子目录
                foreach (DirectoryInfo item in dis)
                    files.AddRange(AddDirFiles(item.FullName, checkRecusive));
            }

            return files;
        }
    }
}
