using UnityEngine;

public class TerrainScript : MonoBehaviour, IInteractable
{
    [Header("Interface Setup")]
    [SerializeField] private bool isInteractable = true;

    [SerializeField] private GameObject terrainHoverVisual;
    [SerializeField] private GameObject terrrainSelectedVisual;

    private void Awake()
    {
        terrainHoverVisual.SetActive(false);
        terrrainSelectedVisual.SetActive(false);
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
    }

    public void OnDeselect()
    {
        terrrainSelectedVisual.SetActive(false);
    }
    // References
    public bool IsInteractable => isInteractable;
}
