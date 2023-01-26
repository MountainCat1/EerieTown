using System.Collections.Generic;

namespace Population
{
    public class House : Building
    {
        private PopulationController _populationController;
        
        protected override void Start()
        {
            base.Start();

            _populationController = FindObjectOfType<PopulationController>();
        }

        protected override List<Population> InitializePopulations()
        {
            return new List<Population>()
            {
                new()
                {
                    Name = "Beggar 1",
                    OccupationName = nameof(BeggarOccupation)
                },
                new()
                {
                    Name = "Beggar 2",
                    OccupationName = nameof(BeggarOccupation)
                },
                new()
                {
                    Name = "Beggar 3",
                    OccupationName = nameof(BeggarOccupation)
                },
            };
        }
    }
}
