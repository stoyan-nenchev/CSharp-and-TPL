namespace Area51 {
public class ElevatorDoor
    {
        public bool isAgentWithApprorpiateAccess(Agent agent, Floor floor)
        {
            Thread.Sleep(1000);
            Console.WriteLine("The elevator door scanner begins to scan the security level of the agent.");
            switch(floor.floorName)
            {
                case("G"):
                    Console.WriteLine("Agent with security level {0} is allowed access to floor {1} and enters it.", agent.securityLevel, floor.floorName);
                    return true;
                case("S"):
                    if (agent.canAccessFloorS) {
                        Console.WriteLine("Agent with security level {0} is allowed access to floor {1} and enters it.", agent.securityLevel, floor.floorName);
                        return true;
                    } else {
                        Console.WriteLine("Access denied! Agent with security level {0} is not allowed access to floor {1}.", agent.securityLevel, floor.floorName);
                        return false;
                    }
                case("T1"):
                    if (agent.canAccessFloorT1) {
                        Console.WriteLine("Agent with security level {0} is allowed access to floor {1} and enters it.", agent.securityLevel, floor.floorName);
                        return true;
                    } else {
                        Console.WriteLine("Access denied! Agent with security level {0} is not allowed access to floor {1}.", agent.securityLevel, floor.floorName);
                        return false;
                    }
                case("T2"):
                    if (agent.canAccessFloorT2) {
                        Console.WriteLine("Agent with security level {0} is allowed access to floor {1} and enters it.", agent.securityLevel, floor.floorName);
                        return true;
                    } else {
                        Console.WriteLine("Access denied! Agent with security level {0} is not allowed access to floor {1}.", agent.securityLevel, floor.floorName);
                        return false;
                    }
                default:
                return false;
            }
        }

    }
}