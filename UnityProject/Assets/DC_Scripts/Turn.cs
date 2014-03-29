using UnityEngine;
using System.Collections;

public class Turn 
{
	private string nickname;
	private Token myToken;
	private int time;
	
	public Turn()
	{
		nickname = "Unnamed!?";
		time = -1;
		myToken = new Token();
		
	}
	
	public Turn(Token theToken): this(theToken, 0) //EDITME TO BE LESS REDUNDANT!
	{
		//DON'T DELETE THIS.
		
		
		int period = GameControllerScript.ENDTIME / myToken.getAct();
		time = period; //This calls the constructor below, but immediately overrides the time that constructor sets with a new one.
		
		//How long it takes for this to get one action. 1000/1= 1000. Which would get it exactly one turn at the end of the day. 
		//1000/10 = 100. Fill the time bar with this and they would get 10 turns.
		
		//In this case, we're just assuming the earliest possible turn the ACT can give is the only one we're going to make use of.
	}
	
	public Turn(Token theToken, int newTime)
	{
		//
		
		myToken = theToken;
		nickname = theToken.getName(); 
		time = newTime; //In this case, we're just assuming the earliest possible turn the ACT can give is the only one we're going to make use of.
	}
	
	//Accessor Method
	public int GetTime()
	{
		
		return this.time;
	}

    public Token getMyToken()
    {
        return myToken;
    }
	
	override public string ToString()
	{
		//return this.time + ": " + this.nickname + "\n";
		// It might be worth deleting the above older version of this command, as this new version doesn't break everything into its own line.

		return this.time + ": " + this.nickname + " * ";
	}

	public bool isFromToken(Token tokenToCheckAgainst) //Checks to see if the token being passed in matches to the token we stored when we were made. Returns bool.
	{
		bool result = false;

		if (myToken == tokenToCheckAgainst) 
		{
			result = true;
		}

		return result;
	}

}
