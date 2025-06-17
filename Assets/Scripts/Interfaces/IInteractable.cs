using UnityEngine;

public interface IInteractable 
{
    bool IsInteractable { get; }    // Check if interaction is allowed
    void OnHoverEnter();
    void OnHoverExit();
    void OnSelect();
    void OnDeselect();
}
