using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum GameStates
{
    MENU,
    PLAYING,
    CUTSCENE,
    WAITING,
    PAUSED,
    VICTORY,
    GAMEOVER
}
;

public class GameControllerScript : MonoBehaviour
{
    
    //Property instantiations and whatnot.
    public static GameStates gameState;
    public const bool DEBUGGING = false;
    public const int ENDTIME = 1000;
    public static int time = 0;
    public static bool enableSound = true;
    public static bool enableMusic = true;
    //public GameObject bgm;

    AudioSource soundForButtons;
    AudioSource soundForNotButtons;
    AudioSource music;

    //This is kinda workaroundy, but it's a way of tracking the player Token for special treatment without copypasta everywhere.
    public static Token playerCharacter = null;

    //The Flow will be handled in the GameControllerScript since it needs to be a singleton type setup anyway.
    public const int maxflow = 1000;
    public static int flow = maxflow; //Just in case by some weird glitch this does get created multiple times, flow will be the same among all.

    //This action-log is useful both for us (current me and future me) and for the player, since the player probably won't be directly shown what actions the other characters are taking.
    public ActionLog theActionLog = new ActionLog();

    //This is used for drawing.
    public GameObject theTextMesh;
    public GUIStyle diurnalStyle = new GUIStyle();
    public Texture2D blackPixelTex;
    public Texture2D orangePixelTex;

        




    
    //public List<Turn> turnList = new List<Turn>();
    TurnList theTurnList = new TurnList();// = new TurnList(new Token("Radstar", 10));
    
    public Token recentTokenSelection = null;
    public Token previousTokenSelection = null;
    public Cell firstCellSelection = null;
    public Cell secondCellSelection = null;
    int touchHeld = 0;
    
    
    //Instantiation of Listeners
    public static event Action<String> addToLog; //This is a function we're going to call to do something in another class.
    
    //-----EVENT HANDLERS-----


    void Awake()
    {

        flow = maxflow;
        time = 0;
        //Intentionally not changing the status of EnableMusic or Enable Sound. This means if the level reloads, this still stays the same.
    }

    // Use this for initialization
    void Start()
    {
        //Get all of the whatchajigs from all of the Tokens, make turns for each, and feed their turns into the array list.
        
        /*
        Token tempToken = new Token();
        Turn tempTurn = new Turn(tempToken);
        turnList.Add(tempTurn);
        
        tempToken = new Token("Kilo", 20);
        tempTurn = new Turn(tempToken);
        //turnList.Add(tempTurn);
        
        tempToken = new Token("Reveille", 10);
        tempTurn = new Turn(tempToken);
        //turnList.Add(tempTurn);
        
        if (DEBUGGING)
        {
            log("HI!");
            
            log("List Contents: " + printTurnList());
            
            log("Now let's play with the constructorthingy!");
            
            TurnList theTurnList = new TurnList(new Token("Radstar", 10));
            log(theTurnList.ToString());
            
            
            theTurnList.AddTurnsOfToken(new Token("Reveille", 10));
            theTurnList.AddTurnsOfToken(new Token("Kilo", 20));
            log(theTurnList.ToString());
        }
        */
        
        
        
        //FIXME: This is just some slapped together code to make it run, but is incorrect. This adds them to the list linearly, not according to the ACT at all.
        
        //theTurnList.AddTurnsOfToken(new Token("Reveille", 10)); 
        //theTurnList.AddTurnsOfToken(new Token("Kilo", 20));
        
        
        gameState = GameStates.PLAYING;

        AudioSource[] allAudio = GetComponents<AudioSource>();
        soundForButtons = allAudio [0];
        soundForNotButtons = allAudio [1];
        music = allAudio [2];


    
    }
    
    // Update is called once per frame
    void Update()
    {
        /* WE NEED TO USE THIS VERSION TO HANDLE CLICKING. THIS IS SOME COPYPASTA. FIX ME.
        switch (gameState) {
        case GameStates.PLAYING:
            if (Input.GetKey (KeyCode.Escape) || Input.touchCount == 2) {
                if (pause != null) {
                    pause ();
                }
                gameState = GameStates.MENU;
                Time.timeScale = 0.0f;
            }
            
            //And now we want to allow the spheres to be clicked on.
            if (Input.GetButtonDown ("Fire1")) {
                //This should actually work with touch devices, but we'll have to manually cast a ray and track the mouse position to find what has been tapped/clicked on.
                
                //Read the coordinates of the mouse
                
                //Cast the ray
                
                
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit; //We can pretty much pull rigidbody type info from whatever it hits.
                if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
                    //hit.
                    SphereWSM sphere = hit.transform.gameObject.GetComponent<SphereWSM> (); //gets a reference to the script.
                    
                    //Check to make sure we acquired a sphere to prevent errors.
                    if (sphere != null) {
                        sphere.onHit ();
                        
                    }
                }
                
                
                time++;
            }
        }
    END OF BROKEN COPYPASTA!*/
        
        switch (gameState)
        {
            case GameStates.PLAYING:
                if (Input.GetKey(KeyCode.Escape) || Input.touchCount == 2)
                {  //Pauses the game on Escape or Two-finger tap.
                    /*
                if (pause != null) {
                    pause ();
                }
                gameState = GameStates.MENU;
                Time.timeScale = 0.0f;
                */
                
                    //THIS IS THE SECTION WHERE STUFF GETS DONE ON ESCAPE OR MULTITOUCH. Might be good to have a menu state change or something here.
                }
            
            //And now we want to allow the spheres to be clicked on.
                if (Input.GetButtonDown("Fire1"))
                { //Originally, this type of code was intended to be contained in the Tokens and Cells themselves. By handling it in the Game Controller Script, the amount of update processing doesn't scale with the number of objects.
                    if (touchHeld == 0)
                    {
                        //This should actually work with touch devices, but we'll have to manually cast a ray and track the mouse position to find what has been tapped/clicked on.
                    
                        //Read the coordinates of the mouse
                    
                        //Cast the ray
                    
                    
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit; //We can pretty much pull rigidbody type info from whatever it hits.
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            //hit.
                            /*SphereWSM sphere = hit.transform.gameObject.GetComponent<SphereWSM> ();*/ //gets a reference to the script.
                        
                            Token theToken = hit.transform.gameObject.GetComponent<Token>();
                            Cell theCell = hit.transform.gameObject.GetComponent<Cell>();
                        
                            //Check to make sure we acquired a sphere to prevent errors.
                            if (theToken != null)
                            {
                                //If we got a Token, we can do soemthing with theToken here.
                                playNotButtonSound();
                                updateTokenSelections(theToken);
                            }
                        
                            if (theCell != null)
                            {
                                //If we got a Cell, we can do soemthing with theCell here.
                                playNotButtonSound();
                                updateCellSelections(theCell);
                            }
                        }
                    }
                
                
                    touchHeld++; //As long as the button is held down, add more to this timer.
                } else
                { //Executes if Fire1 is not being pressed.
                    touchHeld = 0;
                }
                break;
            default:
                break;
        }
        
    }
    
    //THESE ARE MY EVENT THINGIES. THEY MAKE EVENTS HAPPEN FOR LISTENERS TO LISTEN TO. Or something.
    
    public static void log(string theString)
    {
        if (addToLog != null)
        {
            addToLog(theString);
            //TODO: DO STUFF!

        }
    }

    //And now the OnGUI.

    void OnGUI()
    {
        int buttonCount = 0;
        //Let's draw a meter to handle the Flow thingy! And I'm gonna put it in a block to limit the scope of the variables. At least I hope that's how it works.
        {
            //Variables to handle positioning and scaling for the back-bar and the front-bar.
            int barLeft = Screen.width / 4;
            int barTop = 20;
            int barWidth = (Screen.width / 2) - 20;
            int barHeight = Screen.height / 50;

            //GUI.Label(new Rect(barLeft, barTop, barWidth, barHeight), "THE END IS NEVER THE END IS NEVER THE END IS NEVER THE END IS NEVER THE END IS NEVER THE END IS NEVER THE END IS NEVER THE END IS NEVER ");
            //GUI.Label(new Rect(barLeft, barTop, barWidth, barHeight), "", backBarGUIStyle);

            GUI.color = Color.black;
            GUI.DrawTexture(new Rect(barLeft, barTop, barWidth, barHeight), blackPixelTex);
            GUI.color = Color.white;
            GUI.DrawTexture(new Rect(barLeft, barTop, barWidth * ((float)flow / maxflow), barHeight), orangePixelTex);
            GUI.color = Color.white;
        }
        
        /*if (GUI.Button(Rect(10,10,150,100), "I am a button"))
        {
            print ("You clicked the button!");
        }*/
        
        if (DEBUGGING)
        {
            if (null != recentTokenSelection)
            {
                GUI.Label(new Rect(500, 10, 100, 100), "DBGU: " + recentTokenSelection.ToString());
            }
        }
        
        {
            int barLeft = Screen.width / 4;
            int barTop = 50;
            int barWidth = (Screen.width / 2) - 20;
            int barHeight = Screen.height / 10;

            GUI.Label(new Rect(barLeft, barTop, barWidth, barHeight), "Turn Order: " + time.ToString() + " " + theTurnList.ToString());
        }
        //Draw the Flow meter. Or at least, it would be nice to make this a meter later.
        GUI.Label(new Rect(Screen.width - (Screen.width / 20), 20, 100, 30), flow + " / " + maxflow);
        
        
        
        //GUI.Label(new Rect (100, 100, 1000, 1000), theTurnList.turns[1].ToString());
        //GUI.Label(new Rect (100, 100, 100, 100), "My frothing demand for explosions increases.");

        //The remaining buttons are dependant on the state of the game.

        //----------------------{START OF GAME STATE: PLAYING}---------------------
        switch (gameState)
        {
            case GameStates.PLAYING:
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Move"))
                {
                    if (DEBUGGING)
                    {
                        log("BUTTON RELEASED");
                        log(time.ToString() + ": Button Released.");
                    }
                    //Do stuff.
                    if (firstCellSelection != null)
                    {
                        playButtonSound();
                        nextTurnButtonResponse();
                        if (playerCharacter.currentCell)
                        {
                            playerCharacter.currentCell.giveTokenToDifferentCell(playerCharacter, firstCellSelection);
                        }
                        //playerCharacter.jumpToPosition(firstCellSelection.rigidbody.position);
                        string output = "The player moves."; //REMINDER: The first thing the player clicks will be OLD, and the second thing will be the most recent. We want the older thing to deal damage to the newer thing.
                        log(output);
                
                    }
                }

                buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Attack"))
                {
                //Rather than replace the references to previousTokenSelection with playerCharacter, it'll be enforced by replacing the value of previousTokenSelection with the player character.
                    previousTokenSelection = playerCharacter;
                    if (DEBUGGING)
                    {
                        log("BUTTON RELEASED");
                        log(time.ToString() + ": Button Released.");
                    }
                    //Do stuff.
                    if (recentTokenSelection != null && previousTokenSelection != null)
                    {
                        playButtonSound();

                        string damageString = recentTokenSelection.takeDamage(previousTokenSelection.potency); //REMINDER: The first thing the player clicks will be OLD, and the second thing will be the most recent. We want the older thing to deal damage to the newer thing.
                        log(previousTokenSelection.getName() + " attacks " + recentTokenSelection.getName() + " for " + damageString + " damage.");
                        nextTurnButtonResponse(); //This should be the last thing to happen after clicking the buttons.
                
                    }
                }

                buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Aid"))
                {
                
                //Rather than replace the references to previousTokenSelection with playerCharacter, it'll be enforced by replacing the value of previousTokenSelection with the player character.
                previousTokenSelection = playerCharacter;
                    if (DEBUGGING)
                    {
                        log("BUTTON RELEASED");
                    }
                    //Do stuff.
                    if (recentTokenSelection != null && previousTokenSelection != null)
                    {
                        if (DEBUGGING)
                        {
                            log("TWO SELECTED.");
                        }
                        playButtonSound();


                        recentTokenSelection.takeRecovery(previousTokenSelection.potency); //REMINDER: The first thing the player clicks will be OLD, and the second thing will be the most recent. We want the older thing to deal damage to the newer thing.
                        log(previousTokenSelection.getName() + " heals " + recentTokenSelection.getName() + " for " + previousTokenSelection.potency + " hp.");
                        nextTurnButtonResponse(); //This should be the last thing to happen after clicking the buttons.
                    }
                }

                buttonCount++; //Increment this to boost the offset.
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Status"))
                {
                    if (DEBUGGING)
                    {
                        log("BUTTON RELEASED");
                    }
                    //Do stuff.
                    if (recentTokenSelection != null)
                    {
                        if (DEBUGGING)
                        {
                            log("CHECKING STATUS OF SELECTION");
                        }
                        //recentTokenSelection.takeRecovery(previousTokenSelection.potency); //REMINDER: The first thing the player clicks will be OLD, and the second thing will be the most recent. We want the older thing to deal damage to the newer thing.
                
                        //TODO: Use the info from the passed token to change the state to a status display mode with a back button.
                        //TODO: While in Status mode, draw the status of the selected Token until the back button is pressed.
                        playButtonSound();
                        //nextTurnButtonResponse(); Checking status should not cost the player a turn.
                        log(recentTokenSelection.getStatusString());
                    }
                }

                if (DEBUGGING)
                {
                    buttonCount++; //Increment this to boost the offset.
                    if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "DEBUG:TURNEATER"))
                    {
                        if (DEBUGGING)
                        {
                            log("BUTTON RELEASED");
                        }
                        //Do stuff.
                        if (recentTokenSelection != null)
                        {
                            if (DEBUGGING)
                            {
                                log("Removing turns for " + recentTokenSelection.getName() + ".");
                            }
                            playButtonSound();
                            theTurnList.RemoveTurnsOfToken(recentTokenSelection);
                        }
                    }
                }

                if (DEBUGGING)
                {
                    buttonCount++; //Increment this to boost the offset.
                    if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "DEBUG:LEVELUP"))
                    {
                        if (DEBUGGING)
                        {
                            log("BUTTON RELEASED");
                        }
                        //Do stuff.
                        if (recentTokenSelection != null)
                        {
                            if (DEBUGGING)
                            {
                                log("Forcing Levelup for " + recentTokenSelection.getName() + ".");
                            }
                            playButtonSound();
                            recentTokenSelection.levelUp();
                        }
                    }
                }

                buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Options"))
                {
                    {
                        playButtonSound();
                        gameState = GameStates.MENU;
                    }
                }
            
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 10), Screen.height - (Screen.height / 20), (Screen.width / 20), (Screen.height / 20)), "Next Turn"))
                {
                    playButtonSound();
                    nextTurnButtonResponse();
                
                }
            
                break;
            
            case GameStates.WAITING:
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 10), Screen.height - (Screen.height / 20), (Screen.width / 20), (Screen.height / 20)), "Next Turn"))
                {
                    playButtonSound();
                    nextTurnButtonResponse();
                
                }
                break; //End of GAME IS PLAYING case buttons.
        //----------------------{START OF GAME STATE: CUTSCENE}---------------------

            case GameStates.CUTSCENE:
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 10), Screen.height - (Screen.height / 20), (Screen.width / 20), (Screen.height / 20)), "Confirm"))
                {
                    if (touchHeld == 0)
                    {
                        //Do stuff.
                    }
                }
                break;

        //----------------------{START OF GAME STATE: MENU}---------------------
            case GameStates.MENU:
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Toggle Music On/Off"))
                {
                    playButtonSound();
                    enableMusic = !enableMusic;
                    if (enableMusic)
                    {
                        music.Play();
                    } else
                    {
                        music.Pause();
                    }
                }
                
                buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Toggle Sound Effects On/Off"))
                {
               
                    {
                        playButtonSound();
                        enableSound = !enableSound;
                    }
                }

                buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
                if (GUI.Button(new Rect(10, 10 + (Screen.height / 20) * buttonCount, (Screen.width / 10), (Screen.height / 20)), "Exit Options Menu"))
                {
                    {
                        playButtonSound();
                        gameState = GameStates.PLAYING;
                    }
                }
                break;

        case GameStates.VICTORY:
        {
            
            if (GUI.Button(new Rect(10, 10 + (Screen.height / 3) * buttonCount, (Screen.width), (Screen.height / 3)), "Next"))
            {
                {
                    playButtonSound();
                    Application.LoadLevel(Application.loadedLevel + 1);
                }
            }
            buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
            if (GUI.Button(new Rect(10, 10 + (Screen.height / 3) * buttonCount, (Screen.width), (Screen.height / 3)), "Retry"))
            {
                {
                    playButtonSound();
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
            if (GUI.Button(new Rect(10, 10 + (Screen.height / 3) * buttonCount, (Screen.width), (Screen.height / 3)), "Quit"))
            {
                {
                    playButtonSound();
                    Application.Quit();
                }
            }
        }
            break;
            
        case GameStates.GAMEOVER:
        {
            
            if (GUI.Button(new Rect(10, 10 + (Screen.height / 2) * buttonCount, (Screen.width), (Screen.height / 2)), "Retry"))
            {
                {
                    playButtonSound();
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            buttonCount++; //Increment this to boost the offset. This is the first time we do this, so this should be 1. We're multiplying by the offset below.
            if (GUI.Button(new Rect(10, 10 + (Screen.height / 2) * buttonCount, (Screen.width), (Screen.height / 2)), "Quit"))
            {
                {
                    playButtonSound();
					Application.Quit();
                }
            }
        }
            break;

            default:
                break;
        }


        GetComponent<TextMesh>().text = theActionLog.ToString();
        
    }
    
    //-----UTILITY METHODS-----
    void updateTokenSelections(Token newToken)
    {
        previousTokenSelection = recentTokenSelection;
        recentTokenSelection = newToken;
        
    }
    
    void updateCellSelections(Cell newCell)
    {
        secondCellSelection = firstCellSelection;
        firstCellSelection = newCell;
    }

    void playButtonSound()
    {
        if (enableSound)
        {
            soundForButtons.Play();
        }
    }

    void playNotButtonSound()
    {
        if (enableSound)
        {
            soundForNotButtons.Play();
        }
    }

	//Gonna make this public and make a violation of coupling in Token for the purpose of making chained-NPC-Actions work in the time remaining.
	// On second thought, I removed the public access again. While manually advancing the turns is monotonous, it does mean that nothing important to the player is going to get accidentally scrolled off.
    void nextTurnButtonResponse() 
    {

        if (theTurnList.PerformNextTurn() != playerCharacter)
        {
            gameState = GameStates.WAITING;
        } 
        else
        {
            gameState = GameStates.PLAYING;
        }

        if (theTurnList.isEmpty())
        {
            gameState = GameStates.VICTORY;
        }
    }

    
    
    /*
    private string printTurnList()
    {
        string result = "";
        foreach(Turn theTurn in turnList)
        {
            result += theTurn.ToString();
        }
        return result;
    }
    */
}