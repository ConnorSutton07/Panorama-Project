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
    public int tileWidth;

    #region LandInterval
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

    #endregion

    List<LandInterval> GenerateLand()
    {
        int totalWidth = Root.RIGHT_EDGE - Root.LEFT_EDGE;
        int numLandSections = Random.Range(minLand, maxLand+ 1);
        List<LandInterval> intervals = new List<LandInterval>();

        for (int i = 0; i < numLandSections; i++)
        {
            int intervalWidth = Random.Range(2, totalWidth / (tileWidth * numLandSections)) * tileWidth;
            bool overlaps = true;
            int attempts = 0;
            while (overlaps && attempts < 5)
            {
                overlaps = false;
                attempts += 1;
                int intervalStart = Random.Range(Root.LEFT_EDGE, Root.RIGHT_EDGE - intervalWidth);
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

        return intervals;
    }

    void PlaceLand(List<LandInterval> intervals)
    {
        foreach (LandInterval interval in intervals)
        {
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

    void GenerateBackground()
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

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();
        List<LandInterval> intervals = GenerateLand();
        PlaceLand(intervals);
        if (generateBackground) GenerateBackground();
    }
}