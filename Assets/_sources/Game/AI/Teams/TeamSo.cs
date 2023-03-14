using UnityEngine;

namespace Game.AI
{
    [CreateAssetMenu(menuName = GameAssetsConstants.Ai + "Team")]
    public sealed class TeamSo : ScriptableObject
    {
        [SerializeField] private string _teamName;
        [SerializeField] private Color _teamColor;


        public string TeamName => _teamName;
        public Color TeamColor => _teamColor;


        //torefactor: move to helpers class
        public static AllyMode GetAllyMode(TeamSo teamA, TeamSo teamB)
        {
            //extended diplomacy system is overkill in this game,
            //so TEAM is more like SIDE

            if (teamA == teamB)
                return AllyMode.Allies;

            return AllyMode.Enemies;
        }
    }
}
