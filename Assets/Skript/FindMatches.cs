using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{


	private Table table;
	public List<GameObject> currentMathes = new List<GameObject>();
	void Start ()
	{
		table = FindObjectOfType<Table>();
	}

	public void FindAllMatches()
	{
		StartCoroutine(FindAllMathesCo());
	}
	
	
	private IEnumerator FindAllMathesCo()
	{
		yield return new WaitForSeconds(.2f);
		for (int i = 0; i < table.width; i++)
		{
			for (int j = 0; j < table.height; j ++)
			{
              GameObject currentCircle = table.allCircles[i , j];
				if (currentCircle != null)
				{
					if (i > 0 && i < table.width - 1)
					{
						GameObject leftCircle = table.allCircles[i - 1, j];
						GameObject rigthCircle = table.allCircles[i + 1, j];
						if (leftCircle != null && rigthCircle != null)
						{
							if (leftCircle.tag == currentCircle.tag && rigthCircle.tag == currentCircle.tag)
							{
								if (!currentMathes.Contains(leftCircle))
								{
									currentMathes.Add(leftCircle);
								}
								leftCircle.GetComponent<Circle>().isMatched = true;
								if (!currentMathes.Contains(rigthCircle))
								{
									currentMathes.Add(rigthCircle);
								}
								rigthCircle.GetComponent<Circle>().isMatched = true;
								if (!currentMathes.Contains(currentCircle))
								{
									currentMathes.Add(currentCircle);
								}
								currentCircle.GetComponent<Circle>().isMatched = true;
							}
						}
					}
					if (j > 0 && j < table.height - 1)
					{
						GameObject upCircle = table.allCircles[i , j+1];
						GameObject downCircle = table.allCircles[i, j -1 ];
						if (upCircle != null && downCircle != null)
						{
							if (upCircle.tag == currentCircle.tag && downCircle.tag == currentCircle.tag)
							{
								if (!currentMathes.Contains(upCircle))
								{
									currentMathes.Add(upCircle);
								}
								upCircle.GetComponent<Circle>().isMatched = true;
								if (!currentMathes.Contains(downCircle))
								{
									currentMathes.Add(downCircle);
								}
								downCircle.GetComponent<Circle>().isMatched = true;
								if (!currentMathes.Contains(currentCircle))
								{
									currentMathes.Add(currentCircle);
								}
								currentCircle.GetComponent<Circle>().isMatched = true;
							}
						}
					}
				}

			}
		}
	}

}
