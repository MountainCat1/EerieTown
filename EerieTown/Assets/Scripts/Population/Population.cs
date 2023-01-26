namespace Population
{
    public class Population
    {
        public string Name { get; set; }
        public Building OriginBuilding { get; set; }
        public Building PresentBuilding { get; set; }
        
        public string OccupationName { get; set; }
        
        public void MoveTo(Building building)
        {
            PresentBuilding = building;
        }
    }
    
}