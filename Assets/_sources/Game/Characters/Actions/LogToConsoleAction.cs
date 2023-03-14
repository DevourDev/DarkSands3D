using UnityEngine;

namespace Game.Characters.Actions
{
    [CreateAssetMenu(menuName = GameAssetsConstants.CharacterActions + "Log To Console")]
    public sealed class LogToConsoleAction : CharacterActionBase
    {
        [SerializeField] private string _msg;


        public override void Act(Character actor)
        {
            Debug.Log($"LogToConsoleAction ({actor.name}): {_msg}");
        }
    }
}
