using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float maxTurnSpeed = 135.0f;
    public GameObject lookTarget = null;
    
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

            // use quaternion and lerp to look at target
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, maxTurnSpeed * Time.time);
        }
    }
}
