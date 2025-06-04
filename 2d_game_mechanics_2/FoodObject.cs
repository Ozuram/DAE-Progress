using UnityEngine;

public class FoodObject : CellObject
{
    public int FoodValue = 10;

        public override void PlayerEntered()
    {
        Debug.Log("PlayerEntered() called on food at: " + m_Cell);
        GameManager.Instance.ChangeFood(FoodValue);
        GameManager.Instance.BoardManager.GetCellData(m_Cell).ContainedObject = null;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("FoodObject at " + m_Cell + " was destroyed.");
    }

}