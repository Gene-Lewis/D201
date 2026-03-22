using System;
using System.Collections.Generic;

class Program
{
    // Recursive method to get the length of a list
    static int LengthOfList(List<int> list)
    {
        if (list.Count == 0)
        {
            return 0;
        }
        else
        {
            return 1 + LengthOfList(list.GetRange(1, list.Count - 1)); // Remove the first element and call recursively
        }
    }

    // Method to represent graph as a string
    static string DictionaryGraph(Dictionary<string, List<string>> graph)
    {
        string result = "";
        foreach (var node in graph)
        {
            result += $"{node.Key}: {string.Join(", ", node.Value)}\n";
        }
        return result;
    }

    // BFS search method (breadth-first search)
    static bool BFS_Search(Dictionary<string, List<string>> graph, string start, string target, out List<string> traversalOrder)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<string>();
        traversalOrder = new List<string>();

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            traversalOrder.Add(vertex);
            Console.WriteLine($"Visiting: {vertex}");

            if (vertex == target)
            {

                Console.WriteLine($"Found target: {target}");
                return true;
            }

            foreach (var neighboring in graph[vertex])
            {
                if (!visited.Contains(neighboring))
                {
                    visited.Add(neighboring);
                    queue.Enqueue(neighboring);
                }
            }
        }

        Console.WriteLine($"Target {target} not found.");
        return false;
    }

    // DFS search method (depth-first search)
    static bool DFS_Search(Dictionary<string, List<string>> graph, string vertex, string target, HashSet<string> visited, List<string> traversalOrder )
    {
        if (visited.Contains(vertex)) return false;
        Console.WriteLine($"Visiting: {vertex}");
        visited.Add(vertex);
        traversalOrder.Add(vertex);
        if (vertex == target)
        {
            Console.WriteLine($"Found target: {target}");
            return true;
        }

        foreach (var neighbor in graph[vertex])
        {
            if (DFS_Search(graph, neighbor, target, visited, traversalOrder))
                return true;
        }

        return false;
    }

    static void Main(string[] args)
    {
        List<int> myList = new List<int> { 1, 2, 3, 4, 5, 6 };
        int length = LengthOfList(myList);
        Console.WriteLine("The length of the list is: " + length);

        var graph = new Dictionary<string, List<string>>
        {
            { "A", new List<string> { "B", "C" } },
            { "B", new List<string> { "A", "D" } },
            { "C", new List<string> { "A", "D" } },
            { "D", new List<string> { "B", "C", "E" } },
            { "E", new List<string> { "D" } }
        };

        // Get and print the graph structure
        string graphs = DictionaryGraph(graph);
        Console.WriteLine("Graph structure:\n" + graphs);

 // Perform BFS search and print traversal order
        string start = "A";
        string target = "E";
        List<string> bfsTraversalOrder;
        bool foundBFS = BFS_Search(graph, start, target, out bfsTraversalOrder);
        Console.WriteLine("BFS search result: " + foundBFS);
        Console.WriteLine("BFS traversal order: " + string.Join(" -> ", bfsTraversalOrder));

        // Perform DFS search and print traversal order
        HashSet<string> visitedDFS = new HashSet<string>();
        List<string> dfsTraversalOrder = new List<string>();
        bool foundDFS = DFS_Search(graph, start, target, visitedDFS, dfsTraversalOrder);
        Console.WriteLine("DFS search result: " + foundDFS);
        Console.WriteLine("DFS traversal order: " + string.Join(" -> ", dfsTraversalOrder));
        FriendSuggestuibs(graph, "B");
    }

    static void FriendSuggestuibs(Dictionary<string, List<string>> graph, string person)
    {
        if(!graph.ContainsKey(person)){
            Console.WriteLine("dont work");
            return;
        }
        var directFriends = new HashSet<string>(graph[person]);
        var suggestions = new HashSet<string>();
        var visited =  new HashSet<string>{person};
        var queue = new Queue<(string node, int depth)>();
        foreach (var friend in directFriends){
            queue.Enqueue((friend, 1));
            visited.Add(friend);
        }
        while (queue.Count > 0){
            var (current, depth) = queue.Dequeue();
            if (depth == 2 && !directFriends.Contains(current))
            suggestions.Add(current);
            if(graph.ContainsKey(current)){
                foreach(var neighboring in graph[current]){
                    if (!visited.Contains(neighboring))
                    {
                        visited.Add(neighboring);
                        queue.Enqueue((neighboring, depth + 1));
                    }
                }
            }
        }  
        Console.WriteLine("direct freidns: " + string.Join(" ", directFriends) );
        Console.WriteLine("friends  friends: " + string.Join(" ", suggestions));
    }
}