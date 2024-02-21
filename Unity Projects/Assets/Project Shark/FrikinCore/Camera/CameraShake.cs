using System.Collections;
using TSCore.Time;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FrikinCore.CameraControl
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] float cameraShakeTime;
        [SerializeField] float cameraShakeAmount = 0.7f;

        Transform cameraTransform;
        float time;

        public void ShakeCamera()
        {
            cameraTransform = GameManager.instance.MainCamera.transform;
            time = cameraShakeTime;
            StartCoroutine(nameof(Shake));
        }

        IEnumerator Shake()
        {
            var _originalCameraPosition = cameraTransform.localPosition;
            print(time);
            while (time > 0)
            {
                cameraTransform.localPosition = _originalCameraPosition + Random.insideUnitSphere * cameraShakeAmount;
                time = time - TimeManager.GlobalDelta;
                yield return new WaitForEndOfFrame();
            }

            cameraTransform.localPosition = _originalCameraPosition;
        }
    }
}
