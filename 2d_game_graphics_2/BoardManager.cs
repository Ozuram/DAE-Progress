using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public class CellData
    {
        public bool Passable;
        public CellObject ContainedObject;
    }

    private CellData[,] m_BoardData;
    private Tilemap m_Tilemap;

    public int Width;
    public int Height;
    public Tile[] GroundTiles;
    public Tile[] BlockingTiles;
    public FoodObject FoodPrefab;
    public List<Vector2Int> m_EmptyCellsList;
    public ExitCellObject ExitCellPrefab;

    public PlayerController Player;

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Tilemap.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    void Start()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_BoardData = new CellData[Width, Height];

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = BlockingTiles[Random.Range(0, BlockingTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        Player.Spawn(this, new Vector2Int(1, 1));
    }

    public void Init()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_EmptyCellsList = new List<Vector2Int>();
        m_BoardData = new CellData[Width, Height];

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = BlockingTiles[Random.Range(0, BlockingTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;

                    m_EmptyCellsList.Add(new Vector2Int(x, y));
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        m_EmptyCellsList.Remove(new Vector2Int(1, 1));
        GenerateFood();
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height)
        {
            return null;
        }

        return m_BoardData[cellIndex.x, cellIndex.y];
    }

    public void SetCellTile(Vector2Int cellIndex, TileBase tile)
    {
        m_Tilemap.SetTile(new Vector3Int(cellIndex.x, cellIndex.y, 0), tile);
    }

    void AddObject(CellObject obj, Vector2Int coord)
    {
        CellData data = m_BoardData[coord.x, coord.y];
        obj.transform.position = CellToWorld(coord);
        data.ContainedObject = obj;
        obj.Init(coord);
    }

        void GenerateFood()
        {
            int foodCount = 5;

            for (int i = 0; i < foodCount && m_EmptyCellsList.Count > 0; ++i)
            {
                int index = Random.Range(0, m_EmptyCellsList.Count);
                Vector2Int pos = m_EmptyCellsList[index];
                m_EmptyCellsList.RemoveAt(index);

                CellData data = m_BoardData[pos.x, pos.y];

                if (data.Passable && data.ContainedObject == null)
                {
                    FoodObject newFood = Instantiate(FoodPrefab);
                    newFood.Init(pos);
                    newFood.transform.position = CellToWorld(pos);

                    // 🟢 Make absolutely sure this line is running
                    data.ContainedObject = newFood;

                    Debug.Log("Food spawned at: " + pos);
                }
            
            }
        }


    
    public TileBase GetCellTile(Vector2Int cellIndex)
    {
        return m_Tilemap.GetTile(new Vector3Int(cellIndex.x, cellIndex.y, 0));
    }

}
