using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_logic : MonoBehaviour
{
    public Puerta_logic Puerta_Logic;
    public Color rojo = Color.red;
    private Renderer rend; 

    void Start() {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other) {
        Puerta_Logic.Abierto = true;
        rend.material.color = rojo; 
    }
}