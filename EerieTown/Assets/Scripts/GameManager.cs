using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("In Game")]
    [SerializeField] private bool paused = false;

    #region Settings

    [Header("Settings")]
    [SerializeField] private float ticksPerSecond = 2;

    #endregion

    #region Events

    public Action OnTick;

    #endregion
    
    public void Awake()
    {
        StartCoroutine(TickCoroutine());
    }


    private IEnumerator TickCoroutine()
    {
        while (true)
        {
            while (paused)
                yield return new WaitForEndOfFrame();
                
            OnTick?.Invoke();

            yield return new WaitForSeconds(1f / ticksPerSecond);
        }
    }
} 