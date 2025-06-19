using UnityEngine;
using TMPro;

public class Dialogos : MonoBehaviour
{
    public bool Platica = false, Platica_Stil=false;
    public JSONReader json;
    public TMP_Text textMeshProComponent; // Referencia asignada desde el Inspector
    public GameObject Panel_Texto;

    void Start()
    {
        // Asigna el componente de texto directamente desde el objeto
        Panel_Texto = GameObject.Find("Fondo_dialogo");
        if (Panel_Texto != null)
        {
            Transform hijo = Panel_Texto.transform.Find("Texto");
            textMeshProComponent = hijo.GetComponent<TextMeshProUGUI>();
        }
        Panel_Texto.SetActive(false);

        json = GetComponent<JSONReader>();
    }

    void Update()
    {
        if (Platica && json.jsonCargado)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Panel_Texto.SetActive(Platica);
                string texto = json.MostrarSiguienteMensaje();
                textMeshProComponent.text = "...";
                // Asignar el texto al componente TextMeshPro
                if (texto != null && textMeshProComponent != null)
                {
                    textMeshProComponent.text = texto;
                }
                else
                {
                    // Finalizar diálogo
                    Platica = false;
                    Panel_Texto.SetActive(Platica);
                    json.ReiniciarMensajes();
                    textMeshProComponent.text = "";
                    if (Platica_Stil ){
                        Platica = true;
                        json.CargarJSON(); 
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Platica = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Panel_Texto.SetActive(Platica);
            }
            if (Panel_Texto != null)
            {
                Transform hijo = Panel_Texto.transform.Find("Texto");
                textMeshProComponent = hijo.GetComponent<TextMeshProUGUI>();
            }
            json.CargarJSON(); // Cargar solo al iniciar diálogo
        }
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.Space)){
            Platica_Stil = true;
        }
        
    }
}
