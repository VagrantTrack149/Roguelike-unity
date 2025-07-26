using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desvanecer : MonoBehaviour
{
    public float time = 0f;
    public float T_vida = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time +=Time.deltaTime;
        //Debug.Log(time);
        if (time >= T_vida)
        {
            Destroy(gameObject);
        }
    }
}
