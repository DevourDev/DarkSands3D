#define CLEAR_REMOVE_BUFFER

using System;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Game.AI
{
    public sealed class AiManager : MonoBehaviour
    {
        private const int _removeBufferSize = 1024;
        //todo: remove and implement crossframe call
        [SerializeField] private TickSystem _ticker;

        private static AiManager _inst;
        private static HashSet<AiEvaluator> _evaluators;
        private static AiEvaluator[] _removeBuffer;


        private void Awake()
        {
            _inst = this;
            _evaluators = new();
            _removeBuffer = new AiEvaluator[_removeBufferSize];
            _ticker.OnTick += HandleTick;
        }


        private void OnDestroy()
        {
            if (_inst == this)
            {
                _inst = null;
                _evaluators = null;
                _removeBuffer = null;
            }
        }

        private void HandleTick(float deltaTime)
        {
            var hs = _evaluators;
            var buffer = _removeBuffer;
            int c = -1;

            var context = AiContext.CreateContext();
            context.DeltaTime = deltaTime;

            foreach (var e in hs)
            {
                if (e == null)
                {
                    buffer[++c] = e;
                    continue;
                }

                e.EvaluateAiState(context);
            }

            context.Dispose();

            for (int i = -1; i < c;)
            {
                _ = hs.Remove(buffer[++i]);
            }

#if CLEAR_REMOVE_BUFFER
            Array.Clear(buffer, 0, c + 1);
#endif
        }


        internal static void RegisterAiEvaluator(AiEvaluator evaluator)
        {
            bool added = _evaluators.Add(evaluator);

            if (!added)
                throw new System.Exception($"element {evaluator} exists in hashset");
        }
    }
}
