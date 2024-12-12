using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class TestClass : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    
    private void Start()
    {
        TimeUtil.ChangeTimeScale(0.5f, 1);
    }

    private void Update()
    {
        obj.transform.position += transform.forward * 1.2f * Time.deltaTime; 
        Debug.Log(Time.timeScale);
    }
}
