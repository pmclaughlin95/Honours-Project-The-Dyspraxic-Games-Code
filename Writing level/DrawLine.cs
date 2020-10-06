using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DrawLine : MonoBehaviour
{
    //declare variables for drawing/writing operations
    public GameObject linePrefab;
    public GameObject currentLine;
    public Camera cam;
    public static LineRenderer lineRenderer;
    [SerializeField] private string LoadLevel;
    public Text scoreText;
    int score;
    public List<Vector2> fingerPositions;

     void Start()
    {
        // since this is going from one camera where there is no cursor to another where the should be one, we have to make the cursor visible and remove the default lock state that keeps it in position
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        scoreText.text = "score: " + score;
        if (Input.GetMouseButtonDown(0))
        {
            score++;
            CreateLine();

        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = cam.ScreenToWorldPoint(Input.mousePosition);

            // checks if the distance between the tempFingerPos and the previous position is greater than the set buffer value of 0.1 and if so, call the updateLine function
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }

        }
        levelCompleted();

    }

    void CreateLine()
    {
        // Creates a new currentLine prefab
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        // save the line renderer component into the lineRenderer variable
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        //clear the list for the new line
        fingerPositions.Clear();
        // set the first 2 values for the list  to get the 2 points needed for drawing 
        fingerPositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));

        // set the first 2 positions of the line renderer component
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }


    void UpdateLine(Vector2 newFingerPos)
    {
        //add new finger position value to the list
        fingerPositions.Add(newFingerPos);
        // increase size of line renderer points
        lineRenderer.positionCount++;
        //set the new point to the value of newFingerPos
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

    }

    public void levelCompleted()
    {
        //if score is 20, the draw/writing limit is reached and the player will move onto the next level
        if (score >= 20)
        {
            SceneManager.LoadScene(LoadLevel);
        }
    }
}