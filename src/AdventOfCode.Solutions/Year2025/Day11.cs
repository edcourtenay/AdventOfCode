namespace AdventOfCode.Solutions.Year2025;

[Description("Reactor")]
public class Day11 : IPuzzle
{
    public object Part1(string input)
    {
        var graph = ParseInput(input);
        return CountPaths(graph, "you", "out");
    }

    public object Part2(string input)
    {
        var graph = ParseInput(input);
        return CountPaths(graph, "svr", "out", ["fft", "dac"]);
    }

    private static long CountPaths(Graph graph, string startName, string endName, string[] required)
    {
        return required.Permutations()
            .Sum(permutation => ((string[])[startName, ..permutation, endName])
                .SlidingWindow(2)
                .Select(x => x.ToArray())
                .Aggregate(1L, (current, pair) => current * CountPaths(graph, pair[0], pair[1])));
    }
    
    public static int CountPaths(Graph graph, string startName, string endName)
    {
        var startNode = graph.GetNode(startName);
        var endNode = graph.GetNode(endName);
        
        if (startNode == null || endNode == null || startNode == endNode)
            return startNode == endNode ? 1 : 0;
        
        return CountPaths(startNode, endNode, [], [], []);
    }

    private static int CountPaths(GraphNode node, GraphNode endNode, Dictionary<GraphNode, int> pathCounts, HashSet<GraphNode> recursionStack, HashSet<GraphNode> visited)
    {
        var stack = new Stack<StackFrame>();
        stack.Push(new StackFrame(node, 0));
        var results = new Dictionary<GraphNode, int>();
        
        while (stack.Count > 0)
        {
            var frame = stack.Peek();
            var currentNode = frame.Node;
            
            // Base case: reached the end node
            if (currentNode == endNode)
            {
                results[currentNode] = 1;
                stack.Pop();
                continue;
            }
            
            // Check if we already have a cached result
            if (pathCounts.TryGetValue(currentNode, out int cached))
            {
                results[currentNode] = cached;
                stack.Pop();
                continue;
            }
            
            // Check for cycles
            if (recursionStack.Contains(currentNode) && frame.ChildIndex == 0)
            {
                results[currentNode] = 0;
                stack.Pop();
                continue;
            }
            
            // First visit: mark node as being processed
            if (frame.ChildIndex == 0)
            {
                recursionStack.Add(currentNode);
                visited.Add(currentNode);
            }
            
            // Process children
            if (frame.ChildIndex < currentNode.Children.Count)
            {
                var child = currentNode.Children[frame.ChildIndex];
                frame.ChildIndex++;
                
                if (!visited.Contains(child))
                {
                    stack.Push(new StackFrame(child, 0));
                }
            }
            else
            {
                // All children processed, calculate result
                int count = 0;
                foreach (var child in currentNode.Children)
                {
                    if (results.TryGetValue(child, out int childCount))
                    {
                        count += childCount;
                    }
                }
                
                recursionStack.Remove(currentNode);
                visited.Remove(currentNode);
                
                pathCounts[currentNode] = count;
                results[currentNode] = count;
                stack.Pop();
            }
        }
        
        return results.GetValueOrDefault(node, 0);
    }
    
    private class StackFrame
    {
        public GraphNode Node { get; }
        public int ChildIndex { get; set; }
        
        public StackFrame(GraphNode node, int childIndex)
        {
            Node = node;
            ChildIndex = childIndex;
        }
    }

    public static Graph ParseInput(string input)
    {
        var nodes = new Dictionary<string, GraphNode>();
        
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        foreach (var line in lines)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);
            if (parts.Length != 2) continue;
            
            var nodeName = parts[0].Trim();
            var childNames = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            
            // Get or create the parent node
            if (!nodes.TryGetValue(nodeName, out GraphNode? parentNode))
            {
                parentNode = new GraphNode(nodeName);
                nodes[nodeName] = parentNode;
            }

            // Get or create child nodes and link them
            foreach (var childName in childNames)
            {
                if (!nodes.TryGetValue(childName, out GraphNode? value))
                {
                    value = new GraphNode(childName);
                    nodes[childName] = value;
                }
                parentNode.Children.Add(value);
            }
        }
        
        return new Graph(nodes);
    }

    public class Graph
    {
        public Dictionary<string, GraphNode> Nodes { get; }
        
        public Graph(Dictionary<string, GraphNode> nodes)
        {
            Nodes = nodes;
        }
        
        public GraphNode? GetNode(string name) => Nodes.GetValueOrDefault(name);
    }
    
    public class GraphNode
    {
        public string Name { get; }
        public List<GraphNode> Children { get; }
        
        public GraphNode(string name)
        {
            Name = name;
            Children = [];
        }
    }
}