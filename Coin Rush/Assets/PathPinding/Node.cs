using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node 
{
    public Vector2Int cordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int cordinates, bool isWalkable)
	{
        this.cordinates =cordinates;
        this.isWalkable = isWalkable;
	}

}
