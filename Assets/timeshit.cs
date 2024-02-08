using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeshit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float time = 180;
        float timeTrigger = time * 0.125f;
        Debug.Log("timeTrigger: " + timeTrigger);

        float time2 = 60;
        timeTrigger = time2 * 0.125f;
        Debug.Log("timeTrigger 2: " + timeTrigger); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
