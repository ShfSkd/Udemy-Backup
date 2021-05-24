using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
	[SerializeField] Transform weapon;
	[SerializeField] ParticleSystem projectilePartical;
	[SerializeField] float towerRange = 7f;
	Transform target;

	
	private void Update()
	{
		FindClosestTarget();

		if (target != null)
			AimWeapon();
	}

	private void FindClosestTarget()
	{
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		Transform closesetTarget = null;
		float maxDistance = Mathf.Infinity;

		foreach (Enemy enemy in enemies)
		{
			float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

			if (targetDistance < maxDistance)
			{
				closesetTarget = enemy.transform;
				maxDistance = targetDistance;
			}
		}
		target = closesetTarget;
	}

	void AimWeapon()
	{
		float targetDistance = Vector3.Distance(transform.position, target.position);

		weapon.LookAt(target);
		if (targetDistance < towerRange)
		{
			Attack(true);
		}
		else
		{
			Attack(false);
		}
	}
	void Attack(bool isActive)
	{
		var emissonModule = projectilePartical.emission;

		emissonModule.enabled = isActive;
	}
}
