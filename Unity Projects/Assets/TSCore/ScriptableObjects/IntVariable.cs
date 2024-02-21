using UnityEngine;

namespace TSCore.ScriptableObject
{
    [CreateAssetMenu(menuName = "TSCore/Variables/Int Variable", fileName = "Int Variable")]

    public class IntVariable : UnityEngine.ScriptableObject
    {
        [SerializeField] int value;
        public virtual int Value => value;
    }
}
