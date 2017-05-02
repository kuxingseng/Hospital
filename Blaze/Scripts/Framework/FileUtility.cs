namespace Blaze.Framework
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// 提供与文件系统相关的常用功能。
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// 复制文件夹。
        /// </summary>
        /// <param name="srcDir">源文件夹</param>
        /// <param name="destDir">目标文件夹</param>
        /// <param name="copySubDirectories">是否复制子文件夹</param>
        /// <param name="overwrite">是否覆盖</param>
        public static void CopyDirectory(string srcDir, string destDir, bool copySubDirectories, bool overwrite = false)
        {
            var dir = new DirectoryInfo(srcDir);
            var dirs = dir.GetDirectories();

            if (!dir.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + srcDir);

            //确保文件夹存在
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            //复制所有文件
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDir, file.Name);
                if (overwrite && File.Exists(temppath))
                    file.CopyTo(temppath, true);
                else
                    file.CopyTo(temppath);
            }

            //复制子文件夹
            if (!copySubDirectories)
                return;
            foreach (var subdir in dirs)
            {
                var tempPath = Path.Combine(destDir, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath, true, overwrite);
            }
        }

        /// <summary>
        /// 删除文件夹，若文件夹内包含只读属性的文件或文件夹，则会取消只读后删除。
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        public static void DeleteDirectory(string directory)
        {
            var rootDirInfo = new DirectoryInfo(directory);
            rootDirInfo.Attributes = removeReadOnly(rootDirInfo.Attributes);

            foreach (var file in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Attributes = removeReadOnly(fileInfo.Attributes);
            }
            foreach (var dir in Directory.GetDirectories(directory, "*", SearchOption.AllDirectories))
            {
                var dirInfo = new DirectoryInfo(dir);
                dirInfo.Attributes = removeReadOnly(dirInfo.Attributes);
            }

            Directory.Delete(directory, true);
        }

        /// <summary>
        /// 编辑文件内容
        /// </summary>
        /// <param name="changeDict"></param>
        /// <param name="path"></param>
        public static void EditFile(Dictionary<string,string> changeDict , string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8"));
            //StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GB2312"));
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string line = sr.ReadLine();
            StringBuilder sb = new StringBuilder();
            while (line != null)
            {
                for (int j = 0; j < changeDict.Count; j++)
                {
                    if (line.Contains(changeDict.ToArray()[j].Key))
                    {
                        line = changeDict.ToArray()[j].Value;
                        break;
                    }   
                }
                sb.Append(line + "\r\n");
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            File.Delete(path);  //先删除，否则文件后面会多出“}”
            FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("utf-8"));
            //StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("GB2312"));
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
            fs1.Close();
        }

        private static FileAttributes removeReadOnly(FileAttributes attributes)
        {
            return attributes & (~FileAttributes.ReadOnly);
        }
    }
}