using UnityEngine;

namespace Game.Characters.Interacting
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact(Character character);
    }
}
