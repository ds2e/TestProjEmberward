using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable{ get{ return isPlaceable; }}
    
    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start() {
        if(gridManager != null){
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable){
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown() {
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.willBlockPath(coordinates)){
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;

            gridManager.BlockNode(coordinates);
            pathFinder.NotifyRecievers();
        }
    }
}
