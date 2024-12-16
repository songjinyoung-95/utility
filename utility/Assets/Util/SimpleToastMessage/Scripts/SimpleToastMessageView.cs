using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Util.Tween;
using System;

namespace Util.SimpleToast
{
    public class SimpleToastMessageView : MonoBehaviour
    {
        private const float DURATION_TIME = 1;

        [SerializeField] private RectTransform _rect;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _tmpMessage;

        private static readonly WaitForSeconds _delay = new WaitForSeconds(1);

        public void ShowMessageView(string message, Vector2 rectPostion, Action donecallback)
        {
            _rect.anchoredPosition  = rectPostion;
            _tmpMessage.text        = message;

            DoTween.DoScale(_tmpMessage.transform, Vector3.one * 0.8f, Vector3.one, 0.25f, ECurveType.EaseInSine);

            StartCoroutine(Co_Fade(donecallback));
        }

        private IEnumerator Co_Fade(Action donecallback)
        {
            float current       = 1f;
            _canvasGroup.alpha  = 1;

            yield return _delay;

            while(current > 0)
            {
                current -= Time.deltaTime;
                _canvasGroup.alpha = current;

                yield return null;
            }

            _canvasGroup.alpha = 0;
            donecallback?.Invoke();
        }
    }
}