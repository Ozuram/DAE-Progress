using UnityEngine;

public class FoodObject : CellObject
{
   public int AmountGranted = 10;
  
   public override void PlayerEntered()
    {
        Debug.Log("✅ PlayerEntered() was called on food!");

        BoardManager.CellData cell = GameManager.Instance.BoardManager.GetCellData(BoardManagerPosition());
        if (cell != null)
        {
            Debug.Log("✅ Found board cell, clearing ContainedObject.");
            cell.ContainedObject = null;
        }
        else
        {
            Debug.LogWarning("⚠️ Couldn't find the board cell for food!");
        }

        GameManager.Instance.ChangeFood(AmountGranted);
        Destroy(gameObject);
    }

   private Vector2Int m_BoardPosition;

        public void SetBoardPosition(Vector2Int pos)
        {
            m_BoardPosition = pos;
        }

        public Vector2Int BoardManagerPosition()
        {
            return m_BoardPosition;
        }

}
