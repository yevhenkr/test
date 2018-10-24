using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManadger : MonoBehaviour
{
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	public void GoToGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void GoToMenu()
	{
		SceneManager.LoadScene("Main menu");
	}


	public void Complexity1()
	{
		PlayerPrefs.SetInt("heigthTable", 3);
		PlayerPrefs.SetInt("widthTable", 3);
		PlayerPrefs.Save();
	}
	public void Complexity2()
	{
		PlayerPrefs.SetInt("heigthTable", 10);
		PlayerPrefs.SetInt("widthTable", 10);
		PlayerPrefs.Save();
	}
	public void Complexity3()
	{
		PlayerPrefs.SetInt("heigthTable", 15);
		PlayerPrefs.SetInt("widthTable", 15);
		PlayerPrefs.Save();
	}
}
