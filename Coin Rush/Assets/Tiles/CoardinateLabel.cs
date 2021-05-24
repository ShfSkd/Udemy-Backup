using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoardinateLabel : MonoBehaviour
{
	[SerializeField] Color defaultColor = Color.white;
	[SerializeField] Color blockedColor = Color.gray;

	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();

	Waypoint waypoint;

	private void Awake()
	{
		label = GetComponent<TextMeshPro>();
		label.enabled = false;
		waypoint = GetComponentInParent<Waypoint>();
		DispalyCoordinets();
	}
	private void Update()
	{
		if (!Application.isPlaying)
		{
			DispalyCoordinets();
			UpdateObjectName();
		}
		SetLabelColor();
		ToggleLabels();

	}
	void ToggleLabels()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			label.enabled = !label.IsActive();
		}
	}
	private void SetLabelColor()
	{
		if (waypoint.IsPlacebale)
		{
			label.color = defaultColor;
		}
		else
		{
			label.color = blockedColor;
		}
	}

	private void DispalyCoordinets()
	{
		coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
		coordinates.y = Mathf.RoundToInt(transform.parent.position.z);
		label.text = coordinates.x + "," + coordinates.y;
	}
	void UpdateObjectName()
	{
		transform.parent.name = coordinates.ToString();
	}
}
