using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Util.Tween
{
    public class TweenCoroutine : MonoBehaviour
    {
        private const string RESOURCES_PATH = "TweenCoroutine";
        
        private static TweenCoroutine _instance;
        private static TweenCoroutine Instance
        {
            get
            {
                if(_instance == null)
                {
                    TweenCoroutine tween = Resources.Load<TweenCoroutine>(RESOURCES_PATH);

                    _instance = Instantiate(tween, null);
                    _instance._curveManager.Initialize();
                }

                return _instance;   
            }
        }

        [SerializeField] private CurveManager _curveManager;

        public static AnimationCurve GetCurveType(ECurveType curveType)
        {
            return Instance._curveManager.GetCurve(curveType);
        }


        public static Coroutine Tween_CoroutineStart(IEnumerator enumerator)
        {
            if(enumerator == null)
                return null;

            return Instance.StartCoroutine(enumerator);
        }

        public static void Tween_CoroutineStop(Coroutine coroutine)
        {
            if(coroutine == null)
                return;

            Instance.StopCoroutine(coroutine);
        }

        public static void Tween_AllStopCoroutine()
        {
            Instance.StopAllCoroutines();
        }

        [System.Serializable]
        private class CurveManager
        {
            private Dictionary<ECurveType, CurveData> _curveDict;
            [SerializeField] private CurveData[] _curveDatas;

            public void Initialize()
            {
                _curveDict = new Dictionary<ECurveType, CurveData>();
                
                foreach (CurveData curve in _curveDatas)
                {
                    if (!_curveDict.ContainsKey(curve.Type))
                        _curveDict.Add(curve.Type, curve);
                }
            }

            public AnimationCurve GetCurve(ECurveType type)
            {
                if (_curveDict.TryGetValue(type, out var curve))
                    return curve.AnimationCurve;

                Debug.LogWarning($"Curve Type : '{type}' not found.");
                return AnimationCurve.Linear(0, 0, 1, 1);
            }
        }
    }
}