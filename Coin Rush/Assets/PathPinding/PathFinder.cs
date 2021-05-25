using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
	[SerializeField] Vector2Int startCoordinates;
	public Vector2Int StartCoordinates { get { return startCoordinates; } }
	[SerializeField] Vector2Int destinationCoordinates;
	public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

	Node startNode;
	Node destinationNode;
	Node currentSearceNode;

	Queue<Node> frontier = new Queue<Node>();
	Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

	Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
	GridManager gridManager;
	Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

	private void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		if (gridManager != null)
		{
			grid = gridManager.Grid;
			startNode = grid[startCoordinates];
			destinationNode = grid[destinationCoordinates];
		}
	}
	private void Start()
	{
		GetNewPath();
	}
	public List<Node> GetNewPath()
	{
		return GetNewPath(StartCoordinates);
	}
	public List<Node> GetNewPath(Vector2Int coordinates)
	{
		gridManager.ResetNode();
		BreadthFirstSearch(coordinates);
		return BuildPath();
	}
	private void ExploreNeighbors()
	{
		List<Node> neighbors = new List<Node>();

		foreach (Vector2Int dir in directions)
		{
			Vector2Int neighborChoords = currentSearceNode.cordinates + dir;
			if (grid.ContainsKey(neighborChoords))
			{
				neighbors.Add(grid[neighborChoords]);
			}
		}
		foreach (Node nehighbor in neighbors)
		{
			if (!reached.ContainsKey(nehighbor.cordinates) && nehighbor.isWalkable)
			{
				nehighbor.connectedTo = currentSearceNode;
				reached.Add(nehighbor.cordinates, nehighbor);
				frontier.Enqueue(nehighbor);
			}
		}
	}
	void BreadthFirstSearch(Vector2Int coordinates)
	{
		startNode.isWalkable = true;
		destinationNode.isWalkable = true;

		frontier.Clear();
		reached.Clear();

		bool isRunning = true;
		 
		frontier.Enqueue(grid[coordinates]);
		reached.Add(coordinates, grid[coordinates]);

		while (frontier.Count > 0 && isRunning)
		{
			currentSearceNode = frontier.Dequeue();
			currentSearceNode.isExplored = true;
			ExploreNeighbors();

			if (currentSearceNode.cordinates == destinationCoordinates)
				isRunning = true;
		}
	}
	List<Node> BuildPath()
	{
		List<Node> path = new List<Node>();
		Node currentNode = destinationNode;

		path.Add(currentNode);
		currentNode.isPath = true;

		while (currentNode.connectedTo != null)
		{
			currentNode = currentNode.connectedTo;
			path.Add(currentNode);
			currentNode.isPath = true;
		}
		path.Reverse();
		return path;
	}
	public bool WillBlockPath(Vector2Int coordinates)
	{
		if (grid.ContainsKey(coordinates))
		{
			bool previousState = grid[coordinates].isWalkable;

			grid[coordinates].isWalkable = false;
			List<Node> newPath = GetNewPath();
			grid[coordinates].isWalkable = previousState;

			if (newPath.Count <= 1)
			{
				GetNewPath();
				return true;
			}
		}
		return false;
	}
	public void NotifyReciver()
	{
		BroadcastMessage("RecalculatePath",false ,SendMessageOptions.DontRequireReceiver);
	}
}
