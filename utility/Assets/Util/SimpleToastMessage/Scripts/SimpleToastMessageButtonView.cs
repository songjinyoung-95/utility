using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace Util.SimpleToast
{
    public class SimpleToastMessageButtonView : MonoBehaviour
    {
        private const float DURATION_TIME = 1;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _tmpMessage;
        [SerializeField] private Button _btnClose;

        private static readonly WaitForSeconds _delay = new WaitForSeconds(0.5f);

        public void ShowMessageView(bool enabledBtn, string message)
        {
            _tmpMessage.text = message;

            StartCoroutine(Co_Fade());
        }

        private IEnumerator Co_Fade()
        {
            yield return _delay;

            float current       = 1;
            _canvasGroup.alpha  = 1;

            while(current > 0)
            {
                current -= Time.deltaTime;

                _canvasGroup.alpha -= current;
                yield return null;
            }

            _canvasGroup.alpha = 0;
        }
    }
}