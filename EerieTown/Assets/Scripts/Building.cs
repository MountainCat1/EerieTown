using System;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    #region Dependencies

    #endregion
    
    public Vector2Int Position { get; set; }
    public Vector2Int Size { get; set; }

    protected virtual void Start()
    {
        FindObjectOfType<GameManager>().TickEvent += OnTick;
    }

    protected virtual void OnTick()
    {
    }
}