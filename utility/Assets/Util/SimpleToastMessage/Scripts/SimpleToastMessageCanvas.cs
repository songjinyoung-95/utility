using System.Collections;
using System.Collections.Generic;
using Meow_Moew_Shinobi.Pool;
using UnityEngine;

namespace Util.SimpleToast
{
    public enum EToastType
    {
        SimpleToast,
        ButtonToast,
    }

    public class SimpleToastMessageCanvas : MonoBehaviour
    {
        private PoolBase<SimpleToastMessageView>        _toastMessagePool;
        [SerializeField] private SimpleToastMessageView _originToastMessage;

        public void ShowToastMessage(string message, Vector2 rectPosition)
        {
            string type = EToastType.SimpleToast.ToString();

            if(_toastMessagePool == null)
            {
                _toastMessagePool = new PoolBase<SimpleToastMessageView>(transform);
                _toastMessagePool.Generator(_originToastMessage, type, 5);
            }

            SimpleToastMessageView view = _toastMessagePool.Spawn(type, rectPosition);
            view.ShowMessageView(message, rectPosition, () => _toastMessagePool.Despawn(view, type));
        }
    }
}