using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnList
{
	private List<Turn> turns = new List<Turn>();
	
	//START OF CONSTRUCTORS
	public TurnList()
	{
		Token.MakeNewTurns += AddTurnsOfToken;
		Token.RemoveTokenTurns += RemoveTurnsOfToken;
		//This constructor only makes a turn list, but doesn't actually put anything in it. It's required, since we specified other constructors.
	}
	
	public TurnList(Token theToken)
	{
		int period = GameControllerScript.ENDTIME / theToken.getAct();
		int nextTime = period;
		for (int loopTime = GameControllerScript.time; nextTime <= GameControllerScript.ENDTIME;) //Note: This doesn't check for bottom bound. We'd need to retweak it later.
		{
			Turn tempTurn = new Turn(theToken, nextTime);
			this.AddTurn(tempTurn);
			nextTime += period;
		}
		
		//Initializing the listener.
		Token.MakeNewTurns += AddTurnsOfToken; //Each TurnList that gets made will listen out for the thingy. 
		//Though this is supposed to be a Singleton. Should add preventative measures to prevent making more than one.
	}
	//-----END OF CONSTRUCTORS-----
	
	//-----START OF MUTATOR METHODS-----
	public void AddTurn(Turn newTurn)
	{
		int i = 0;
		for (; i < turns.Count && newTurn.GetTime() < turns[i].GetTime(); i++)
		{
			//Most of the logic of incrementation and whatnot is done by the for above. 
		}
		
		turns.Insert(i, newTurn);
	}
	
	public void AddTurnsOfToken(Token theToken)
	{
		
		/* DELETE THIS BIG BLOCK COMMENT
		this.AddTurn(nickname = theToken.getName()); 
		//This version of a term gives an incorrect turn, but will work for now.
		float period = GameControllerScript.ENDTIME / myToken.getAct(); //How long it takes for this to get one action. 1000/1= 1000. Which would get it exactly one turn at the end of the day. 
		//1000/10 = 100. Fill the time bar with this and they would get 10 turns.
		
		time = (int)period; //In this case, we're just assuming the earliest possible turn the ACT can give is the only one we're going to make use of.
		END OF DELETEME*/
		
		int period = GameControllerScript.ENDTIME / theToken.getAct();
		int nextTime = period;
		for (int loopTime = GameControllerScript.time; nextTime <= GameControllerScript.ENDTIME;) //Note: This doesn't check for bottom bound. We'd need to retweak it later.
		{
			Turn tempTurn = new Turn(theToken, nextTime);
			this.AddTurn(tempTurn);
			nextTime += period;
		}
		
	}

	public void RemoveTurnsOfToken(Token theToken)
    {
        for (int index = 0; index < turns.Count; index++) //Note: This doesn't check for bottom bound. We'd need to retweak it later.
        {
            if (turns[index].isFromToken(theToken))
            {
                turns.RemoveAt(index);
                index--; //Because once we delete it, the order of index thingies will shift. We make sure to set the index back as the last action after deleting the turn.
            }
        }
        
    }

    /*
    public void PerformNextTurnOfToken(Token theToken)
    {
        for (int index = 0; index < turns.Count; index++) //Note: This doesn't check for bottom bound. We'd need to retweak it later.
        {
            if (turns[index].isFromToken(theToken))
            {
                GameControllerScript.time = turns[index].GetTime;
                turns.RemoveAt(index);
                theToken.PerformTurnAction();

                index = turns.Count; //Immediately exit the loop.
            }
        }
        
    }
    */

    public Token PerformNextTurn()
    {
        Token theToken = null;
        if (turns.Count > 0)
        {

            int index = turns.Count - 1;
            GameControllerScript.time = turns[index].GetTime();
            theToken = turns[index].getMyToken();

            turns.RemoveAt(index);
            theToken.PerformTurnAction();

            return theToken;
        }

        return theToken;
        
    }

    //Here's an accessor:
    public bool isEmpty()
    {
        bool result = true;
        if (turns.Count > 0)
        {
            result = false;
        }
        return result; //Surviving all of the turns in a level should be victory condition.
    }
	
	
	//-----END OF MUTATOR METHODS-----
	
	//-----EVENT MESSAGE METHODS-----
	
	
	
	//-----END OF EVENT MESSAGE METHODS-----
	
	//-----OVERRIDES-----
	
	override public string ToString()
	{
		//Returns all of the turns stored within as a string.
		string result = "";
		for (int i = this.turns.Count - 1; i >= 0 ; i--)
		{
			result += turns[i].ToString();
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
