using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get{return grid;}}
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSizeSnap = 10;
    public int UnityGridSizeSnap{get { return unityGridSizeSnap;}}

    private void Awake() {
        CreateGrid();
    }

    void CreateGrid()
    {
        for(int x= 0; x<gridSize.x; x++){
            for(int y=0; y<gridSize.y; y++){
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates,true));
                Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }

    public Node GetNode(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            grid[coordinates].isWalkable = false;
        }
    }
    public void ResetNodes(){
        foreach(KeyValuePair<Vector2Int,Node> entry in grid){
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath =  false;
        }
    }

    public Vector2Int GetCoordinatesFromPosotion(Vector3 position){
        Vector2Int coordinates = new Vector2Int();

        
        coordinates.x = Mathf.RoundToInt(position.x/unityGridSizeSnap);
        coordinates.y = Mathf.RoundToInt(position.z/unityGridSizeSnap);
        
        return coordinates;
    }

    public Vector3 GetPostionFromCoordinates(Vector2 coordinates){
    Vector3 position = new Vector3();
    
    position.x = coordinates.x * unityGridSizeSnap;
    position.z = coordinates.y  *unityGridSizeSnap;
    
    return position;
    }
}
