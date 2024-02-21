using System.Collections;
using UnityEngine;

namespace FrikinCore.Utils
{
	public class Game_Coroutines : MonoBehaviour
	{

		public static IEnumerator SwapBetweenTwoSprites(SpriteRenderer spriteRenderer, float swapInterval,
			Sprite spriteToSwapTo)
		{
			var sprite = spriteRenderer.sprite;
			bool swap = true;

			while (true)
			{
				yield return new WaitForSeconds(swapInterval);
				spriteRenderer.sprite = swap ? spriteToSwapTo : sprite;
				swap = !swap;
			}
		}
	}
}
