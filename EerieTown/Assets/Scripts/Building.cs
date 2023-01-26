using System;
using System.Collections.Generic;
using UnityEngine;
using Population;

public abstract class Building : MonoBehaviour
{
    #region Dependencies

    protected GameManager Manager { get; private set; }
    protected BuildingController BuildingController { get; private set; }
    protected PopulationController PopulationController { get; private set; }
    
    #endregion
    
    public Vector2Int Position { get; set; }
    public Vector2Int Size { get; set; }
    
    public List<Population.Population> Populations { get; private set; }


    protected virtual void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        BuildingController = FindObjectOfType<BuildingController>();
        PopulationController = FindObjectOfType<PopulationController>();
            
        Manager.TickEvent += OnTick;

        Populations = InitializePopulations();

        foreach (var pop in Populations)
        {
            pop.OriginBuilding = this;
            PopulationController.AddPopulation(pop);
        }
    }

    protected virtual void OnTick()
    {
    }
    
    protected abstract List<Population.Population> InitializePopulations();
}