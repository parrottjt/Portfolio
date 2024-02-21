using UnityEngine;

namespace TSCore.ScriptableObject
{
    [CreateAssetMenu(menuName = "TSCore/Variables/Float Variable", fileName = "Float Variable")]

    public class FloatVariable : UnityEngine.ScriptableObject
    {
        [SerializeField] protected float value;
        public virtual float Value => value;
    }
}
