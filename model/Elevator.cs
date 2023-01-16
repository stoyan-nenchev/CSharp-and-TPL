namespace Area51 {
    public class Elevator
    {
        public Floor currentFloor { get; set; }
        public Agent? agentUsingElevator { get; set; }

        public Elevator(Floor floor)
        {
            this.currentFloor = floor;
            this.agentUsingElevator = null;
        }

        public void moving(Floor floorToMove, int pressedButton) {
            if (this.agentUsingElevator != null) {
                    if (this.currentFloor.floorNumber == pressedButton) {
                    Console.WriteLine("Agent with security level {0} pressed button {1}, but the elevator is already on floor {2}.",
                     this.agentUsingElevator.securityLevel, pressedButton, this.currentFloor.floorName);
                    Thread.Sleep(1000);
                } else {
                    Console.WriteLine("The elevator button is pressed to floor {0} by agent with security level {1}.", floorToMove.floorName, this.agentUsingElevator.securityLevel);
                    Thread.Sleep((Math.Abs(this.currentFloor.floorNumber - pressedButton)) * 1000);
                    Console.WriteLine("The elevator has arrived on floor {0}.", floorToMove.floorName);
                    this.currentFloor = floorToMove;
                }
            } else {
                Console.WriteLine("The elevator is empty.");
            }
        }
    
    }
}