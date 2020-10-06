using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class ballManager : MonoBehaviour
{
    //variables for gross motor level operations
    public GameObject ball;
    public GameObject playerCamera;
    private float throwForce = 480;
    private float ballToPlayerDistance;
    private float holdingDistance = 0.9f;
    private static int score;
    [SerializeField] public string LoadLevel;
    public Text scoreText;

    private bool holdingBall;

    Vector3 originalPos;
    private void Start() //sets relevant defaults 
    {
        //set original position
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.GetComponent<Rigidbody>().detectCollisions = true;
        holdingBall = false;
    }
    void Update()
    {
        //calculate the ball to player distance
        ballToPlayerDistance = Vector3.Distance(ball.transform.position, playerCamera.transform.position);

        //only picks up the ball if the player is at a certain distance and the left mouse button is clicked
        if ((Input.GetMouseButtonDown(0)) && ballToPlayerDistance <= 1)
        {
            holdingBall = true;
            ball.GetComponent<Rigidbody>().useGravity = false;
            ball.GetComponent<Rigidbody>().detectCollisions = true;
        }
        if (holdingBall)
        {
            //calculate the distance the ball will be held at when moving with the camera 
            ball.transform.position = playerCamera.transform.position + playerCamera.transform.forward * holdingDistance;
            //if right mouse button is clicked set gravity back to true and throw the ball in the direction the player is facing
            if (Input.GetMouseButtonDown(1))
            {
                holdingBall = false;
                ball.GetComponent<Rigidbody>().useGravity = true;
                ball.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * throwForce);

            }

        }
        else
        {
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.GetComponent<Rigidbody>().detectCollisions = true;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "collidableObject1" || collision.gameObject.name == "collidableObject2" || collision.gameObject.name == "collidableObject3" || collision.gameObject.name == "collidableObject4" || collision.gameObject.name == "collidableObject5" || collision.gameObject.name == "collidableObject6")
        {
            //target hit
            Destroy(collision.gameObject);
            Destroy(ball);
            score++;
            scoreText.text = "score: " + score;

        }
        // if the ball hits the back wall, reset ball position. 
        if (collision.gameObject.name == "back_wall")
        {
            ball.transform.position = originalPos;
        }
        //if ball hits any other wall or the floor while holding it, drop the ball on the floor
        if (holdingBall)
        {
            if (collision.gameObject.name == "room1_left_wall")
            {
                holdingBall = false;
            }
            if (collision.gameObject.name == "room1_room3_wall")
            {
                holdingBall = false;
            }
            if (collision.gameObject.name == "hallway_left_wall1")
            {
                holdingBall = false;
            }
            if (collision.gameObject.name == "hallway_left_wall2")
            {
                holdingBall = false;
            }
            if (collision.gameObject.name == "game_floor")
            {
                holdingBall = false;
            }

            if (collision.gameObject.name == "back_wall")
            {
                holdingBall = false;
            }
        }
        // if all targets have been hit, move onto the next level
        if (score >= 6)
        {
            SceneManager.LoadScene(LoadLevel);
        }

    }

}