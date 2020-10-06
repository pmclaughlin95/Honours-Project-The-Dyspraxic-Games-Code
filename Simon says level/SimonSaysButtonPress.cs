using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysButtonPress : MonoBehaviour
{
    //declare variables for button press operations
    public Material LightMat;
    public Material DarkMat;
    private Renderer myR;

    public simonSaysRobot logic;

    public int Number;  
    public delegate void clickEvent(int number);
    public event clickEvent onClick;
    void Start() // sets defaults for the renderer
    {
        myR = GetComponent<Renderer>();
        myR.enabled = true;
     }
    private void OnMouseDown()
    {
        //checks whether the colour is clicked and passes a number into the event to check if this is the correct answer
        if (logic.player)
        {
            // changes colour to the darker material
            activeColour();
            onClick.Invoke(Number);
        }
    }
    private void OnMouseUp()
    {
        //changes colour to the original
        unActiveColour();

    }

    public void activeColour()
    {
        // set the renderer material to dark material, colours set up in the UI
        myR.sharedMaterial = DarkMat;
    }
    public void unActiveColour()
    {
        // sets renderer back to light material, colours set up in the UI
        myR.sharedMaterial = LightMat;
    }
}
