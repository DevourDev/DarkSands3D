namespace Game.AI
{
    public abstract class SensorDataBase
    {
        public SensorDataBase()
        {

        }


        /// <summary>
        /// is information actual? (was received this tick/turn)
        /// </summary>
        public bool Relevant { get; internal set; }


    }
}
