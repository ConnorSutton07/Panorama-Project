using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public Tilemap map;
    public Tile[] tiles = new Tile[3];
    public GameObject[] backgroundLayers;

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();
        int length = Root.RIGHT_EDGE - Root.LEFT_EDGE;

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE - (length / 4); i += 3)
            map.SetTile(new Vector3Int(i, -5, 0), tiles[0]); // snow
        map.SetTile(new Vector3Int(Root.RIGHT_EDGE - (length / 4) + 2, -5, 0), tiles[1]); // transition
        for (int i = Root.RIGHT_EDGE - (length / 4) + 3; i <= Root.RIGHT_EDGE + 3; i += 1)
            map.SetTile(new Vector3Int(i, -5, -1), tiles[2]); // water

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE + 16; i += 16)
        {
            for (int j = 0; j < backgroundLayers.Length; j++)
                Instantiate(backgroundLayers[j], new Vector3(i, -3.2f, -j + 5), Quaternion.identity);
        }
    }
}