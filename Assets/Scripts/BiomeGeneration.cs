using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class BiomeGeneration : MonoBehaviour
{
    private Tilemap map;
    public Tile[] floorTiles      = new Tile[3];
    public Tile[] transitionTiles = new Tile[2];
    public GameObject[] landSpecies = new GameObject[4];
    public GameObject[] waterSpecies = new GameObject[1];
    public Tile waterTile;
    public GameObject[] backgroundLayers;
    public Texture2D sky;

    public float speciesDensity;
    public float backgroundHeight;
    public int floorHeight;
    public float backgroundScale = 1.0f;
    public bool generateBackground;
    public int minLand;
    public int maxLand;
    public int padding;
    public int tileWidth;

    #region Interval Class
    public class Interval : IComparable
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

        public Interval(int leftEnd, int rightEnd)
        {
            left = leftEnd;
            right = rightEnd;
        }

        public bool intersects(Interval other, int padding)
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

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Interval other = obj as Interval;
            if (other.Left == left) return 0;
            else if (other.Left > left) return -1;
            else return 1;
        }
    }

    #endregion

    List<Interval> GenerateLand()
    {
        int totalWidth = Root.RIGHT_EDGE - Root.LEFT_EDGE;
        int numLandSections = UnityEngine.Random.Range(minLand, maxLand+ 1);
        List<Interval> landIntervals = new List<Interval>();

        for (int i = 0; i < numLandSections; i++)
        {
            int intervalWidth = UnityEngine.Random.Range(2, totalWidth / (tileWidth * numLandSections)) * tileWidth;
            bool overlaps = true;
            int attempts = 0;
            while (overlaps && attempts < 5)
            {
                overlaps = false;
                attempts += 1;
                int intervalStart = UnityEngine.Random.Range(Root.LEFT_EDGE, Root.RIGHT_EDGE - intervalWidth);
                Interval interval = new Interval(intervalStart, intervalStart + intervalWidth);
                foreach (Interval other in landIntervals)
                {
                    if (interval.intersects(other, padding * tileWidth))
                    {
                        overlaps = true;
                        break;
                    }
                }
                if (!overlaps)
                {
                    landIntervals.Add(interval);
                }
            }
        }

        return landIntervals;
    }

    void PlaceLand(List<Interval> intervals)
    {
        foreach (Interval interval in intervals)
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

    List<Interval> GetNegativeIntervals(List<Interval> original)
    {
        List<Interval> negatives = new List<Interval>();
        int leftEnd = Root.RIGHT_EDGE;
        foreach (Interval interval in original)
        {
            int rightEnd = interval.Left - 1;
            if (rightEnd > leftEnd)
            {
                Interval newInterval = new Interval(leftEnd, rightEnd);
                negatives.Add(newInterval);
            }
            leftEnd = interval.Right + 1;
        }
        return negatives;
    }

    void PlaceSpecies(List<Interval> intervals, GameObject[] species)
    {

    }

    void printIntervals(List<Interval> intervals)
    {
        Debug.Log("-----------------");
        foreach (Interval interval in intervals)
        {
            Debug.Log("(" + interval.Left + ", " + interval.Right + ")");
        }
        Debug.Log("-----------------");
    }

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();
        List<Interval> landIntervals = GenerateLand();
        landIntervals.Sort();
        PlaceLand(landIntervals);
        List<Interval> waterIntervals = GetNegativeIntervals(landIntervals);
        if (generateBackground) GenerateBackground();
    }
}