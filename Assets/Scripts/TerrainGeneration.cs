using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour
{
    private Tilemap map;
    public Tile[] floorTiles      = new Tile[3];
    public Tile[] transitionTiles = new Tile[2];
    public Tile waterTile;
    public GameObject[] backgroundLayers;
    public Texture2D sky;

    public float backgroundHeight;
    public int floorHeight;
    public float backgroundScale = 1.0f;
    public bool generateBackground;
    public int minLand;
    public int maxLand;
    public int padding;
    public int leftEnd;
    public int rightEnd;
    public int tileWidth;

    public class LandInterval
    {
        private int left;
        private int right;

        public int Left
        {
            get => left;
        }

        public int Right
        {
            get => right;
        }

        public LandInterval(int leftEnd, int rightEnd)
        {
            left = leftEnd;
            right = rightEnd;
        }

        public bool intersects(LandInterval other, int padding)
        {
            if (Mathf.Max(right, other.Right) - Mathf.Min(left, other.Left) < (right - left + other.Right - other.Left + padding))
                return true;
            return false;
        }

        public bool contains(int point)
        {
            if (point >= left && point <= right)
                return true;
            return false;
        }
    }

    void Generate()
    {
        int totalWidth = rightEnd - leftEnd;
        int numLandSections = Random.Range(minLand, maxLand+ 1);
        List<LandInterval> intervals = new List<LandInterval>();

        Debug.Log("Sections: " + numLandSections);
        
        for (int i = 0; i < numLandSections; i++)
        {
            int intervalWidth = Random.Range(2, totalWidth / (tileWidth * numLandSections)) * tileWidth;
            Debug.Log("Width: " + intervalWidth);
            bool overlaps = true;
            int attempts = 0;
            while (overlaps && attempts < 5)
            {
                overlaps = false;
                attempts += 1;
                int intervalStart = Random.Range(leftEnd, rightEnd - intervalWidth);
                LandInterval interval = new LandInterval(intervalStart, intervalStart + intervalWidth);
                foreach (LandInterval other in intervals)
                {
                    if (interval.intersects(other, padding * tileWidth))
                    {
                        overlaps = true;
                        break;
                    }
                }
                if (!overlaps)
                {
                    intervals.Add(interval);
                }
            }
        }   

        for (int i = leftEnd; i < rightEnd; i += tileWidth)
        {
            bool place = true;
            foreach (LandInterval interval in intervals)
            {
                if (interval.contains(i))
                {
                    place = false;
                    break;
                }
            }
            if (place)
            {
                map.SetTile(new Vector3Int(i, floorHeight, 0), waterTile);
            }
        }

        foreach (LandInterval interval in intervals)
        {
            Debug.Log("Here");
            Debug.Log(interval.Left + " " + interval.Right);
            map.SetTile(new Vector3Int(interval.Left, floorHeight, 0), transitionTiles[0]);
            map.SetTile(new Vector3Int(interval.Right, floorHeight, 0), transitionTiles[1]);
            int j = 1;
            for (int i = interval.Left + tileWidth; i < interval.Right; i += tileWidth)
            {
                int tileIndex = j % floorTiles.Length;
                map.SetTile(new Vector3Int(i, floorHeight, 0), floorTiles[tileIndex]);
            }
        }
    }

    void Start()
    {
        
        map = gameObject.GetComponent<Tilemap>();
        /*
        int length = Root.RIGHT_EDGE - Root.LEFT_EDGE;

        for (int i = Root.LEFT_EDGE; i <= Root.RIGHT_EDGE - (length / 4); i += 3)
        {
            int tileIndex = i % 3;
            map.SetTile(new Vector3Int(i, floorHeight, 0), tiles[tileIndex]); // snow
        }
        map.SetTile(new Vector3Int(Root.RIGHT_EDGE - (length / 4) + 2, floorHeight, 0), tiles[3]); // transition
        for (int i = Root.RIGHT_EDGE - (length / 4) + 5; i <= Root.RIGHT_EDGE + 3; i += 3)
            map.SetTile(new Vector3Int(i, floorHeight, -1), tiles[4]); // water
        */

        Generate();

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