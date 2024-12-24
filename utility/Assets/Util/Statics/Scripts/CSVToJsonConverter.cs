using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Statics
{
    public class CSVToJsonConverter
    {
        public void ConvertCSVToJson(string csvContent, string jsonPath)
        {
            var csvLines = csvContent.Split('\n');

            if (csvLines.Length == 0)
            {
                Debug.LogError("CSV 파일이 비어있습니다.");
                return;
            }

            var headers = csvLines[0].Split(',');
            var dataList = new List<Dictionary<string, string>>();

            for (int i = 1; i < csvLines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(csvLines[i]))
                    continue;

                var row = csvLines[i].Split(',');
                var rowDict = new Dictionary<string, string>();

                for (int j = 0; j < headers.Length; j++)
                {
                    if (j < row.Length)
                        rowDict[headers[j]] = row[j];
                    else
                        rowDict[headers[j]] = "";
                }

                dataList.Add(rowDict);
            }

            var json = JsonConvert.SerializeObject(dataList, Formatting.Indented);
            File.WriteAllText(jsonPath, json);

            if (json == null)
            {
                Debug.LogError("CSV를 JSON으로 변환하는 중 오류가 발생했습니다");
            }
            else
            {
                Debug.Log($"CSV가 성공적으로 JSON으로 변환되었습니다. \n파일 경로 : {jsonPath}");
            }
        }
    }
}