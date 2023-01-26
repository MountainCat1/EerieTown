using System.Collections.Generic;
using UnityEngine;

namespace Population
{
    [CreateAssetMenu(fileName = nameof(BeggarOccupation), menuName = "Occupations/Beggar", order = 1)]
    public class BeggarOccupation : Occupation
    {
        private GameManager _gameManager;
        private BuildingController _buildingController;


        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _buildingController = FindObjectOfType<BuildingController>();
        }

        protected override void Act(Population population)
        {
            Debug.Log("XD");
            // Every hour
            if (_gameManager.Minutes == 0)
            {
                UpdateLocation(population);
            }
        }

        private void UpdateLocation(Population population)
        {
            // At day time
            if (_gameManager.IsDay)
            {
                var targetBuilding = _buildingController
                    .GetRandomBuildingInRange(population.OriginBuilding.Position, 1);
                
                population.MoveTo(targetBuilding);
                return;
            }

            // At night time
            if (!_gameManager.IsDay)
            {
                population.MoveTo(population.OriginBuilding);
                return;
            }
        }
    }
}