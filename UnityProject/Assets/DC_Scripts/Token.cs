using UnityEngine;
using System;
using System.Collections;

public class Token : MonoBehaviour
{
    //Property instantiations and whatnot.
    public int level = 1;
    public int exp = 0;

    //Base stats that never change for this archetype. They're added first.
    //Levelups are bonuses on top of this initial base.
    public int baseMaxHP = 20;
    public int basePotency = 10;
    public int baseDefense = 5;
    public int baseAct = 3;

    //Setting level-up modifers. Based on Divinity archetype.
    public int hpPerLevel = 1;
    public int levelsPerActUp = 5;
    public int ActsPerActUp = 1;
    public int levelsPerPotUp = 2;
    public int potencyPerPotUp = 1;
    public int levelsPerDefUp = 4;
    public int defensePerDefUp = 1;
    public string nickname /*{get; set;}*/ = "UNNAMED TOKEN"; //Fix this later, if we have the time.
    public int maxhp = 50;
    public int hp = 50;
    public int res = 50;
    public int maxres = 50;
    public int potency = 10;
    public int defense = 5;
    public int act = 1;
    public Cell currentCell;

    //Properties used for randomly naming tokens.
    public static string[] possibleDivinityNames = new string[] {
                "Quicksand",
                "Sandstone",
                "Diablo",
                "Gatomon",
                "Hydro",
                "Catlyn",
                "Raelyn",
                "Voltair",
                "Skyheat",
                "Surprise",
                "Strawberry",
                "Amberly",
                "Diane",
                "Bloodruby",
                "Honeysuckle",
                "Stormdancer",
                "Starstorm",
                "Rifle",
                "Pausen",
                "Subject 01",
                "Reveille-Dux",
                "Ruby Eye",
                "Blueblood",
                "Fragments",
                "Mentals",
                "Quala",
                "Revya",
                "Gig"
        };
    
    
    
    //Instatiation of Listener Event Thingies we want..er...act.
    
    public static event System.Action<Token> MakeNewTurns; //This is a function we're going to call. vvv
    public static event System.Action<Token> RemoveTokenTurns; //This is a function we're going to call. vvv
    public static event System.Action<Token> Touched; //This is a function we're going to call. >>> Remember: These does things IN ANOTHER CLASS.
    //public static event System.Action<Token> IAmThePlayerCharacter; //Ditto.
    //public static event System.Action DedPlayerCharacter; //Ditto.
    
    
    
    //Constructors
    public Token(): this("UNNAMED TOKEN")
    {
        /*
        //Token("UNNAMED TOKEN");
        nickname = "UNNAMED TOKEN";
        act = 1;
        */
    }
    
    public Token(string newName): this(newName, 1)
    {
        /*
        //Token(newName, 1);
        nickname = newName;
        act = 1;
        */
    }
    
    public Token(string newName, int newAct)
    {
        nickname = newName;
        act = newAct;
        this.MakeTurns();
    }
    
    //THESE ARE MY EVENT THINGIES. THEY MAKE EVENTS HAPPEN FOR LISTENERS TO LISTEN TO. Or something.

    public void MakeTurns()
    {
        if (MakeNewTurns != null)
        {
            if (!this.nickname.Equals("UNNAMED TOKEN"))
            {
                MakeNewTurns(this); //If MakeNewTurns has been defined (in TurnList), then when this is called, use the given (in TurnList) method.
            }
        }
    }

    public void RemoveTurns()
    {
        if (RemoveTokenTurns != null)
        {
            if (!this.nickname.Equals("UNNAMED TOKEN"))
            {
                RemoveTokenTurns(this); //If RemoveTokenTurns has been defined (in TurnList), then when this is called, use the given (in TurnList) method.
            }
        }
    }
    
    public void WasTouched()
    {
        if (GameControllerScript.DEBUGGING)
        {
            GameControllerScript.log(this.ToString());
        }
        
        if (Touched != null)
        {
            Touched(this);
        }
    }

    /*
    public void MarkAsPlayer()
    {
        if (IAmThePlayerCharacter != null)
        {
            IAmThePlayerCharacter(this);
        }
    }

    public void PlayerDied()
    {
        if (DedPlayerCharacter != null)
        {
            DedPlayerCharacter();
        }
    }

*/
    
    // Use this for initialization
    
    public static Token Create()
    {
        return Token.Create("NO NAME GIVEN");
    }
    
    public static Token Create(string newName)
    {
        return Token.Create(newName, 1);
    }
    
    public static Token Create(string newName, int newAct)
    {
        GameObject newObject = Instantiate(Resources.Load("DC_Prefabs/Tokens/PinkBunny")) as GameObject;
        Token newToken = newObject.GetComponent<Token>();
        
        newToken.nickname = newName;
        newToken.act = newAct;
        
        return newToken;
    }
    
    void Reset()
    {
        int level = 1;
        int exp = 0;
        
        //Base stats that never change for this archetype. They're added first.
        //Levelups are bonuses on top of this initial base.
        int baseMaxHP = 20;
        int basePotency = 10;
        int baseDefense = 5;
        int baseAct = 3;
        
        //Setting level-up modifers. Based on Divinity archetype.
        int hpPerLevel = 1;
        int levelsPerActUp = 5;
        int ActsPerActUp = 1;
        int levelsPerPotUp = 2;
        int potencyPerPotUp = 1;
        int levelsPerDefUp = 4;
        int defensePerDefUp = 1;
        string nickname /*{get; set;}*/ = "???"; //Fix this later, if we have the time.
        int maxhp = 50;
        int hp = 50;
        int res = 50;
        int maxres = 50;
        int potency = 10;
        int defense = 5;
        int act = 1;
    }

    void Start()
    {

        if (this.nickname.Equals("???"))
        {
            int randomNumber = UnityEngine.Random.Range(0, possibleDivinityNames.Length);
            this.nickname = possibleDivinityNames [randomNumber];
        }

        if (!this.nickname.Equals("UNNAMED TOKEN"))
        {
            setLevel(level);
            this.MakeTurns();
            
            GameControllerScript.flow -= maxhp;//We should probably be using delegates for this somehow.
        }


        if (this.nickname.Equals("Kilo"))
        {
            GameControllerScript.playerCharacter = this;
        }
    
    }
    
    // Update is called once per frame
    void Update()
    {           
    
    }
    
    void OnMouseDown()
    {
        //Yay, you clicked one!
        if (GameControllerScript.DEBUGGING)
        {
            this.WasTouched();
        }
    }
    
    
    
    //Accessors
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
    
    //Mutators

    public void setCurrentCell(Cell theCell)
    {
        currentCell = theCell;
    }

    public string takeDamage(int damage) //Had a problem where I couldn't overload this with a string-returning version. So instead, this is now the string version.
    {
        string result = "fatal";
        if (damage <= defense)
        { //Minimum damage is always 1, regardless of defense.
            damage = 1;
        }
        
        hp -= damage;
        result = damage.ToString();
        
        if (hp < 1)
        {
            hp = 0;
            defeated();
        }

        return result;
    }
    
    public void takeRecovery(int recovery)
    {
        hp += recovery;
        
        if (hp > maxhp)
        {
            hp = maxhp;
        }
    }

    public void setLevel(int newLevel)
    {
        // The stats in this version are based on DIVINITY class. If we add subclasses, update these values.
        level = newLevel;

        GameControllerScript.flow += maxhp;//Make sure to RESTORE Res to The Flow before we update stats. We can take it back out after. | We should probably be using delegates for this somehow.

        maxhp = baseMaxHP + (level / hpPerLevel); //NOTE: There should probably be info here such that the HP is only restored to max if there's enough The Flow to allow it, otherwise only fit what's left over.

        if (GameControllerScript.flow >= maxhp)
        {
            hp = maxhp;
            GameControllerScript.flow -= maxhp; //And now we readjust the flow to account for the changed status. | We should probably be using delegates for this somehow.
    
        } else
        {
            hp = GameControllerScript.flow;
            //This is HP, not RES.  While MaxHP takes from The Flow, currentHP is not dependant on it.

                
        }



                

        maxres = maxhp;
        res = maxres;

        //MEMO: This is integer division, so the remainder is discarded. 
        potency = basePotency + (level / levelsPerPotUp) * potencyPerPotUp;
        defense = baseDefense + (level / levelsPerDefUp) * defensePerDefUp;
        act = baseAct + (level / levelsPerActUp) * ActsPerActUp;



    }

    public void levelUp()
    {
        setLevel(level + 1);
        GameControllerScript.log(getName() + " felt a spark of understanding. (Level Up! Level: " + level + ")");

    }

    //Destroying stuff.
    public void defeated()
    {
        //Announce what is happening. 
        GameControllerScript.log(GameControllerScript.time.ToString() + ": " + getName() + " has been defeated!");

		//Return the Token's MaxHP back to the flow.
		GameControllerScript.flow += maxhp;

        //And then do anything else left over necessary for the Token.
        RemoveTurns(); //Gotta make sure to clean up all of the turns left over for this token.
        //And now remove the token from the board.

        if (this == GameControllerScript.playerCharacter)
        {
            GameControllerScript.gameState = GameStates.GAMEOVER;
        }
        Destroy(gameObject);
    }

    public void jumpToPosition(float newx, float newy, float newz)
    {
        //If we wanted it to a trace a straght line instantly and perform collisions, we would use MovePosition here.
        //However, we want an instantaneous teleport, so instead we're going to directly manipulate the transform.
        transform.position = new Vector3(newx, transform.position.y, newz);

        GameControllerScript.log(GameControllerScript.time.ToString() + ": " + getName() + " moves to a new spot.");
    }

    public void jumpToPosition(Vector3 newPosition)
    {
        /*
        //If we wanted it to a trace a straght line instantly and perform collisions, we would use MovePosition here.
        //However, we want an instantaneous teleport, so instead we're going to directly manipulate the transform.
        transform.position = newPosition;

        GameControllerScript.log(GameControllerScript.time.ToString() + ": " + getName() + " moves to a new spot.");
        */

        jumpToPosition(newPosition.x, transform.position.y, newPosition.z);
    }

    public void PerformTurnAction()
    {
        if (this != GameControllerScript.playerCharacter)
        {
			if (this.nickname.Equals("Charlotte"))
			{
				this.takeRecovery(this.potency);
				GameControllerScript.log(this.getName() + " sets to work mending her wounds with magic. She recovers " + this.potency + " hp.");
			}
			else
			{
				//Take a chance of either damaging self or doing nothing.
				int randomAction = UnityEngine.Random.Range(0,10);
				if (randomAction < 8)
				{
					//Self harm. At least this way there's something approximating a goal.
	            	string damageString = this.takeDamage(this.potency); 
	            	GameControllerScript.log(this.getName() + " attacks " + this.getName() + " for " + damageString + " damage.");
				}
				else
				{
					//Do nothing of particular interest.
					GameControllerScript.log(this.getName() + " was distracted by a shiny magic balloon.");
				}
			}
        } 
		else
        {
            GameControllerScript.log("Player's Turn!");
        }
    }

    //This method is a quick and dirty status report into the log. 
    //If there were more time, it would be nice to have individual setters and gettters for each variable to pull information and draw it.
    public string getStatusString()
    {
        string result = "";
        result += getName() + " HP: " + hp + " / " + maxhp + " RES: " + res + " / " + maxres + "\n";
        result += getName() + " Potency: " + potency + " Defense: " + defense + " Action: " + act + " Level: " + level + "\n";

        return result;
    }

    
}