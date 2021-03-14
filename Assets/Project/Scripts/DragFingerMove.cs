using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFingerMove : MonoBehaviour
{
    public Rigidbody rb;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Vector2 direction;
    public float speed=7;//ideal 7
    private Vector3 velocity;
    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
     private void Update()
    {
        Swipe();
    }
    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Moved)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards                
                if (currentSwipe.y > 0 && currentSwipe.x > -0.9f && currentSwipe.x < 0.9f) 
                {
                    Debug.Log("up swipe");
                    Move();
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.9f && currentSwipe.x < 0.9f)
                {
                    Debug.Log("down swipe");
                    Move();
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.9f && currentSwipe.y < 0.9f)
                {
                    Debug.Log("left swipe");
                    Move();
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.9f && currentSwipe.y < 0.9f)
                {
                    Debug.Log("right swipe");
                    Move();
                }
            }
            if (t.phase == TouchPhase.Ended)
            {   
                rb.velocity = Vector2.zero;
            }           
        }
    }
    void Move()
    {
        direction = Input.touches[0].deltaPosition.normalized;  //Unit Vector of change in position
        velocity = new Vector3(direction.x, direction.y,0) * speed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}

