using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Util.SimpleToast;

public class SimpleToastMessage
{
    private static SimpleToastMessage _instance;
    private static SimpleToastMessageCanvas _canvas;

    private static async Task<SimpleToastMessage> Init()
    {
        if(_instance != null)
            return _instance;

        var handle = Addressables.InstantiateAsync("SimpleToastMessage");

        var instance = await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded || instance == null)
        {
            Debug.LogError($"SimpleToastMessage 오브젝트가 제대로 생성되지 않았습니다");
            return null;
        }

        if (!instance.TryGetComponent<SimpleToastMessageCanvas>(out var component))
        {
            Debug.LogError($"{handle.Result.name} Prefab에 : {component}가 제대로 적용되지 않았습니다");
            return null;
        }

        _canvas     = component;
        _instance   = new SimpleToastMessage();

        return _instance;
    }

    public static async Task Show_ButtonToastMessage(string message, Vector2 rectPosition)
    {
        await Init();
        _instance._Show_ButtonToastMessage(message, rectPosition);
    }

    private void _Show_ButtonToastMessage(string message, Vector2 rectPosition)
    {
        // _canvas.ShowButtonToastMessage();
    }

    public static async void Show_ToastMessage(string message, Vector2 rectPosition)
    {
        await Init();
        _instance._Show_ToastMessage(message, rectPosition);
    }
    
    private void _Show_ToastMessage(string message, Vector2 rectPosition)
    {
        _canvas.ShowToastMessage(message, rectPosition);
    }
}
