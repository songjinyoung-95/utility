using System;
using System.Collections;
using UnityEngine;

namespace Util.Tween
{
    public enum ECurveType
    {
        Linear,
        EaseInSine,
        EaseOutSine,
    }


    public class DoTween
    {
        public static Coroutine DOMove(Transform target, Vector3 startPos, Vector3 endPos, float time, ECurveType curve = ECurveType.Linear, Action donecallback = null)
         => TweenCoroutine.Tween_CoroutineStart(Co_Move(target, startPos, endPos, time, curve, donecallback));

        public static Coroutine DoRotate(Transform target, Quaternion startRot, Quaternion endRot, float time, ECurveType curve = ECurveType.Linear, Action donecallback = null)
         => TweenCoroutine.Tween_CoroutineStart(Co_Rotate(target, startRot, endRot, time, curve, donecallback));

        public static Coroutine DoParabolicMove(Transform target, Vector3 startPos, Vector3 endPos, float time, float height, ECurveType curve = ECurveType.Linear, Action doneCallback = null)
        => TweenCoroutine.Tween_CoroutineStart(Co_ParabolicMove(target, startPos, endPos, time, height, curve, doneCallback));

        public static Coroutine DoScale(Transform target, Vector3 startScale, Vector3 endScale, float time, ECurveType curve = ECurveType.Linear, Action donecallback = null)
        => TweenCoroutine.Tween_CoroutineStart(Co_Scale(target, startScale, endScale, time, curve, donecallback));


        public static void AllStopTween() => TweenCoroutine.Tween_AllStopCoroutine();
        public static void StopTween(Coroutine coroutine) => TweenCoroutine.Tween_CoroutineStop(coroutine);


        private static IEnumerator Co_Move(Transform target, Vector3 startPos, Vector3 endPos, float time, ECurveType curveType, Action donecallback)
        {
            Transform moveTarget    = target;
            float defaultTime       = 0;
            target.position         = startPos;
            AnimationCurve curve    = TweenCoroutine.GetCurveType(curveType);

            while (defaultTime <= time)
            {
                moveTarget.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(defaultTime / time));
                
                defaultTime += Time.deltaTime;
                yield return null;
            }

            moveTarget.position = endPos;
            donecallback?.Invoke();
        }

        private static IEnumerator Co_Rotate(Transform target, Quaternion startRot, Quaternion endRot, float time, ECurveType curveType, Action donecallback)
        {
            Transform moveTarget    = target;
            float defaultTime       = 0;
            target.rotation         = endRot;
            AnimationCurve curve    = TweenCoroutine.GetCurveType(curveType);

            while (defaultTime <= time)
            {
                moveTarget.rotation = Quaternion.Lerp(startRot, endRot, curve.Evaluate(defaultTime / time));
                
                defaultTime += Time.deltaTime;
                yield return null;
            }

            moveTarget.rotation = endRot;
            donecallback?.Invoke();
        }        

        private static IEnumerator Co_ParabolicMove(Transform target, Vector3 startPos, Vector3 endPos, float time, float height, ECurveType curveType, Action doneCallback)
        {
            Transform moveTarget    = target;
            float elapsedTime       = 0f;
            Vector3 distance        = endPos - startPos;
            AnimationCurve curve    = TweenCoroutine.GetCurveType(curveType);

            while (elapsedTime < time)
            {
                float t = elapsedTime / time;
                float parabolicT    = 4 * t * (1 - t);

                Vector3 horizontal  = Vector3.Lerp(startPos, endPos, curve.Evaluate(t));
                float vertical      = Mathf.Lerp(startPos.y, endPos.y, curve.Evaluate(t)) + height * parabolicT;

                target.position = new Vector3(horizontal.x, vertical, horizontal.z);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            moveTarget.position = endPos;
            doneCallback?.Invoke();
        }        

        private static IEnumerator Co_Scale(Transform target, Vector3 startScale, Vector3 endScale, float time, ECurveType curveType, Action donecallback)
        {
            float defaultTime       = 0;
            target.localScale       = startScale;
            AnimationCurve curve    = TweenCoroutine.GetCurveType(curveType);

            while (defaultTime <= time)
            {
                target.localScale = Vector3.Lerp(startScale, endScale, curve.Evaluate(defaultTime / time));
                
                defaultTime += Time.deltaTime;
                yield return null;
            }

            target.localScale = endScale;
            donecallback?.Invoke();
        }        
    }
}