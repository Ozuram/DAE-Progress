using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
   public TileBase ObstacleTile;
   public int MaxHealth = 3;

   private int m_HealthPoint;
   private TileBase m_OriginalTile;
  
   public override void Init(Vector2Int cell)
   {
       base.Init(cell);

       m_HealthPoint = MaxHealth;
      
       m_OriginalTile = GameManager.Instance.BoardManager.GetCellTile(cell);
       GameManager.Instance.BoardManager.SetCellTile(cell, ObstacleTile);
   }

   public override bool PlayerWantsToEnter()
   {
       m_HealthPoint -= 1;

       if (m_HealthPoint > 0)
       {
           return false;
       }

       GameManager.Instance.BoardManager.SetCellTile(m_Cell, m_OriginalTile);
       Destroy(gameObject);
       return true;
   }
}

