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

   public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Tilemap.GetCellCenterWorld((Vector3Int)cellIndex);
    }

   private CellData[,] m_BoardData;

   private Tilemap m_Tilemap;


   public int Width;
   public int Height;
   public Tile[] GroundTiles;
   public Tile[] BlockingTiles;
   public FoodObject FoodPrefab;
   public List<Vector2Int> m_EmptyCellsList;


   // Start is called before the first frame update
    public PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
    m_Tilemap = GetComponentInChildren<Tilemap>();
    
    m_BoardData = new CellData[Width, Height];

    for (int y = 0; y < Height; ++y)
    {
        for(int x = 0; x < Width; ++x)
        {
            Tile tile;
            m_BoardData[x, y] = new CellData();

            if(x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
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
    public CellData GetCellData(Vector2Int cellIndex)
    {
    if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height)
    {
        return null;
    }

    return m_BoardData[cellIndex.x, cellIndex.y];
    }
    void GenerateFood()
    {
        int foodCount = 5;
        for (int i = 0; i < foodCount; ++i)
        {
            int randomX = Random.Range(1, Width-1);
            int randomY = Random.Range(1, Height-1);
            CellData data = m_BoardData[randomX, randomY];
            if (data.Passable && data.ContainedObject == null)
            {
                FoodObject newFood = Instantiate(FoodPrefab);
                newFood.transform.position = CellToWorld(new Vector2Int(randomX, randomY));
                data.ContainedObject = newFood;
            }
        }
    }

    
public void Init()
{
   m_Tilemap = GetComponentInChildren<Tilemap>();
   //Initialize the list
   m_EmptyCellsList = new List<Vector2Int>();
  
   m_BoardData = new CellData[Width, Height];


   for (int y = 0; y < Height; ++y)
   {
       for(int x = 0; x < Width; ++x)
       {
           Tile tile;
           m_BoardData[x, y] = new CellData();
          
           if(x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
           {
               tile = BlockingTiles[Random.Range(0, BlockingTiles.Length)];
               m_BoardData[x, y].Passable = false;
           }
           else
           {
               tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
               m_BoardData[x, y].Passable = true;
              
               //this is a passable empty cell, add it to the list!
               m_EmptyCellsList.Add(new Vector2Int(x, y));
           }
          
           m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
       }
   }
  
   //remove the starting point of the player! It's not empty, the player is there
   m_EmptyCellsList.Remove(new Vector2Int(1, 1));
   GenerateFood();
}}