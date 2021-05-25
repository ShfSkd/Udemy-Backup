using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy ))]
public class EnemyMover : MonoBehaviour
{
	[SerializeField] [Range(0,5f)] float speed = 1;
	 List<Node> path = new List<Node>();

	Enemy enemy;
	GridManager gridManager;
	PathFinder pathFinder;
	private void OnEnable()
	{
		ReturnToStart(); 
		RecalculatePath(true);
	}
	private void Awake()
	{
		enemy = GetComponent<Enemy>();
		gridManager = FindObjectOfType<GridManager>();
		pathFinder = FindObjectOfType<PathFinder>();
	}
	void RecalculatePath(bool resetPath)
	{
		Vector2Int coordinates = new Vector2Int();

		if (resetPath)
		{
			coordinates = pathFinder.StartCoordinates;
		}
		else
		{
			coordinates = gridManager.GetCoordinateFromPosition(transform.position);
		}
		StopAllCoroutines();
		path.Clear();
		path = pathFinder.GetNewPath(coordinates);
		StartCoroutine(FollowPath());	
	}
	void ReturnToStart()
	{
		transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
	}
	IEnumerator FollowPath()
	{
		for (int i = 1; i < path.Count; i++)
		{
			Vector3 startPos = transform.position;
			Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].cordinates);
			float travelPrecent = 0;

			transform.LookAt(endPos);


			while (travelPrecent < 1f)
			{
				travelPrecent += Time.deltaTime * speed;
				transform.position = Vector3.Lerp(startPos, endPos, travelPrecent);
				yield return new WaitForEndOfFrame();
			}
		}
		pathFinder.NotifyReciver();
		FinishPath();
	}

	private void FinishPath()
	{
		enemy.StealGold();
		gameObject.SetActive(false);
	}
}
