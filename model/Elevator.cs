namespace Area51 {
    public class Elevator
    {
        public Floor currentFloor { get; set; }
        public Agent? agentUsingElevator { get; set; }
        public ManualResetEvent elevatorBegginingToMove { get; set; }
        public int pressedButton { get; set; }
        public Floor? floorToMove { get; set; }
        public bool noMoreAgentsToUseElevator { get; set; }

        public Elevator(Floor floor)
        {
            this.currentFloor = floor;
            this.agentUsingElevator = null;
            elevatorBegginingToMove = new ManualResetEvent(false);
            pressedButton = 0;
            floorToMove = null;
            noMoreAgentsToUseElevator = false;
        }

        
        public void startElevator() {
            while(!noMoreAgentsToUseElevator) {
                elevatorBegginingToMove.WaitOne();
                moving(this.floorToMove!, this.pressedButton);
                elevatorBegginingToMove.Reset();
            }
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
            elevatorBegginingToMove.Reset();
            this.agentUsingElevator!.isRidingOnElevator.Set();
            this.agentUsingElevator!.isRidingOnElevator.Reset();
        }
    
    }
}