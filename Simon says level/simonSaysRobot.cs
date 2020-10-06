using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class simonSaysRobot : MonoBehaviour
{
    //declare variables for simon says robot operations
    public SimonSaysButtonPress[] Buttons;
    public List<int> colourList;

    private float showTime = 0.5f;
    private float pauseTime = 0.5f;

    private int levels;
    private int playerLevel;
    private bool robot = false;
    public bool player = false;
    [SerializeField] private string LoadLevel;

    private int R; // random variable

    public Button start;
    public Text IncorrectInputText;
    public Text scoreText;
    private int score;
     void Start()
    {
        // since this is going from one camera where there is no cursor to another where the should be one, we have to make the cursor visible and remove the default lock state that keeps it in position
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        for (int i = 0; i < Buttons.Length; i++)
        {
            //Bounds player to the onclick event
            Buttons[i].onClick += ButtonClicked;
            //set the number of the button
            Buttons[i].Number = i;
        }
    }
    private void Update() 
    {
        if (robot)
        {
            //set the robot variable and start the robot routine
            robot = false;
            StartCoroutine(Robot());
        }
    }

    private void ButtonClicked(int _number)
    {

        if (player == true)
        {
            //if the button's number = the robots button sequence
            if (_number == colourList[playerLevel])
            {
                playerLevel++; 
                score++;  
                scoreText.text = score.ToString();

              //if score=14 go to the next level
                if (score >= 15)
                {
                    SceneManager.LoadScene(LoadLevel);
                }
            }
            //if the button's number does not = the robot's sequence 
            else if (_number != colourList[playerLevel])
            {
                IncorrectAnswer();
            }
            if (playerLevel == levels)
            {
                levels += 1;
                playerLevel = 0;
                player = false;
                robot = true;
            }
        }
    }
    private IEnumerator Robot()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < levels; i++)
        {
            //checks that the random selection is only registered if the level is higher than the level before
            if (colourList.Count < levels)
            {
                R = Random.Range(0, Buttons.Length);
                colourList.Add(R);
            }
            // Robot presses the buttons 
            Buttons[colourList[i]].activeColour();
            yield return new WaitForSeconds(showTime);
            Buttons[colourList[i]].unActiveColour();
            yield return new WaitForSeconds(pauseTime);

        }
        player = true;

    }

    //sets all the defaults of the gameplay elements of the simon says level, this happens once the play button is pressed on the UI
    public void StartGame()
    {
        robot = true;
        score = 0;
        playerLevel = 0;
        levels = 1;
        IncorrectInputText.text = "";
        scoreText.text = score.ToString();
        start.interactable = false;


    }
    void IncorrectAnswer()
    {
        //when a player input is incorrect, the player will have to press play again and start over 
        IncorrectInputText.text = "Incorrect";
        start.interactable = true;
        player = false;
    }

}
