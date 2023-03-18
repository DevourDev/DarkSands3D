using DevourDev.Unity.ScriptableObjects;
using UnityEngine;

namespace Game.Characters.Actions
{
    [CreateAssetMenu(menuName = GameAssetsConstants.CharacterActions + "Database")]
    public sealed class CharacterActionsDatabase : ScriptableObjectsDatabase<CharacterActionBase>
    {

    }
}
