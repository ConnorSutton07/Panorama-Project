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

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE - 8; i += 3)
        {
            map.SetTile(new Vector3Int(i, -5, 0), tiles[0]);
        }

        map.SetTile(new Vector3Int(Root.RIGHT_EDGE - 7, -5, 0), tiles[1]);
        for (int i = Root.RIGHT_EDGE - 6; i <= Root.RIGHT_EDGE + 3; i += 1)
        {
            map.SetTile(new Vector3Int(i, -5, 0), tiles[2]);
        }

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE + 16; i += 16)
        {
            for (int j = 0; j < backgroundLayers.Length; j++)
                Instantiate(backgroundLayers[j], new Vector3(i, -3f, -j + 5), Quaternion.identity);
        }
    }
}