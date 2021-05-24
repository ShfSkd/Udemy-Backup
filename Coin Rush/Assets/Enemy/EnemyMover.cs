using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy ))]
public class EnemyMover : MonoBehaviour
{
	[SerializeField] List<Waypoint> path = new List<Waypoint>();
	[SerializeField] [Range(0,5f)] float speed = 1;

	Enemy enemy;
	private void OnEnable()
	{
		FindPath();
		ReturnToStart(); 
		StartCoroutine(FollowPath());	
	}
	private void Start()
	{
		enemy = GetComponent<Enemy>();
	}
	void FindPath()
	{
		path.Clear();

		GameObject parent = GameObject.FindGameObjectWithTag("Path");

		foreach (Transform child in parent.transform)
		{
			Waypoint waypoint = child.GetComponent<Waypoint>();
			if (waypoint != null)
				path.Add(waypoint);
		}
	}
	void ReturnToStart()
	{
		transform.position = path[0].transform.position;
	}
	IEnumerator FollowPath()
	{
		foreach (Waypoint waypoint in path)
		{
			Vector3 startPos = transform.position;
			Vector3 endPos = waypoint.transform.position;
			float travelPrecent = 0;

			transform.LookAt(endPos);


			while (travelPrecent < 1f)
			{
				travelPrecent += Time.deltaTime * speed;
				transform.position = Vector3.Lerp(startPos, endPos, travelPrecent);

				yield return new WaitForEndOfFrame();
			}
		}
		FinishPath();
	}

	private void FinishPath()
	{
		enemy.StealGold();
		gameObject.SetActive(false);
	}
}
