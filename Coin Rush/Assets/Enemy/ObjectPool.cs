using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] [Range(0, 50)] int poolSize = 5;
	[SerializeField] [Range(0.1f,30f)] float spawnTimer = 1;

	GameObject[] pool;

	private void Awake()
	{
		PopulatePool();
	}

	private void Start()
	{
		
		StartCoroutine(SpawnEnemy());
	}
	private void PopulatePool()
	{
		pool = new GameObject[poolSize];

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i]= Instantiate(enemyPrefab, transform);
			pool[i].SetActive(false);
		}
	}

	void EnableObjectPool()
	{
		for (int i = 0; i < pool.Length; i++)
		{
			if (!pool[i].activeInHierarchy)
			{
				pool[i].SetActive(true);
				return;
			}
		}
	}
	private IEnumerator SpawnEnemy()
	{
		while (true)
		{
			EnableObjectPool();
			yield return new WaitForSeconds(spawnTimer);
		}
	}
}
