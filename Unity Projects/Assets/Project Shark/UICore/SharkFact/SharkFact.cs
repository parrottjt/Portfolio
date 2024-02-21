using UnityEngine;

namespace UICore.SharkFact
{
    [CreateAssetMenu]
    public class SharkFact : ScriptableObject
    {
        public enum SharkFactType
        {
            Fact,
            Funny,
            Helpful
        }

        public SharkFactType typeOfFact;

        public enum SharkFactWorldUnlock
        {
            Always,
            World1,
            World2,
            World3,
            World4,
            World5,
            World6,
            World7,
            World8
        }

        [Tooltip("Unlocks after this world is unlocked")]
        public SharkFactWorldUnlock worldUnlock;

        [Tooltip("How many times it appears in the Array")]
        public int copiesOfFacts = 1;

        public string fact;

    }
}
