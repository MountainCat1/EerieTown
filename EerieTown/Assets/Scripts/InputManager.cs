using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events

    public event Action MainClickedEvent;
    public event Action MainClickedUpEvent;

    #endregion

    public void OnFire(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (context.started)
            MainClickedEvent?.Invoke();

        if (context.canceled)
            MainClickedUpEvent?.Invoke();
    }
}