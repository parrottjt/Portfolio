using System.Collections.Generic;
using UnityEngine;

namespace FrikinCore.AI.Combat.Projectiles
{
    public class HoldAttachedProjectileInfos : MonoBehaviour
    {
        List<ProjectileInfo> attachedObjects = new List<ProjectileInfo>();

        public void AddAttachedObject(ProjectileInfo attachedObject)
        {
            attachedObjects.Add(attachedObject);
        }

        public void RemoveAttachedObject(ProjectileInfo attachedObject)
        {
            attachedObjects.Remove(attachedObject);
        }

        public bool HasOtherAttachedObjects()
        {
            return attachedObjects.Count > 0;
        }
    }
}
