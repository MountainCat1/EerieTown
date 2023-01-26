using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Population
{
    public class PopulationController : MonoBehaviour
    {
        #region Dependencies

        [SerializeField] private GameManager _gameManager;

        [SerializeField] private List<Occupation> _occupations;

        #endregion

        public List<Population> Populations { get; } = new();
        public Dictionary<Occupation, List<Population>> OccupantionsPopulations { get; } = new();

        private void Start()
        {
            Debug.Log(Populations.Count);
            
            _gameManager.TickEvent += OnTick;

            foreach (var occupation in _occupations)
            {
                occupation.Initialize();
                
                OccupantionsPopulations.Add(occupation, new List<Population>());
            }
        }

        private void OnTick()
        {
            foreach (var occupation in _occupations)
            {
                occupation.ActAll(OccupantionsPopulations[occupation]);
            }
        }

        public void AddPopulation(Population population)
        {
            var targetOccupation = _occupations
                .FirstOrDefault(x => x.GetType().Name == population.OccupationName);
            
            if(targetOccupation != null)
                OccupantionsPopulations.Extend(targetOccupation, population);
            else
                Debug.LogError($"No occupation named: {population.OccupationName}");
            
            Populations.Add(population);
        }
    }
}