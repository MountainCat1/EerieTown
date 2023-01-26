using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum DayPhase
{
    Morning, Midday, Afternoon, Evening, Night, Midnight
}

public class GameManager : MonoBehaviour
{
    [field: Header("In Game")] 
    [field: SerializeField] public bool Paused { get; private set; } = false;

    #region Settings


    [Header("Settings")]
    [SerializeField] private float _ticksPerSecond = 2;

    [SerializeField] private int _ticksPerInGameHour = 10;
    [SerializeField] private int _inGameHoursPerInGameDay = 24;

    #endregion

    #region Events

    public event Action TickEvent;

    #endregion

    public int Tick { get; private set; } = 0;
    public int Minutes => Tick * 60 / _ticksPerInGameHour % 60;
    public int Hour => 6 + Tick / _ticksPerInGameHour % 24;
    public int Day => Tick / _ticksPerInGameHour / _inGameHoursPerInGameDay;

    public bool IsDay => Hour is >= 7 and <= 19;

    public DayPhase DayPhase
        => Hour < 4 ? DayPhase.Midnight
            : Hour < 8 ? DayPhase.Morning
            : Hour < 12 ? DayPhase.Midday
            : Hour < 16 ? DayPhase.Afternoon
            : Hour < 20 ? DayPhase.Evening
            : Hour < 24 ? DayPhase.Night 
            : throw new InvalidOperationException();
    


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

            try
            {
                TickEvent?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            yield return new WaitForSeconds(1f / _ticksPerSecond);
        }
    }
} 