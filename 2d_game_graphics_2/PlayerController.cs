using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   private BoardManager m_Board;
   private Vector2Int m_CellPosition;
    
    public Vector2Int Cell
    {
        get { return m_CellPosition; }
    }

   


   public void Spawn(BoardManager boardManager, Vector2Int cell)
   {
       m_Board = boardManager;
       MoveTo(cell);
   }
  
   public void MoveTo(Vector2Int cell)
   {
       m_CellPosition = cell;
       transform.position = m_Board.CellToWorld(m_CellPosition);
   }

  
   private void Update()
   {
       Vector2Int newCellTarget = m_CellPosition;
       bool hasMoved = false;

       if (Time.timeScale > 0f) // Only allow movement when game is not paused
        {
    if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
            newCellTarget.y += 1;
            hasMoved = true;
            }
    else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
            newCellTarget.y -= 1;
            hasMoved = true;
            }
    else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
            newCellTarget.x -= 1;
            hasMoved = true;
            }
    else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
            newCellTarget.x += 1;
            hasMoved = true;
            }
        }


       if(hasMoved)
        {
            //check if the new position is passable, then move there if it is.
            BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);

            if (cellData != null && cellData.Passable)
            {
                GameManager.Instance.TurnManager.Tick();

                MoveTo(newCellTarget); // Player moves first

                // Refresh cell data at new position
                cellData = m_Board.GetCellData(m_CellPosition);

                if (cellData.ContainedObject != null)
                {
                    cellData.ContainedObject.PlayerEntered();
                }
            }
        }
    }
}