using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Population
{
    public class PopulationController : MonoBehaviour
    {
        #region Dependencies

        [SerializeField] private GameManager _gameManager;

        [SerializeField] private List<Occupation> _occupations;

        #endregion

        public List<Population> Populations { get; } = new();

        private void Start()
        {
            _gameManager.TickEvent += OnTick;
        }

        private void OnTick()
        {
            foreach (var occupation in _occupations)
            {
                
                occupation.ActAll();
            }
        }

        public void AddPopulation(Population population)
        {
            var targetOccupation = _occupations
                .FirstOrDefault(x => x.GetType().Name == population.OccupationName);
            
            if(targetOccupation != null)
                targetOccupation.Populations.Add(population);
            else
                Debug.LogError($"No occupation named: {population.OccupationName}");
            
            Populations.Add(population);
        }
    }
}