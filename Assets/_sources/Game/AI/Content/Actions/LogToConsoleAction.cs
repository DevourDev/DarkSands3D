using Game.Characters;
using UnityEngine;

namespace Game.AI
{
    [CreateAssetMenu(menuName = "Ai/Actions/Log To Console")]
    public sealed class LogToConsoleAction : AiActionSo
    {
        [SerializeField] private string _msg;

        public override void Act(AiContext context)
        {
            Debug.Log($"LogToConsoleAction ({context.Agent.name}): {_msg}");
        }
    }
}
