using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barking : MonoBehaviour
{
    
    public AudioSource bark = null;
    
    public void Bark()
    {
        // if not defined outside
        if (null == bark)
            // use default barking sound 
            bark = GetComponent<AudioSource>();

        bark.Play();
    }

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
