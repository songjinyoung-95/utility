using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Util.Tween
{
    public class Singleton : MonoBehaviour
    {
        private static Singleton _instance;
        public static Singleton Instance
        {
            get
            {
                if(_instance == null)
                {
                    GameObject  obj     = new GameObject("TweenManager");
                    Singleton   inst    = obj.AddComponent<Singleton>();

                    inst.Init();
                }

                return _instance;   
            }
        }




        /// ---------------
        /// serializedField
        /// ---------------
        [SerializeField] private Curve[] _curves;


        /// ---------------
        /// private
        /// ---------------
        private string _bundleLabel = "AnimationCurve";

        private Dictionary<ECurveType, Curve> _curveDict;
        private AsyncOperationHandle<IList<Curve>> _handles;


        private void Init()
        {
            _instance   = this;
            _curveDict  = new Dictionary<ECurveType, Curve>();
            _curves     = new Curve[] { };

            LoadAddressableAssets();
            DontDestroyOnLoad(gameObject);
        }

        private void AddCurves()
        {
            for (int i = 0; i < _curves.Length; i++)
            {
                if(_curveDict.ContainsKey(_curves[i].Type))
                {
                    Debug.LogError($"duplication Curve Type : {_curves[i].Type}");
                    return;
                }
                
                _curveDict.Add(_curves[i].Type, _curves[i]);
            }            
        }

        private void LoadAddressableAssets()
        {
            _handles = Addressables.LoadAssetsAsync<Curve>(_bundleLabel, null);
            _handles.Completed += OnComplete;


            void OnComplete(AsyncOperationHandle<IList<Curve>> curves)
            {
                if(curves.Status != AsyncOperationStatus.Succeeded)
                    return;

                _curves = _handles.Result.ToArray();

                AddCurves();
                Addressables.Release(_handles);
            }
        }

        private void Release()
        {
            Addressables.Release(_handles);
            Destroy(_instance.gameObject);
            _instance = null;
        }

        public AnimationCurve GetCurveType(ECurveType curveType)
        {
            if(!_curveDict.ContainsKey(curveType))
            {
                Debug.LogWarning($"{curveType}에 해당하는 애니메이션 커브 데이터가 없습니다");
                return AnimationCurve.Linear(0, 0, 1, 1);
            }

            return _curveDict[curveType].AnimationCurve;
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
    }
}