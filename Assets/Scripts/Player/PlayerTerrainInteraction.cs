using UnityEngine;

public class PlayerTerrainInteraction : MonoBehaviour
{
    private IInteractable hoveredTerrain;
    private IInteractable selectedTerrain;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != hoveredTerrain)
            {
                if (hoveredTerrain != null) hoveredTerrain.OnHoverExit();
                hoveredTerrain = interactable;
                if (hoveredTerrain != null) hoveredTerrain.OnHoverEnter();
            }

            if (Input.GetMouseButtonDown(0) && interactable != null)
            {
                if (selectedTerrain != null) selectedTerrain.OnDeselect();
                selectedTerrain = interactable;
                selectedTerrain.OnSelect();
            }
        }
        else
        {
            if (hoveredTerrain != null) hoveredTerrain.OnHoverExit();
            hoveredTerrain = null;
        }
    }
}
