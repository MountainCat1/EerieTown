using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events

    public event Action MainClicked;
    public event Action MainClickedUp;

    #endregion

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.started)
            MainClicked?.Invoke();
        
        if(context.canceled)
            MainClickedUp?.Invoke();
    }
}
