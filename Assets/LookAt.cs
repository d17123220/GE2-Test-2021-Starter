using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float maxTurnSpeed = 10.0f;
    public GameObject lookTarget;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        

        if (null != lookTarget)
        {
                        
            // get direction of the target
            Vector3 direction = lookTarget.transform.position - transform.position;
            direction.y = 0.0f;
            
            // use quaternion and lerp to look at target
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, maxTurnSpeed * Time.deltaTime);
        }
    }
}
