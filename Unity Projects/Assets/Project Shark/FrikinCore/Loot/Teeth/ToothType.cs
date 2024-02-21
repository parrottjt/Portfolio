using UnityEngine;

namespace FrikinCore.Loot.Teeth
{
	public enum ToothPickTypes
	{
		Tooth,
		Teeth,
		GoldenTooth
	}
	
	//Script name may change depending on if this becomes wider reaching
	public class ToothType : MonoBehaviour
	{
		[SerializeField] ToothPickTypes typeOfTooth;
		public ToothPickTypes TypeOfTooth => typeOfTooth;

		//This is a function just for testing, this may stay depending on what happens to this script
		public void TurnOffGemBrush() => GetComponent<SpriteRenderer>().enabled = false;
		public void TurnOnGemBrush() => GetComponent<SpriteRenderer>().enabled = true;
	}

}