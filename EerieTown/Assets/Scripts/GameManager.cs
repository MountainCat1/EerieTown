using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [field: Header("In Game")] 
    [field: SerializeField] public bool Paused { get; private set; } = false;

    #region Settings

    [Header("Settings")]
    [SerializeField] private float ticksPerSecond = 2;

    #endregion

    #region Events

    public event Action TickEvent;

    #endregion

    public int Tick { get; private set; } = 0;
    
    public void Awake()
    {
        StartCoroutine(TickCoroutine());
    }

    public void ChangePause()
    {
        Paused = !Paused;
    }

    private IEnumerator TickCoroutine()
    {
        while (true)
        {
            while (Paused)
                yield return new WaitForEndOfFrame();

            Tick++;
            
            TickEvent?.Invoke();

            yield return new WaitForSeconds(1f / ticksPerSecond);
        }
    }
} 