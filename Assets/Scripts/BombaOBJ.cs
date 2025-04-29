using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaOBJ : MonoBehaviour
{
    public float b;
    public float count;
    public Rigidbody rb;
    public int vel=3;
    public GameObject Explotion;
    void Start() {
        rb=GetComponent<Rigidbody>();
    }
    void Update()
    { 
        count+=Time.deltaTime;
        if (count<2){
            rb.AddForce(transform.forward*vel);
        }
        if (count>=2){
            Instantiate(Explotion,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
