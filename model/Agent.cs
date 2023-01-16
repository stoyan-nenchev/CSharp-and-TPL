namespace Area51 {
public class Agent
    {
        public String securityLevel { get; set; }
        public Floor currentFloor { get; set; }
        public bool isInElevator { get; set; }
        public bool canAccessFloorG { get; set; }
        public bool canAccessFloorS { get; set; }
        public bool canAccessFloorT1 { get; set; }
        public bool canAccessFloorT2 { get; set; }
        public bool hasLeftArea51 { get; set; }
        public bool decidesToLeave { get; set; }
        public Elevator elevator { get; set; }
        public Agent(String securityLevel, Floor currentFloor, Elevator elevator)
        {
            this.securityLevel = securityLevel;
            this.currentFloor = currentFloor;
            this.hasLeftArea51 = false;
            this.isInElevator = false;
            this.decidesToLeave = false;
            this.elevator = elevator;
            setupAccess(this.securityLevel);
        }

        static Random rand = new Random();

        private bool actionChance(int chance)
        {
            int dice = rand.Next(100);
            return dice < chance;
        }

        Floor floorG = new Floor("G", 1);
        Floor floorS = new Floor("S", 2);
        Floor floorT1 = new Floor("T1", 3);
        Floor floorT2 = new Floor("T2", 4);
        ElevatorDoor elevatorDoor = new ElevatorDoor();
        ManualResetEvent elevatorIsTaken = new ManualResetEvent(false);

        public void enterArea51() {
            int elevatorMemoryButton = 0;
            Console.WriteLine("Agent with security level {0} entered Area51", this.securityLevel);

            if (this.securityLevel == "Confidential") {
                Thread.Sleep(3000);
            } else if (this.securityLevel == "Secret") {
                Thread.Sleep(2000);
            } else if (this.securityLevel == "Top-secret") {
                Thread.Sleep(1000);
            }
            
            while (!this.hasLeftArea51)
            {
                //Call elevator
                this.callElevator(elevator);
                    
                if (elevator.agentUsingElevator == this) {
                    //Agent is in the elevator
                    while(this.isInElevator) {
                        //If agent decides to leave, the path is hardcoded
                        if (decidesToLeave) {
                            this.pressElevatorButton(1, elevator);
                        } else {
                            this.pressElevatorButton(rand.Next(1, 5), elevator);
                        }

                        //Scanner works only if agent went on a different floor
                        if (elevator.currentFloor.floorName != this.currentFloor.floorName) {
                                
                            //If agent can access the floor, he enters it (if security level passes)
                            if (elevatorDoor.isAgentWithApprorpiateAccess(this, elevator.currentFloor) && elevatorMemoryButton != elevator.currentFloor.floorNumber) 
                            {
                                this.currentFloor = elevator.currentFloor;
                                elevator.agentUsingElevator = null;
                                this.isInElevator = false;
                                elevatorMemoryButton = 0;
                                elevatorIsTaken.Set();
                                Thread.Sleep(1000);
                                break;
                            } else {
                                Console.WriteLine("Agent with security level {0} will press another button.", this.securityLevel);
                                Thread.Sleep(1000);
                            }
                        } else {
                            //If agent decides to leave the elevator on the same floor that he was
                            if (actionChance(30) && this.currentFloor.floorName == elevator.currentFloor.floorName) {
                                Console.WriteLine("Agent with security level {0} leaves the elevator on floor {1}", this.securityLevel, this.currentFloor.floorName);
                                elevator.agentUsingElevator = null;
                                this.isInElevator = false;
                                elevatorMemoryButton = 0;
                                elevatorIsTaken.Set();
                                Thread.Sleep(1000);
                                break;
                            } else {
                                elevatorMemoryButton = elevator.currentFloor.floorNumber;
                                Console.WriteLine("Agent with security level {0} decided to stay in the elevator", this.securityLevel);
                                Thread.Sleep(1000);
                            }
                        }
                            
                    }
                }

                //Floor activities
                while (true)
                {
                    //If agent already decided to leave, there is no going back, so no activies for the floor
                    if (this.decidesToLeave && this.currentFloor.floorName == floorG.floorName) {
                        this.hasLeftArea51 = true;
                        break;
                    } else if (this.decidesToLeave) {
                        break;
                    }
                    
                    //Shall I work?
                    if (actionChance (40))
                    {
                        Console.WriteLine("Agent with security level {0} is cleaning up on floor {1}", this.securityLevel, this.currentFloor.floorName);
                        Thread.Sleep(1000);
                    }

                    if (actionChance (60))
                    {
                        Console.WriteLine("Agent with security level {0} is gathering equipment on floor {1}", this.securityLevel, this.currentFloor.floorName);
                        Thread.Sleep(1000);
                    }

                    if (actionChance (80))
                    {
                        Console.WriteLine("Agent with security level {0} is developing on floor {1}", this.securityLevel, this.currentFloor.floorName);
                        Thread.Sleep(1000);
                    }

                    //Shall I leave this floor?
                    if (actionChance (20))
                    {
                        this.decidesToLeave = true;
                        Console.WriteLine("Agent with security level {0} will start leaving Area51.", this.securityLevel);
                        Thread.Sleep(1000);
                        break;
                    }
                  Thread.Sleep(1000);
                }
            }
        }

        public void callElevator(Elevator elevator) {
            Console.WriteLine("Agent with security level {0} calls the elevator to floor {1}.", this.securityLevel, this.currentFloor.floorName);
            Thread.Sleep(1000);
            if (elevator.agentUsingElevator == null && elevator.currentFloor.floorName == this.currentFloor.floorName) {
                this.enterElevator(elevator);
            } else if (elevator.currentFloor.floorName != this.currentFloor.floorName) {
                Console.WriteLine("The elevator is on a different floor than the agent. Elevator is on floor {0} and the agent is on floor {1}.",
                elevator.currentFloor.floorName, this.currentFloor.floorName);
                elevatorIsTaken.WaitOne();
            } else if (elevator.agentUsingElevator != null && elevator.currentFloor.floorName == this.currentFloor.floorName) {
                Console.WriteLine("The elevator is already taken by agent with security level {0}.", elevator.agentUsingElevator.securityLevel);
                elevatorIsTaken.WaitOne();
            }
        }

        public void enterElevator(Elevator elevator) {
            Thread.Sleep(1000);
            Console.WriteLine("Agent with security level {0} boards the elevator at floor {1}.", this.securityLevel, elevator.currentFloor.floorName);
            elevator.agentUsingElevator = this;
            this.isInElevator = true;
        }

        public void pressElevatorButton(int pressedButton, Elevator elevator) {
            /*
            * Buttons go as:
            * 1. Floor G
            * 2. Floor S
            * 3. Floor T1
            * 4. Floor T2
            */
            switch(pressedButton) {
                case 1:
                elevator.moving(floorG, pressedButton);
                break;
                case 2:
                elevator.moving(floorS, pressedButton);
                break;
                case 3:
                elevator.moving(floorT1, pressedButton);
                break;
                case 4:
                elevator.moving(floorT2, pressedButton);
                break;
            }
        }


        public void leaveElevator(Elevator elevator) {
            Console.WriteLine("Agent with security level {0} leaves elevator on floor {1}.", this.securityLevel, elevator.currentFloor.floorName);
            elevator.agentUsingElevator = null;
            this.currentFloor = elevator.currentFloor;
        }

        private void setupAccess(String securityLevel) {
            switch(securityLevel){
                case("Confidential"):
                this.canAccessFloorG = true;
                this.canAccessFloorS = false;
                this.canAccessFloorT1 = false;
                this.canAccessFloorT2 = false;
                break;
                case("Secret"):
                this.canAccessFloorG = true;
                this.canAccessFloorS = true;
                this.canAccessFloorT1 = false;
                this.canAccessFloorT2 = false;
                break;
                case("Top-secret"):
                this.canAccessFloorG = true;
                this.canAccessFloorS = true;
                this.canAccessFloorT1 = true;
                this.canAccessFloorT2 = true;
                break;
            }
        }

    }
}