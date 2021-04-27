using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new SeekPlayerState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
