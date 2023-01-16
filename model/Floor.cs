namespace Area51 {
public class Floor
    {
        public String floorName { get; set; }
        public int floorNumber { get; set; }
        public Floor(string floorName, int floorNumber)
        {
            this.floorName = floorName;
            this.floorNumber = floorNumber;
        }
    }
}