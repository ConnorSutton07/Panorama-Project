using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public Tilemap map;
    public Tile[] tiles = new Tile[3];
    public GameObject[] backgroundLayers;
    public Texture2D sky;
    public float backgroundHeight;
    public int floorHeight;
    public float backgroundScale = 1.0f;
    public bool generateBackground;

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();
        int length = Root.RIGHT_EDGE - Root.LEFT_EDGE;

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE - (length / 4); i += 3)
        {
            int tileIndex = i % 3;
            map.SetTile(new Vector3Int(i, floorHeight, 0), tiles[tileIndex]); // snow
        }
        map.SetTile(new Vector3Int(Root.RIGHT_EDGE - (length / 4) + 2, floorHeight, 0), tiles[3]); // transition
        for (int i = Root.RIGHT_EDGE - (length / 4) + 5; i <= Root.RIGHT_EDGE + 3; i += 3)
            map.SetTile(new Vector3Int(i, floorHeight, -1), tiles[4]); // water

        if (generateBackground)
        {
            for (float i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE + 16; i += (16 * backgroundScale))
            {
                for (int j = 0; j < backgroundLayers.Length; j++)
                {
                    GameObject bgLayer = Instantiate(backgroundLayers[j], new Vector3(i, backgroundHeight, -j + 5), Quaternion.identity);
                    bgLayer.transform.localScale = new Vector3(backgroundScale, backgroundScale, 1f);
                }
            }
        }
    }
}