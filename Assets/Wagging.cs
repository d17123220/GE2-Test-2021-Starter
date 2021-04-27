using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagging : MonoBehaviour
{
    public float maxAngle = 65.0f;
    public int tailDirection = 1;
    public float tailAngle = 0.0f;
    public float minWagSpeed = 130.0f;
    public float maxWagSpeed = 720.0f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get dog, parent of the tail 
        GameObject dog = this.transform.parent.gameObject;

        // get dog's velocity
        float dogSpeed = dog.GetComponent<Boid>().velocity.magnitude;
        float maxSpeed = dog.GetComponent<Boid>().maxSpeed;

        // get dog's tail sausage
        GameObject tail = transform.GetChild(0).gameObject;

        // calculate waghging speed
        float rotationSpeed = maxWagSpeed * dogSpeed / maxSpeed; 
        rotationSpeed = Mathf.Clamp(rotationSpeed, minWagSpeed, maxWagSpeed);

        // rotate tail around base
        float rotationAngle = rotationSpeed * Time.deltaTime * tailDirection;
        tail.transform.RotateAround(
                tail.transform.position + tail.transform.up * tail.transform.localScale.y, 
                tail.transform.forward, 
                rotationAngle); // (point, axis, angle)

        // modify global angle of the tail for tracking
        tailAngle += rotationAngle;

        // Check if tail at the maximum angle
        if ((tailAngle >= maxAngle && tailDirection > 0) || (tailAngle <= -1*maxAngle && tailDirection < 0 ))
            // change direction of wagging
            tailDirection = -1 * tailDirection;        


    }
}
