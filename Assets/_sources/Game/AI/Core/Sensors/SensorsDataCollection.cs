using System;
using System.Collections.Generic;

namespace Game.AI
{
    public sealed class SensorsDataCollection
    {
        private sealed class SensorDatasInfo
        {
            private readonly DataBucket[] _buckets;


            public SensorDatasInfo(SensorSo sensorSo)
            {
                _buckets = new DataBucket[sensorSo.DataClassesCount];
            }


            public bool Relevant { get; set; }


            public SensorDataBase FindData(System.Type type)
            {
                var arr = _buckets;
                int c = arr.Length;

                for (int i = 0; i < c; i++)
                {
                    var b = arr[i];
                    var t = b.DataType;

                    if (t == null)
                        break;

                    if (t == type)
                        return b.Data;
                }

                return null;
            }

            public void RegisterData(SensorDataBase data)
            {
                var arr = _buckets;
                int c = arr.Length;

                for (int i = 0; i < c; i++)
                {
#if UNITY_EDITOR
                    if (arr[i].DataType == data.GetType())
                    {
                        throw new Exception("unexpected behaviour");
                    }
#endif
                    if (arr[i].DataType == null)
                    {
                        arr[i] = new DataBucket(data.GetType(), data);
                        return;
                    }
                }
            }
        }


        private readonly struct DataBucket
        {
            public readonly System.Type DataType;
            public readonly SensorDataBase Data;


            public DataBucket(Type dataType, SensorDataBase data)
            {
                DataType = dataType;
                Data = data;
            }
        }


        private readonly Dictionary<SensorSo, SensorDatasInfo> _datas = new();


        /// <summary>
        /// for sensors
        /// </summary>
        internal TSensorData GetOrCreateSensorData<TSensorData>(SensorSo sensor)
            where TSensorData : SensorDataBase, new()
        {
            if (!TryGetSensorData(sensor, out TSensorData sensorData))
            {
                sensorData = new();
                RegisterSensorData(sensor, sensorData);
            }
            return sensorData;
        }

        private void RegisterSensorData<TSensorData>(SensorSo sensor, TSensorData data) where TSensorData : SensorDataBase, new()
        {
            var bucket = new DataBucket(typeof(TSensorData), data);

            if (!_datas.TryGetValue(sensor, out var container))
            {
                container = new(sensor);
                _datas.Add(sensor, container);
            }

            container.RegisterData(data);

#if UNITY_EDITOR
            throw new Exception("unexpected code reached");
#endif
        }



        internal TSensorData GetRelevantSensorData<TSensorData>(AiContext context, SensorSo sensor)
            where TSensorData : SensorDataBase
        {
            if (!TryGetSensorData(sensor, out TSensorData data))
            {
                sensor.Scan(context);
                return (TSensorData)_datas[sensor].FindData(typeof(TSensorData));
            }

            return data;
        }


        /// <summary>
        /// for analyzers
        /// </summary>
        internal bool TryGetSensorData<TSensorData>(SensorSo sensor, out TSensorData data)
            where TSensorData : SensorDataBase
        {
            if (!_datas.TryGetValue(sensor, out var container))
            {
                data = default;
                return false;
            }

#if UNITY_EDITOR
            if (container == null)
            {
                string msgStart = $"Requested SensorData for sensor {sensor} of type {typeof(TSensorData)}";
                throw new System.NullReferenceException($"{msgStart} is NULL (buckets)");
            }
#endif

            data = container.FindData(typeof(TSensorData)) as TSensorData;
            return data != null;
        }


        public void MarkAllAsIrrelevant()
        {
            var values = _datas.Values;

            foreach (var v in values)
            {
                v.Relevant = false;
            }
        }
    }
}
