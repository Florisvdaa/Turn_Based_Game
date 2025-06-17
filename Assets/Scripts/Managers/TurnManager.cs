using UnityEngine;

public enum TurnState { PlayerTurn, EnemyTurn}
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [SerializeField] private TurnState currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        float r = (int)Random.Range(1, 11);
        Debug.Log(r);
        if(r <= 5)
            currentState = TurnState.PlayerTurn;
        else 
            currentState = TurnState.EnemyTurn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
        
    }

    public void EndTurn()
    {
        currentState = (currentState == TurnState.PlayerTurn) ? TurnState.EnemyTurn : TurnState.PlayerTurn;

        Debug.Log($"Current Turn State = {currentState}");
    }

    // References
    public TurnState GetCurrentTurn() => currentState;
}
