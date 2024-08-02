using UnityEngine;

public interface IInteractable
{
    // SpriteRenderer interactSign { get; set; }
    void Interact();
    bool PlayerNearby();
}
