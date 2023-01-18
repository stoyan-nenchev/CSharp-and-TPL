namespace Area51 {
    class Program
    {
        static void Main()
        {
            Floor startingFloor = new Floor("G", 1);
            Elevator elevator = new Elevator(startingFloor);
            Agent confidentialAgent = new Agent("Confidential", startingFloor, elevator);
            Agent secretAgent = new Agent("Secret", startingFloor, elevator);
            Agent topSecretAgent = new Agent("Top-secret", startingFloor, elevator);

            Thread confidentialAgentThread = new Thread(confidentialAgent.enterArea51);
            Thread secretAgentThread = new Thread(secretAgent.enterArea51);
            Thread topSecretAgentThread = new Thread(topSecretAgent.enterArea51);

            Thread elevatorThread = new Thread(elevator.startElevator);
            
            var agents = new List<Thread>();
            agents.Add(confidentialAgentThread);
            agents.Add(secretAgentThread);
            agents.Add(topSecretAgentThread);

            secretAgentThread.Start();
            confidentialAgentThread.Start();
            topSecretAgentThread.Start();
            elevatorThread.Start();
            foreach (var a in agents) a.Join();
            Console.WriteLine("No more agents in Area51.");
            elevator.noMoreAgentsToUseElevator = true;
            elevatorThread.Join();
            Console.WriteLine("Elevator stopped working.");
        }
    }
}
