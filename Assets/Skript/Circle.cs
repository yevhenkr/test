using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
	
	[Header( "Table Variables")]
	public int column,row;
	public int targetX, targetY;
	public bool isMatched = false;
	public int maxMove;                      //How many times a player can walk

	public int previouseRow,previouseColumn;

	private EndGame endGame;
	private FindMatches findMatches;
	private GameObject otherCircle;
	private Table table;
	
	private Vector2 firstTouchposition;
	private Vector2 finalTouchposition;
	private Vector2 tempPosition;
	
	public float swipeAngele = 0;
	private float swipeResist = 0.01f;
	
	void Start ()
	{
		table = FindObjectOfType<Table>();
		findMatches = FindObjectOfType<FindMatches>();
		endGame = FindObjectOfType<EndGame>();
//		targetX = (int) transform.position.x; //Position Cirkle start
//		targetY  = (int) transform.position.y;
//		row = targetY;
//		column = targetX;
//		previouseRow = row;
//		previouseColumn = column;

	}
	
	// Update is called once per frame
	void Update ()
	{
		//FindMatches();
		if (isMatched)
		{
			SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
			mySprite.color = new Color(0f,0f,0f, .2f);
		}
		
		targetX = column;//Position Cirkle naw
		targetY =row;

		if (Mathf.Abs(targetX - transform.position.x) > .1)
		{//Move Towards the target
			tempPosition = new Vector2(targetX, transform.position.y);
			transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
			if (table.allCircles[column, row] != this.gameObject){
				table.allCircles[column, row] = this.gameObject;
			}
		findMatches.FindAllMatches();
		}
		else
		{//Directly set the position
			tempPosition = new Vector2(targetX, transform.position.y);
			transform.position = tempPosition;
		}
		if (Mathf.Abs(targetY - transform.position.y) > .1)
		{//Move Towards the target
			tempPosition = new Vector2(transform.position.x, targetY);
			transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
			if (table.allCircles[column, row] != this.gameObject){
				table.allCircles[column, row] = this.gameObject;
			}
		}
		else
		{//Directly set the position
			tempPosition = new Vector2(transform.position.x, targetY);
			transform.position = tempPosition;
		}
		findMatches.FindAllMatches();

	}

	private void OnMouseDown()
	{
		if (table.currentState == GameState.move)
		{
			firstTouchposition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			//Debug.Log(Mathf.Abs(firstTouchposition.x ));
		}
	}

	private void OnMouseUp()
	{
		if (table.currentState == GameState.move)
		{
			finalTouchposition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			CalculateAngel();
		}
	}

	void CalculateAngel()
	{
		if (Mathf.Abs(finalTouchposition.y - firstTouchposition.y) > swipeResist || Mathf.Abs(finalTouchposition.x - firstTouchposition.x) > swipeResist)
		{
			swipeAngele = Mathf.Atan2(finalTouchposition.y - firstTouchposition.y,
				                       finalTouchposition.x - firstTouchposition.x) * 180 / Mathf.PI;
			MovePieces();
			table.currentState = GameState.wait;
		}
		else
		{
			table.currentState = GameState.move;
		}
	}

	void MovePieces()
	{
		if (swipeAngele > -45 && swipeAngele <= 45 && column < table.width -1)
		{//Rigth swipe 
			otherCircle = table.allCircles[column + 1, row];
			previouseRow = row;
			previouseColumn = column;
			otherCircle.GetComponent<Circle>().column -= 1;
			column += 1;
		}
		else if (swipeAngele > 45 && swipeAngele <= 135 && row < table.height -1)
		{//Up swipe 
			otherCircle = table.allCircles[column, row + 1];
		previouseRow = row;
		previouseColumn = column;

			otherCircle.GetComponent<Circle>().row -= 1;
			row += 1;
		}
		else if ((swipeAngele > 135 || swipeAngele <= -135) && column > 0 )
		{//Left swipe 
			otherCircle = table.allCircles[column - 1, row];
			previouseRow = row;
			previouseColumn = column;
			otherCircle.GetComponent<Circle>().column += 1;
			column -= 1;
		}
		else if (swipeAngele < -45 && swipeAngele >= -135 && row > 0)
		{//Down swipe 
			otherCircle = table.allCircles[column, row - 1];
			previouseRow = row;
			previouseColumn = column;
			otherCircle.GetComponent<Circle>().row += 1;
			row -= 1;
		}

		StartCoroutine(CheckMoveCo());

	}

	public IEnumerator CheckMoveCo(){
		yield return  new WaitForSeconds(.5f);
		if (otherCircle != null)
		{
			if (!isMatched && !otherCircle.GetComponent<Circle>().isMatched)
			{
				otherCircle.GetComponent<Circle>().row = row;
				otherCircle.GetComponent<Circle>().column = column;
				row    = previouseRow;
				column = previouseColumn;
				yield return new WaitForSeconds(.5f);
				table.currentState = GameState.move;
			}else
			{
				if (endGame != null)
				{
					endGame.DecreaseCountervalue();
				}
				table.DestroyMatches();
			}

			otherCircle = null;
		}
		
	}
	
	void FindMatches()
	{
		if (column > 0 && column < table.width - 1)
		{
			GameObject leftCircle1 = table.allCircles[column - 1, row];
			GameObject rigthCircle1 = table.allCircles[column + 1, row];
			if (leftCircle1 != null && rigthCircle1 != null)
			{

				if (leftCircle1.tag == this.gameObject.tag && rigthCircle1.tag == this.gameObject.tag)
				{
					leftCircle1.GetComponent<Circle>().isMatched = true;
					rigthCircle1.GetComponent<Circle>().isMatched = true;
					isMatched = true;
				}
			}
		}

		if (row > 0 && row < table.height - 1)
		{
			GameObject upCircle1 = table.allCircles[column, row + 1];
			GameObject downCircle1 = table.allCircles[column, row - 1];
			if (upCircle1 != null && downCircle1 != null)
			{

				if (upCircle1.tag == this.gameObject.tag && downCircle1.tag == this.gameObject.tag)
				{
					upCircle1.GetComponent<Circle>().isMatched = true;
					downCircle1.GetComponent<Circle>().isMatched = true;
					isMatched = true;
				}
			}
		}
	}
}
