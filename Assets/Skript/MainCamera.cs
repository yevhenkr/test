using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

	public GameObject table;
	public float posW,posH,sizeWidth,sizeHeith;
    
	void Start()
	{
		posW = table.GetComponent<Table>().width;
		posH = table.GetComponent<Table>().height;
		sizeWidth = table.GetComponent<Table>().width;
		sizeHeith = table.GetComponent<Table>().height;

	}

	void Update()
	{
		transform.position = new Vector3(posW/2-1, posH/2, -10);

		if (sizeHeith >= sizeWidth)
		{
			GetComponent<Camera>().orthographicSize = sizeHeith;
		}

		if (sizeHeith < sizeWidth)
		{
			GetComponent<Camera>().orthographicSize = sizeWidth;
		}
		


	}
}