using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] Tower towerPrefab;

	[SerializeField] bool isPlacebale;

	GridManager gridManager;
	PathFinder pathFinder;
	Vector2Int coordinates = new Vector2Int();
	public bool IsPlacebale { get { return isPlacebale; } }

	private void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		pathFinder = FindObjectOfType<PathFinder>();
	}
	private void Start()
	{
		if (gridManager != null)
		{
			coordinates = gridManager.GetCoordinateFromPosition(transform.position);
			if (!isPlacebale)
			{
				gridManager.BlockNode(coordinates);
			}
		}
	}
	private void OnMouseDown()
	{
		if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
		{
			bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

			if (isSuccessful)
			{
				gridManager.BlockNode(coordinates);
				pathFinder.NotifyReciver();
			}
		}
	}
}
