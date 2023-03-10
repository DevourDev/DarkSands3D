using System.Runtime.CompilerServices;
using Game.Characters;
using UnityEngine;

namespace Game.AI
{
    public abstract class SensorSo : ScriptableObject
    {
        internal virtual int DataClassesCount { get; } = 1;


        public abstract void Scan(AiContext context);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TData GetSensorData<TData>(Character agent)
            where TData : SensorDataBase, new()
        {
            return agent.SensorsDataCollection.GetOrCreateSensorData<TData>(this);
        }
    }

}
