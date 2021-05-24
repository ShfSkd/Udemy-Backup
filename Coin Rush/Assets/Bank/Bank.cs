using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
	[SerializeField] int stratingBalance = 150;
	int currentBalance = 150;
	[SerializeField] TextMeshProUGUI displayBalance;
	public int CurrentBalance { get { return currentBalance; } }

	private void Awake()
	{
		currentBalance = stratingBalance;
		UpdateDisplay();
	}
	public void Deposit(int amount)
	{
		currentBalance += Mathf.Abs(amount) ;
		UpdateDisplay();
	}
	public void Withdraw(int amount)
	{
		currentBalance -= Mathf.Abs(amount);
		UpdateDisplay();

		if (currentBalance < 0)
		{
			// lose the game;
			ReloadScene();
		}
	}
	void UpdateDisplay()
	{
		displayBalance.text = "Gold: " + currentBalance;
	}
	void ReloadScene()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex);
	}
}
