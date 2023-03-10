using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.AI
{
    public abstract class ConditionBase : ScriptableObject
    {
        public abstract bool Evaluate(AiContext context);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TData GetRelevantSensorData<TData>(AiContext context, SensorSo sensor)
            where TData : SensorDataBase
        {
            return context.Agent.SensorsDataCollection.GetRelevantSensorData<TData>(context, sensor);
        }

    }
}
