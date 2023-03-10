using Game.Characters;
using UnityEngine;

namespace Game.AI
{
    [RequireComponent(typeof(Character))]
    public sealed class AiEvaluator : MonoBehaviour
    {
        [SerializeField] private AiStateBase _initialState;
        private Character _agent;


        public AiStateBase CurrentAiState { get; internal set; }


        private void Start()
        {
            _agent = GetComponent<Character>();
            AiManager.RegisterAiEvaluator(this);
        }


        internal void EvaluateAiState(AiContext context)
        {
            //var context = AiContext.CreateContext();
            context.Agent = _agent;
            //context.DeltaTime = deltaTime;
            CurrentAiState = CurrentAiState.Evaluate(context);
            //context.Dispose();
        }
    }
}
