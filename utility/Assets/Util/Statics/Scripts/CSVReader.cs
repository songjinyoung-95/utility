using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Statics
{
    public static class CSVReader
    {
        public static List<T> CSVToJsonReader<T>(string path) where T : class
        {
            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);
                List<T> dataList = JsonConvert.DeserializeObject<List<T>>(jsonContent);

                return dataList;
            }
            else
            {
                Debug.LogError($"{path} 해당 경로에서 Json File을 찾을 수 없습니다");

                return null;
            }
        }
    }
}