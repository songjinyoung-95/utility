using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Statics
{
    public class InitializerManager : MonoBehaviour
    {
        void Start()
        {
            string fileName = "QuestData";

            TextAsset csvFile = Resources.Load<TextAsset>(fileName);
            string jsonOutputPath = Path.Combine(Application.persistentDataPath, "output.json");

            if (csvFile == null)
            {
                Debug.LogError($"Resources 폴더에서 {fileName}.csv 파일을 찾을 수 없습니다.");
                return;
            }

            CSVToJsonConverter converter = new CSVToJsonConverter();

            // CSV파일을 Json으로 변경하는 과정 에디터에서 미리 Json화로 변경시켜서 사용하는게 좋음
            converter.ConvertCSVToJson(csvFile.text, jsonOutputPath);

            // Json Data를 
            List<QuestData> questDatas = CSVReader.CSVToJsonReader<QuestData>(jsonOutputPath);

            Debug.Log($"퀘스트 데이터 크기 :{questDatas.Count}");
            
            foreach (var quest in questDatas)
            {
                Debug.Log($"ID : {quest.ID}, DiaLog : {quest.Dialog}, Reward : {quest.Reward}");
            }
        }
    }
}