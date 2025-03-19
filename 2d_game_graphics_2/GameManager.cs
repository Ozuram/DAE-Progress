using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
  
   public BoardManager BoardManager;
   public PlayerController PlayerController;
   public UIDocument UIDoc;
   private Label m_FoodLabel;

   public TurnManager TurnManager { get; private set;}

   private void Awake()
   {
       if (Instance != null)
       {
           Destroy(gameObject);
           return;
       }
      
       Instance = this;
   }
    private int m_FoodAmount = 100;
    

    void Start()
    {
    TurnManager = new TurnManager();
    TurnManager.OnTick += OnTurnHappen;

    m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
    m_FoodLabel.text = "Food : " + m_FoodAmount;
  
    BoardManager.Init();
    PlayerController.Spawn(BoardManager, new Vector2Int(1,1));
    }

    void OnTurnHappen()
    {
    m_FoodAmount -= 1;
    Debug.Log("Current amount of food : " + m_FoodAmount);
    }
}
