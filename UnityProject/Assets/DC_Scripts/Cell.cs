using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{
    
    //-----Property instantiations and whatnot.-----
    //private Vector3 position = transform.position;
    
    protected Texture mySprite;
    protected List<Token> contents = new List<Token>(); //WORKAROUND NOTE: WE changed this to public so that the subclass can still access the contents. This is not the safest way of halding this scenario. Maybe change to protected instead?
    protected string myName;
    protected Action useAction;
    protected string useActionName;

    //And now we're going to make it a little link-list styled.

    protected Cell next = null;
    protected Cell prev = null;
    
    public static event Action<Cell> Touched;
    
    
    //-----End of Properties-----
    
    
    
    //-----Instatiation of Lister Event Thingies-----
    
    //-----End of Listeners-----
    
    
    
    //public static event Action<Token> MakeNewTurns; //This is a function we're going to call.

    
    //-----Constructors-----
    
    //-----End of Constructors-----
    
    

    /*
    public void MakeTurns()
    {
        if(MakeNewTurns != null)
        {
            MakeNewTurns(this);
        }
    }
    */
    
    
    
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

    public void setNext(Cell newCell)
    {
        next = newCell;
    }

    public void setPrev(Cell newCell)
    {
        prev = newCell;
    }


    //-----End of Mutators-----
    
    
    //-----Event Maker Thingies-----
    
    public void WasTouched() //This works practically the same as the version in Token. Should look for a way to refactor this to avoid copypasta.
    {
        if (GameControllerScript.DEBUGGING)
        {
            print(this.ToString());
        }
        
        if (Touched != null)
        {   
            Touched(this);
        }
    }
    
    //-----End of Event Maker Thingies-----
    
    //-----Event Handlers-----
    
    // Use this for initialization
    void Start()
    {
    
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
    
    //-----End of Event Handlers-----
    
    //-----Other Methods Go Here-----
    public void addToken(Token newToken)
    {
        newToken.setCurrentCell(this);
        contents.Add(newToken);
        newToken.jumpToPosition(this.transform.position);
    }

    public void removeToken(Token newToken)
    {
        contents.Remove(newToken);
    }

    public void giveTokenToDifferentCell(Token newToken, Cell theCell)
    {
        theCell.addToken(newToken); //With this order, thie object is briefly in two locations at the same time...
        removeToken(newToken);//But then the older storage of it is removed.
    }
    
    //-----End of Other Methods-----
    

}
