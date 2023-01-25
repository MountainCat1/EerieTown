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

    public event Action TickEvent;

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
                
            TickEvent?.Invoke();

            yield return new WaitForSeconds(1f / ticksPerSecond);
        }
    }
} 