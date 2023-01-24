using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HelloWorld : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;


    private void Start()
    {
        gameManager.OnTick += OnTick;
    }

    private void OnTick()
    {
        Debug.Log("XD");
    }
}
