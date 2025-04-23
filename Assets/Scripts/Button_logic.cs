using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_logic : MonoBehaviour
{
    public Puerta_logic Puerta_Logic;
    private void OnTriggerStay(Collider other) {
        if(Input.GetKeyDown(KeyCode.E)) {
            Puerta_Logic.Abierto=true;
        }
    }
}
