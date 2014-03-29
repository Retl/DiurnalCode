using UnityEngine;
using System.Collections;

public class SimonGameController : MonoBehaviour {
	
	//Property declarations.
	ArrayList simonList;
	ArrayList playerList;
	ArrayList buttons;
	
	string a="";
	string b="";
	
	const int numberOfButtons = 4;
	
	bool mouseReleased = true;
	bool noButton = true;
	
	// Use this for initialization
	void Start () 
	{
		setupSimon();
		/*
		for(int i = 0; i<numberOfButtons; i++)
		{
			
		}
		*/
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	// And this handles drawing the GUI.
	void OnGUI()
	{
		GUI.Label (new Rect (120, 10, 100, 50), "Simon: " + arrayListIntegerString(simonList));
		GUI.Label (new Rect (120, 110, 100, 50), "Player: " + arrayListIntegerString(playerList));
		GUI.Label (new Rect (120, 210, 100, 50), a + " | " + b);
		
		if (GUI.Button(new Rect (10, 10, 100, 100), "1"))
		{
			if (mouseReleased)
			{
				mouseReleased = false;
				playerList.Add(1);
				checkInputAgainstGame();
			}
		}
		
		else if (GUI.Button(new Rect (10, 110, 100, 100), "2"))
		{
			if (mouseReleased)
			{
				mouseReleased = false;
				playerList.Add(2);
				checkInputAgainstGame();
			}
		}
		
		else if (GUI.Button(new Rect (10, 210, 100, 100), "3"))
		{
			if (mouseReleased)
			{
				mouseReleased = false;
				playerList.Add(3);
				checkInputAgainstGame();
			}
		}
		
		else if (GUI.Button(new Rect (10, 310, 100, 100), "4"))
		{
			if (mouseReleased)
			{
				mouseReleased = false;
				playerList.Add(4);
				checkInputAgainstGame();
			}
		}
		else {mouseReleased = true;}
		
		
		/*
		int which = 0; 
		foreach (GUI.Button currentButton in buttons)
		{
			which++;
			if (currentButton)
			{
				if (mouseReleased)
				{
					mouseReleased = false;
					playerList.Add(which);
					
					
					checkInputAgainstGame();
						
				}
			}
		}
		
		*/
	}
	
	void checkInputAgainstGame()
	{
		//Check whole.
		if (simonList.Count == playerList.Count)
		{
			bool lose = false;
			for(int i = 0; i<playerList.Count; i++)
			{
				if (playerList[i].ToString() != simonList[i].ToString())
				{
					a = playerList[i].ToString(); //DELME
					b = simonList[i].ToString(); //DELME
					
					lose = true;
					setupSimon();
				}
			}
			
			if (!lose)
			{
				increaseDifficulty();
			}
		}
		
		else
		{
			//Check partial.
			for(int i = 0; i<playerList.Count; i++)
			{
				if (playerList[i].ToString() != simonList[i].ToString())
				{
					a = playerList[i].ToString(); //DELME
					b = simonList[i].ToString(); //DELME
					
					setupSimon();
				}
			}
		}
		
		//Fail.
		
	}
	
	void increaseDifficulty()
	{
		int newInt = Random.Range(1, numberOfButtons);
		simonList.Add(newInt);
		playerList = new ArrayList();
	}
	
	void setupSimon()
	{
		int startInt = Random.Range(1, numberOfButtons);
		simonList = new ArrayList();
		simonList.Add(startInt);
		playerList = new ArrayList();
		mouseReleased = true;
		noButton = true;	
	}
	
	string arrayListIntegerString(ArrayList theList)
	{
		string result = "";
		foreach(int current in theList)
		{
			result += current.ToString();
		}
		return result;
	}
}
