using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType
{
	Moves
}

[System.Serializable]
public class EndGameRequirmentts
{
	public GameType gameType;
	public int counterValue;
}


public class EndGame : MonoBehaviour
{
	public GameObject losePanel;
	
	public GameObject amountMove;
	public Text moveText;
	public EndGameRequirmentts requirmentts;
	public int currentCoutlerValue;
	
	
	
	void Start () {
		SetupGame();
	}

	void SetupGame()
	{
		currentCoutlerValue = requirmentts.counterValue;
		if (requirmentts.gameType == GameType.Moves)
		{
			moveText.text = "" + currentCoutlerValue;
		}
		
	}

	public void DecreaseCountervalue()
	{
		
		currentCoutlerValue--;
		moveText.text = "" + currentCoutlerValue;
		if (currentCoutlerValue <= 0)
		{
			Debug.Log("You Lose");
			currentCoutlerValue = 0;
			moveText.text = "" + currentCoutlerValue;
			losePanel.SetActive(true);
		}
		else
		{
			
		}

		// Update is called once per frame
	}
}
