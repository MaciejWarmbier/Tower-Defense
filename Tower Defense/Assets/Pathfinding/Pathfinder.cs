using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates{get{return startCoordinates;}}
    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int EndCoordinates{get{return endCoordinates;}}

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null){
            grid = gridManager.Grid;
        }
        startNode = grid[startCoordinates];
        endNode = grid[endCoordinates];
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        GetNewPath();
    }

    public List<Node> GetNewPath(){
        gridManager.ResetNodes();
        BFS();
        return BuildPath();
    }

    void ExploreNeighbours(){
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions){
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors){
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable){
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BFS(){
        startNode.isWalkable = true;
        endNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);
        
        while(frontier.Count >0 && isRunning){
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if(currentSearchNode.coordinates == endCoordinates){
                isRunning = false;
            }
        }
   }
    
    

    List<Node> BuildPath(){
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null){
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable= false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1){
                GetNewPath();
                return true;
            }

            return false;
        }

        return false;
    }

    public void NotifyReceivers(){
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Message Sent");
    }

    
}