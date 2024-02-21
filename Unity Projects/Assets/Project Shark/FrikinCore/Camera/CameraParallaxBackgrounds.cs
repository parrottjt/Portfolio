using System.Collections.Generic;
using UICore;
using UnityEngine;

namespace FrikinCore.CameraControl
{
	public class CameraParallaxBackgrounds : MonoBehaviour
	{
		[SerializeField] MeshRenderer farParallaxBackground, midParallaxBackground, closeParallaxBackground;

		readonly List<Transform> materialTransformsList = new List<Transform>();

		bool ParallaxNeedsToScale()
		{
			return GameManager.GameSettings[Settings.IsBossScene] || GameManager.GameSettings[Settings.MiniBossActive]
			                                                      || GameManager.instance.MainCamera.orthographicSize >
			                                                      20;
		}

		// Use this for initialization
		void Start()
		{
			materialTransformsList.Add(farParallaxBackground.transform);
			materialTransformsList.Add(midParallaxBackground.transform);
			materialTransformsList.Add(closeParallaxBackground.transform);

			foreach (var materialTransform in materialTransformsList)
			{
				materialTransform.gameObject.SetActive(true);
			}

			farParallaxBackground.material =
				UIManager.instance.BackgroundMaterialChanger.GetWorldBackgroundMaterials().far;
			midParallaxBackground.material =
				UIManager.instance.BackgroundMaterialChanger.GetWorldBackgroundMaterials().mid;
			closeParallaxBackground.material =
				UIManager.instance.BackgroundMaterialChanger.GetWorldBackgroundMaterials().close;
		}

		void Update()
		{
			foreach (var materialTransform in materialTransformsList)
			{
				materialTransform.localScale =
					ParallaxNeedsToScale() ? new Vector3(138.667f, 52) : new Vector3(109.33f, 41);
			}
		}
	}
}
