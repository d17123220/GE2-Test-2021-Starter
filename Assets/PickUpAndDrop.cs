using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndDrop : MonoBehaviour
{
    public GameObject ballObject = null;
    public float pickupSpeed = 5.0f;
    private bool pickingup = false;
    private bool dropping = false;
    public float throwForce = 150.0f;
    public float LookAtSpeed = 10.0f;
    public bool ballPicked = false;


    public void PickUp(GameObject ball)
    {
        ballObject = ball;
        pickingup = true;
        dropping = false;
    }

    public void Drop()
    {
        pickingup = false;
        dropping = true;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (null != ballObject)
        {
            if (pickingup)
            {
                // look at the ball
                Vector3 direction = ballObject.transform.position - transform.position;
                direction.y = 0.0f;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, LookAtSpeed * Time.deltaTime);

                // move ball to the mouth
                GameObject mouth = transform.Find("dog").Find("ballAttach").gameObject;
                ballObject.transform.position = Vector3.Lerp(ballObject.transform.position, mouth.transform.position, pickupSpeed * Time.deltaTime);



                // if ball in the mouth, it is done
                if (Vector3.Distance(ballObject.transform.position, mouth.transform.position) < 0.3f)
                {
                    ballObject.transform.position = mouth.transform.position;
                    ballObject.transform.parent = mouth.transform;
                    pickingup = false;
                    ballPicked = true;
                }
            }
            else if (dropping)
            {
                // drop ball from the mouth
                ballObject.transform.parent = null;
                Rigidbody rb = ballObject.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * throwForce);
                Camera.main.gameObject.GetComponent<FPSController>().BallReturned();
                dropping = false;
                ballPicked = false;
                ballObject = null;
                Destroy(ballObject, 1.0f);
            }
        }    
    }
}
