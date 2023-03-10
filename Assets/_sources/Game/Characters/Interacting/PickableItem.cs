using UnityEngine;

namespace Game.Characters.Interacting
{
    public sealed class PickableItem : Interactable
    {
        public override void Interact(Character character)
        {
            Debug.Log("picked up item " + name);
            Destroy(gameObject);
        }
    }
}
