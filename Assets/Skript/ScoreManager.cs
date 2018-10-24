using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	private Text scoreText;
	private int score;
	private int gameWin = 7;

	public GameObject canvasVictory;
	void Start ()
	{
		scoreText = GetComponent<Text>();
		Win();
	}
	// Update is called once per frame
	void Update () {
		score = FindObjectOfType<Table>().score;
		Win();
		scoreText.text = score.ToString();
	}

	private bool Win()
	{
		if (gameWin <= score)
		{
			canvasVictory.SetActive(true);
			return true;
		}
		else
		{
			return false;
		}
	}
}
