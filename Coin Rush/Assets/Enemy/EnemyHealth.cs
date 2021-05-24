using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
	[Tooltip("Adds amount to maxHitPoints when enemy dies.")]
	[SerializeField] int difficultyRamp = 1;
    int currentHitPoint;

	Enemy enemy;

	private void OnEnable()
	{
		currentHitPoint = maxHitPoints;
	}
	private void Start()
	{
		enemy = GetComponent<Enemy>();
	}
	private void OnParticleCollision(GameObject other)
	{
		ProccesHit();
	}

	private void ProccesHit()
	{
		currentHitPoint--;

		if (currentHitPoint <= 0)
		{
			gameObject.SetActive(false);
			maxHitPoints += difficultyRamp;
			enemy.RewardGold();
		}
	}
}
