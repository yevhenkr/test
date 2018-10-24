using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	wait,
	move
}

public class Table : MonoBehaviour
{
	
	public GameState currentState = GameState.move;
	
	public int width;
	public int height;
	public int offSet;
	public GameObject tilePrefab; //Tile from table
	private BackgroundImage[,] allImages;
	public GameObject[] circles;
	public GameObject[,] allCircles;
	private FindMatches findMatches;
	public int score;
	void Start()
	{
		findMatches = FindObjectOfType<FindMatches>();
		allImages = new BackgroundImage[width, height]; //count Tile from table
		allCircles = new GameObject[width, height]; //count Circles from table
		
		height = PlayerPrefs.GetInt("heigthTable");
		width = PlayerPrefs.GetInt("widthTable");
		СreateTable();
		
	}

	private void СreateTable()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Vector2 position = new Vector2(i, j+offSet*10); // todo 
				GameObject backgroundImage = Instantiate(tilePrefab, position, Quaternion.identity) as GameObject;
				backgroundImage.transform.parent = this.transform;
				backgroundImage.name = "( " + i + ", " + j + " )";
				int circleUse = Random.Range(0, circles.Length);
				int maxIteratons = 0;

				while (MatchestAt(i, j, circles[circleUse]) && maxIteratons < 100)
				{
					circleUse = Random.Range(0, circles.Length);
					maxIteratons++;
				}

				maxIteratons = 0;
				GameObject circle = Instantiate(circles[circleUse], position, Quaternion.identity);
				circle.GetComponent<Circle>().row = j;
				circle.GetComponent<Circle>().column = i;
				circle.transform.parent = this.transform;
				circle.name = "( " + i + ", " + j + " )";
				allCircles[i, j] = circle;
			}
		}
	}

	private bool MatchestAt(int column, int row, GameObject piece)
	{

		if (column > 1 && row > 1)
		{
			if (allCircles[column - 1, row].tag == piece.tag && allCircles[column - 2, row].tag == piece.tag)
			{
				return true;
			}

			if (allCircles[column, row - 1].tag == piece.tag && allCircles[column, row - 2].tag == piece.tag)
			{
				return true;
			}
		}
		else if (column <= 1 || row <= 1)
		{
			if (row > 1)
			{
				if (allCircles[column, row - 1].tag == piece.tag && allCircles[column, row - 2].tag == piece.tag)
				{
					return true;
				}
			}

			if (column > 1)
			{
				if (allCircles[column - 1, row].tag == piece.tag && allCircles[column - 2, row].tag == piece.tag)
				{
					return true;
				}
			}
		}

		return false;
		}


	private void DestroyMatchesAt(int column, int row)
	{
		if (allCircles[column, row].GetComponent<Circle>().isMatched)
		{
			findMatches.currentMathes.Remove(allCircles[column,row]);
			Destroy(allCircles[column,row]);
			allCircles[column, row] = null;
			score ++;
		}
	}

	public void DestroyMatches()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (allCircles[i, j] != null)
				{
					DestroyMatchesAt(i,j);
				}
			}
		}

		StartCoroutine(DecreaseRowCo());
	}

	private IEnumerator DecreaseRowCo()
	{
		int nullCount = 0;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (allCircles[i, j] == null)
				{
					nullCount++;
				}
				else if (nullCount > 0)
				{
					allCircles[i, j].GetComponent<Circle>().row -= nullCount;
					allCircles[i, j] = null;
				}
			}

			nullCount = 0;
		}

		yield return  new WaitForSeconds( .4f);
		StartCoroutine(FillTableCo());
	}

	private void RefillTable()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (allCircles[i, j] == null)
				{
					Vector2 position = new Vector2(i,j + offSet);
					int circlesUse = Random.Range(0, circles.Length);
					GameObject piece = Instantiate(circles[circlesUse], position, Quaternion.identity);
					allCircles[i, j] = piece;
					piece.GetComponent<Circle>().row = j;
					piece.GetComponent<Circle>().column = i;
				}
			}
		}
	}

	private bool MathesOnTable()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (allCircles[i, j] != null)
				{
					if (allCircles[i, j].GetComponent<Circle>().isMatched)
					{
						return true;
						
					}
				}
			}
		}
		return false; 
	}

	private IEnumerator FillTableCo()
	{
		RefillTable();
		yield return new WaitForSeconds(.5f);

		while (MathesOnTable())
		{
			yield return new WaitForSeconds(.5f);
			DestroyMatches();
		}
		yield return  new WaitForSeconds(.5f);
		currentState = GameState.move;
	}
	
}
	