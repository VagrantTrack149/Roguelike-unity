using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro; 

public class Dialogos : MonoBehaviour
{
    public bool Platica = false;
    public JSONReader json;
    private bool jsonCargado = false;
    public TMP_Text textMeshProComponent;
    public GameObject Panel_Texto;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Texto=GameObject.Find("Texto");
        textMeshProComponent = Panel_Texto.GetComponent<TextMeshProUGUI>();
        json = GetComponent<JSONReader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Platica)
        {
            json.CargarJSON();
            // Mostrar siguiente mensaje al presionar Espacio
            if (Input.GetKeyDown(KeyCode.Space) && jsonCargado)
            {
                string texto = json.MostrarSiguienteMensaje();
                textMeshProComponent.text = texto;

            }

        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.Space)){
            Platica=true;
        }
    }
    
}
