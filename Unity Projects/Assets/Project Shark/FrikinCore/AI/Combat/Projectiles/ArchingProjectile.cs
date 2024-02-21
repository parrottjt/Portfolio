using System.Collections;
using FrikinCore.Player;
using TSCore.Time;
using TSCore.Utils;
using UnityEngine;
using Math = TSCore.Utils.Math;

namespace FrikinCore.AI.Combat.Projectiles
{
    /// <summary>
    /// This needs to be fixed with a better solution as the arc isn't happening
    /// </summary>

    public class ArchingProjectile : AbstractProjectile
    {
        int defaultHeight = 20;
        AnimationCurve curve;
        Vector2 lastPlayerPosition, startPos;
        Vector2 lastPos;
        float heightCheck;
        bool reachedPosition;

        void OnEnable()
        {
            lastPlayerPosition = GetTargetPosition();
            var position = transform.position;
            curve = GetComponent<ArchingPathGraph>().curve;
            startPos = position;
            StartCoroutine(nameof(Arc));
        }

        void Update()
        {
            if (!reachedPosition) return;
            transform.Translate(Vector2.up * ModifiedSpeed() * 2, Space.Self);
        }

        Vector2 GetTargetPosition() => GameManager.instance != null
            ? PlayerManager.instance.Player.transform.position
            : GameObject.Find("Player").transform.position;

        IEnumerator Arc()
        {
            float time = 0;
            DebugScript.Log_QuickTest(typeof(ArchingProjectile));
            while (time < speed)
            {
                time += TimeManager.Delta;
                float linearT = time / speed;
                float heightT = curve.Evaluate(linearT);

                float height = Mathf.Lerp(0, defaultHeight, heightT);
                lastPos = transform.position;
                Vector2 pos = Vector2.Lerp(startPos, lastPlayerPosition, linearT) + new Vector2(0f, height);

                transform.position = pos;
                var rot = Quaternion.Euler(0, 0, Math.GetRotationAngle(pos, lastPos) - 90);
                transform.rotation = rot;
                yield return rot;
            }

            print("hit");
            reachedPosition = true;
        }
    }
}
