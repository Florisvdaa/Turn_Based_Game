using UnityEngine;

public enum TerrainType
{
    Grass,
    Water,
    HighTerrain
}
public class TerrainScript : MonoBehaviour, IInteractable
{
    [Header("Interface Setup")]
    [SerializeField] private bool isInteractable = true;

    [SerializeField] private GameObject terrainRangeHighLight;
    [SerializeField] private GameObject terrainHoverVisual;
    [SerializeField] private GameObject terrrainSelectedVisual;

    [SerializeField] private Transform targetPosition;

    [Header("Terrain Type")]
    [SerializeField] private TerrainType terrainType;

    private PlayerTank playerTank; 

    private void Awake()
    {
        ShowAsReachable(false);

        terrainHoverVisual.SetActive(false);
        terrrainSelectedVisual.SetActive(false);

        playerTank = GameObject.Find("Player Tank").GetComponent<PlayerTank>();

        Vector2Int coord = new Vector2Int(Mathf.RoundToInt(targetPosition.position.x), Mathf.RoundToInt(targetPosition.position.z));
        GridManager.Instance.RegisterTile(coord, this);

    }
    public void OnHoverEnter()
    {
        if(IsInteractable)
            terrainHoverVisual.SetActive(true);
    }
    public void OnHoverExit()
    {
        terrainHoverVisual.SetActive(false);
    }
    public void OnSelect()
    {
        if (IsInteractable)
            terrrainSelectedVisual.SetActive(true);

        if (playerTank != null)
            playerTank.TryMove(targetPosition.position);
    }
    public void OnDeselect()
    {
        terrrainSelectedVisual.SetActive(false);
    }

    public void ShowAsReachable(bool show)
    {
        if (terrainRangeHighLight != null)
            terrainRangeHighLight.SetActive(show);
    }
    // References
    public bool IsInteractable => isInteractable;
    public TerrainType GetTerrainType() => terrainType;
    public Vector2Int GetGridPos() => new(Mathf.RoundToInt(targetPosition.position.x), Mathf.RoundToInt(targetPosition.position.z));
}
