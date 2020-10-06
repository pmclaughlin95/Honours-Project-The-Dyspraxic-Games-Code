using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ColourCodedLevelBrown : MonoBehaviour
{
    //declare variables for organisation level brown objects 
    public GameObject gameobject;
    public GameObject playerCamera;
    private static float objectToPlayerDistance;
    private float holdingDistance = 0.9f;
    [SerializeField] private string LoadLevel;
    public Text scoreText;
    private bool holdingObject;

    Vector3 originalPos;
    private void Start()//sets relevant defaults 
    {
        //sets original position 
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        gameobject.GetComponent<Rigidbody>().useGravity = true;
        gameobject.GetComponent<Rigidbody>().detectCollisions = true;
        holdingObject = false;
    }
    void Update()
    {
        //calculate the object to player distance
        objectToPlayerDistance = Vector3.Distance(gameobject.transform.position, playerCamera.transform.position);

        //only picks up the object if the player is at a certain distance and the left mouse button is clicked
        if ((Input.GetMouseButtonDown(0)) && objectToPlayerDistance <= 1)
        {
            holdingObject = true;
            gameobject.GetComponent<Rigidbody>().useGravity = false;
            gameobject.GetComponent<Rigidbody>().detectCollisions = true;
        }
        if (holdingObject)
        {
            //calculate the distance the object will be held at when moving with the camera 
            gameobject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * holdingDistance;

            //if right mouse button is clicked set gravity back to true and drop the object
            if (Input.GetMouseButtonDown(1))
            {
                holdingObject = false;
                gameobject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            gameobject.GetComponent<Rigidbody>().useGravity = true;
            gameobject.GetComponent<Rigidbody>().detectCollisions = true;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "colourCodedFloorBrown")
        {
            //object has been dropped into the correct box's floor
            Destroy(gameobject);
            ColourCodedLevelYellow.score++;
            scoreText.text = "score: " + ColourCodedLevelYellow.score;

            // if all objects have been dropped into their correct boxes, move on to the next level
            if (ColourCodedLevelYellow.score >= 16)
            {
                SceneManager.LoadScene(LoadLevel);
            }
        }
        // if object lands in the incorrect box, reset object position. 
        else if (collision.gameObject.name == "colourCodedFloorGreen" || collision.gameObject.name == "colourCodedFloorBlue" || collision.gameObject.name == "colourCodedFloorYellow")
        {
            gameObject.transform.position = originalPos;
        }
        //if object hits any wall or the floor while holding it, drop the object on the floor
        if (holdingObject)
        {
            if (collision.gameObject.name == "room1_room3_wall")
            {
                holdingObject = false;
            }
            if (collision.gameObject.name == "room3_room5_wall")
            {
                holdingObject = false;
            }
            if (collision.gameObject.name == "hallway_left_wall2")
            {
                holdingObject = false;
            }
            if (collision.gameObject.name == "hallway_left_wall3")
            {
                holdingObject = false;
            }
            if (collision.gameObject.name == "game_floor")
            {
                holdingObject = false;
            }

            if (collision.gameObject.name == "outside_left_wall")
            {
                holdingObject = false;

            }
        }
    }
}