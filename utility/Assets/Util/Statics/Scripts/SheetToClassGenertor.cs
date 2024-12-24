using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Statics
{
    public static class SheetToClassGenertor
    {
        /// 구글 스프레드 시트 API 통해 시트에 접근
        /// 
        ///
        
        public static void GenerateClass(string jsonData, string className, string outputPath)
        {
            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

            if(data == null || data.Count == 0)
            {
                throw new Exception("클래스를 생성하는데 사용할 수 있는 데이터가 없습니다");
            }

            var firstRow = data[0];
            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine($"public class {className}");
            sb.AppendLine("{");

            foreach (var key in firstRow.Keys)
            {
                string propertyName = key.Replace(" ", "_").Replace("-", "_");
                string propertyType = PropertyType(firstRow[key]);

                sb.AppendLine($"public {propertyType} {propertyName} {{get; set;}}");
            }

            sb.AppendLine("}");

            File.WriteAllText(outputPath, sb.ToString());
            Debug.Log($"Class {className} generated at {outputPath}");
        }

        private static string PropertyType(object value)
        {
            if(value == null) return "string";
            if(int.TryParse(value.ToString(), out _)) return "int";
            if (float.TryParse(value.ToString(), out _)) return "float";
            if (bool.TryParse(value.ToString(), out _)) return "bool";
            if (DateTime.TryParse(value.ToString(), out _)) return "DateTime";

            return "string";
        }
    }
}