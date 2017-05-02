namespace Blaze.Editor.Workflow.Localizations
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using Aspose.Cells;
    using Newtonsoft.Json.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 多语言处理工具，支持将多个Excel语言文件合并导出为配置文件。
    /// </summary>
    public class LocalizationTool
    {
        /// <summary>
        /// 将多语言配置导出到指定的路径。
        /// </summary>
        /// <param name="excelDirectory">配置文件目录路径</param>
        /// <param name="exportFile">导出文件路径</param>
        /// <param name="language">需要导出的语言标识符</param>
        /// <returns>是否导出成功</returns>
        public static bool Export(string excelDirectory, string exportFile, string language)
        {
            if (!Directory.Exists(excelDirectory))
            {
                Debug.LogError("Localization directory not exist -> " + excelDirectory);
                return false;
            }

            var allEntries = new List<LocalizationEntry>();
            var files = Directory.GetFiles(excelDirectory, "*.xlsx", SearchOption.AllDirectories);
            try
            {
                for (var index = 0; index < files.Length; index++)
                {
                    var file = files[index];
                    EditorUtility.DisplayProgressBar("Scanning...", file, (float) index / file.Length);
                    if (file.Contains("~$"))
                    {
                        //Skip temporary files
                        continue;
                    }
                    var workbook = new Workbook(file);
                    var worksheet = workbook.Worksheets[0];
                    var headers = readHeaders(worksheet);
                    var entries = readEntries(file, worksheet, headers);
                    allEntries.AddRange(entries);
                }
                EditorUtility.DisplayProgressBar("Checking...", "Checking duplicated ids", 1);
                if (!checkDuplication(allEntries))
                    return false;
                export(allEntries, language, exportFile);
                AssetDatabase.Refresh();
                return true;
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static bool checkDuplication(List<LocalizationEntry> enties)
        {
            var query = from entry in enties
                group entry by entry.Id
                into g
                where g.Count() > 1
                select new
                {
                    Id = g.Key,
                    Entries = g.ToArray(),
                };

            var ret = true;
            foreach (var result in query)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("Duplicated id -> {0}:", result.Id);
                builder.AppendLine();
                foreach (var entry in result.Entries)
                    builder.AppendLine(entry.Source);
                Debug.LogError(builder.ToString());
                ret = false;
            }
            return ret;
        }

        private static void export(List<LocalizationEntry> entries, string language, string path)
        {
            var jObject = new JObject();
            foreach (var entry in entries)
            {
                var content = entry[language];
                if (content == null)
                    continue;
                content = content.Replace("\r\n", @"\r\n");
                jObject.Add(entry.Id, content);
            }
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(jObject.ToString());
            }
        }

        private static List<LocalizationEntry> readEntries(string source, Worksheet sheet, string[] headers)
        {
            var list = new List<LocalizationEntry>();
            var row = 1;

            while (true)
            {
                var idCell = sheet.Cells[row, 0];
                if (string.IsNullOrEmpty(idCell.StringValue))
                    break;

                var entry = new LocalizationEntry
                {
                    Id = idCell.StringValue,
                    Source = source,
                };
                list.Add(entry);
                for (var column = 1; column < headers.Length; column++)
                {
                    var cell = sheet.Cells[row, column];
                    entry.Add(headers[column], cell.StringValue);
                }

                row++;
            }
            return list;
        }

        private static string[] readHeaders(Worksheet sheet)
        {
            var list = new List<string>();
            var column = 0;

            while (true)
            {
                var cell = sheet.Cells[0, column];
                if (string.IsNullOrEmpty(cell.StringValue))
                    break;
                list.Add(cell.StringValue);
                column++;
            }
            return list.ToArray();
        }
    }
}