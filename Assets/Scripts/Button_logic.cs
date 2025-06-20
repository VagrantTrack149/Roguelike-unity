using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_logic : MonoBehaviour
{
    public GameObject Puerta;
    public Color rojo = Color.red;
    private Renderer rend;
    public Puerta_logic logic;

    void Start()
    {
        rend = GetComponent<Renderer>();
        logic=Puerta.GetComponent<Puerta_logic>();
    }
    void OnTriggerEnter(Collider other) {
        
        logic.Abierto = true;
        rend.material.color = rojo; 
    }
}