using System.Collections;
using System.Collections.Generic;
using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.Loot.Teeth
{
	public class ToothMovementForExplosion : MonoBehaviour
	{
		public float speed;
		float distance;
		float modifiedSpeed => speed * Time.deltaTime;

		Collider2D _collider;
		List<Vector2> _movePositions;
		int _index;

		void Start()
		{
			_collider = GetComponent<Collider2D>();
			StartCoroutine(MoveToPosition());
		}

		public void SetMovePositions(List<Vector2> movePositions) => _movePositions = movePositions;

		IEnumerator MoveToPosition()
		{
			while (_index < _movePositions.Count && _collider.enabled)
			{
				if (Conditions.DistanceBetweenGreaterThan(transform.position, _movePositions[_index], .1f))
				{
					transform.position = Vector2.MoveTowards(transform.position, _movePositions[_index], modifiedSpeed);
				}
				else _index += 1;

				yield return null;
			}

			print("End");
			enabled = false;
			StopCoroutine(MoveToPosition());
			yield return null;
		}
	}
}
