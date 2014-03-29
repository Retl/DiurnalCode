using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionLog
{
	private List<string> strings = new List<string>();

	//Instatiation of Listener Event Thingies we want..er...act.

	
	//START OF CONSTRUCTORS
	public ActionLog()
	{

		//Token.MakeNewTurns += AddTurnsOfToken; //LISTENER STUFF copied from TurnList. WE might use this later.
		//This constructor only makes a turn list, but doesn't actually put anything in it. It's required, since we specified other constructors.
		strings = new List<string>();

		//Initializing the listener.
		GameControllerScript.addToLog += addToLog;

		if (GameControllerScript.DEBUGGING) 
		{
			addToLog("RUNNING IN DEBUG MODE.");	
		}
	}
		
		
		//Token.MakeNewTurns += AddTurnsOfToken; //Each TurnList that gets made will listen out for the thingy. -- MIGHT NEED THIS LATER
		//Though this is supposed to be a Singleton. Should add preventative measures to prevent making more than one.

	//-----END OF CONSTRUCTORS-----
	
	//-----START OF MUTATOR METHODS-----
	
	//This lets us feed a string to the ActionLog which will be drawn on screen pretty much all the time.
	//It'll help us get rid of a lot of icky print statemenets as well as letting the player see what NPCs are doing on turns.
	// TODO: Maybe we should move this actionlog stuff into its own class, just like the Turn List? It'd be a little less messy, at least.
	public void addToLog(string newStringForLog)
	{
		strings.Add(newStringForLog + "\n");
	}
	
	
	//-----END OF MUTATOR METHODS-----
	
	//-----EVENT MESSAGE METHODS-----
	
	
	
	//-----END OF EVENT MESSAGE METHODS-----
	
	//-----OVERRIDES-----
	
	override public string ToString()
	{
		//Returns all of the turns stored within as a string.
		string result = "";
		for (int i = this.strings.Count - 1; i >= 0 ; i--)
		{
			result += strings[i].ToString();
		}
		/*
		 * foreach(Turn theTurn in turns)
		{
			result += theTurn.ToString();
		}
		*/
		return result;
	}
	
}
