# CSharp-and-TPL
This is a university project which utilizes C# and TPL.

The idea of the task is we have three agents, which go to Area51. We have an elevator, which every agent can use to access a certain floor.
Area51 has 4 floors - G, S, T1 and T2. Agents start at floor G. Agents also have three types of access levels - Confidential, Secret and Top-secret.
The access for the floors are as follow:
Floor G: Confidential, Secret and Top-Secret
Floor S: Secret and Top-Secret
Floor T1: Top-Secret
Floor T2: Top-Secret

The elevator has a scanner which scans each agent, if the have access to the floor which they have chosen.

All three agents and the elevator are controlled by threads and movement is random, as well as decisions (if the agent should work or go home).
