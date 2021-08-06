using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public Tilemap map;
    public Tile[] tiles = new Tile[2];
    //public GameObject layer1;

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE + 3; i += 3)
        {
            map.SetTile(new Vector3Int(i, -5, 0), tiles[0]);
            //map.SetTile(new Vector3Int(i, -5, 0), tiles[1]);
        }
        /*for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE + 16; i += 16)
        {
            Instantiate(layer1, new Vector3(i, -0.2f, 5), Quaternion.identity);
            //newPart.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .25f * i);
        }*/
    }
}