using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	[SerializeField] Tower towerPrefab;

	[SerializeField] bool isPlacebale;
	public bool IsPlacebale { get { return isPlacebale; } }

	private void OnMouseDown()
	{
		if (isPlacebale)
		{
			bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
			isPlacebale = false;
		}
	}
}
