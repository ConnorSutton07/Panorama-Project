using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public Tilemap map;
    public Tile[] tiles = new Tile[2];
    
    
    void Start() 
    {
        //map.SetTile(new Vector3Int(i, j, 0), tile);
        map = gameObject.GetComponent<Tilemap>();
        //for 
        for (int i = Constants.LEFT_EDGE; i <= Constants.RIGHT_EDGE; i+=3)
        {
            map.SetTile(new Vector3Int(i, -4, 0), tiles[0]);
            map.SetTile(new Vector3Int(i, -5, 0), tiles[1]);
            //map.SetTile(new Vector3Int(i, -5, 0), tiles[1]);
        }
        
    }

    
    void Update ()
    {
        
    }
}