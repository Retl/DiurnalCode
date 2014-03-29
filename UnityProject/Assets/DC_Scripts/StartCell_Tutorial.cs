using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StartCell_Tutorial : Cell
{
	
	//-----Property instantiations and whatnot.-----
	//private Vector3 position = rigidbody.position;
	
	public GameObject stone;

	/*
	private Texture mySprite;
	private List<Token> contents;
	private string myName;
	private Action useAction;
	private string useActionName;
	*/
	
	
	
	//public static event Action<Cell> Touched;
	
	
	//-----End of Properties-----
	
	
	
	//-----Instatiation of Lister Event Thingies-----
	
	//-----End of Listeners-----
	
	
	
	//public static event Action<Token> MakeNewTurns; //This is a function we're going to call.
	
	
	//-----Constructors-----
	
	//-----End of Constructors-----
	
	
	//-----Accessors-----
	/*
	public string toString()
	{
		return nickname + " ACT: " + act.ToString();
	}
	
	public int getAct()
	{
		return act;
	}
	
	public string getName()
	{
		return nickname;
	}
	*/
	
	//-----End of Accessors-----
	
	
	
	//-----Mutators-----
	//-----End of Mutators-----
	
	//-----End of Event Maker Thingies-----
	
	//-----Event Handlers-----
	
	// Use this for initialization
	void Start () 
	{
		//Relocate the contents into a position within our area.
		// It's possible multiple tokens will be in once cell, so we would want to space those out somehow for easier player selection.

		//Make the other cells for the level.

        GameObject tempPrev = null; //This is necessary for the following loop to function.

		for (int i = 1; i<3; i++) //We start at 1 instead of 0, because 0 refers to THIS start cell, which already exists.
		{
			//GameObject sandstoneCell = Resources.Load("DC_Prefabs/Cells/SandstoneCell");

			GameObject temp = (GameObject)Instantiate(stone, new Vector3(transform.position.x + (i * 2.0f), transform.position.y, transform.position.z), transform.rotation);

			if (i == 1)
			{
				setNext(temp.GetComponent<Cell>()); //Set my next to the one we just created.
				next.setPrev(GetComponent<Cell>()); //Set the prev of the new token to the first cell.


			}
			else //Not the first new cell.
			{
                if (tempPrev != null)
                {
                    tempPrev.GetComponent<Cell>().setNext(temp.GetComponent<Cell>()); //Set the previous cell's new to the current cell. 
                    temp.GetComponent<Cell>().setPrev(tempPrev.GetComponent<Cell>()); //Set the newesst cell's previous to the previous cell.
                }

			}

            tempPrev = temp;
		}
		
		//START of TokenPlacement - Adjust all of the tokens to their proper places.
		
		//TokenPlacement: Tokens that start on this cell:
		foreach (Token currentToken in contents)
		{
			currentToken.jumpToPosition(transform.position);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown()
	{
		//Yay, you clicked one!
		if (GameControllerScript.DEBUGGING)
		{
			this.WasTouched();
		}
	}
	
	//-----End of Event Handlers-----
}
