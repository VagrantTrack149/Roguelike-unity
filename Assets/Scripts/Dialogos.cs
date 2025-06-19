using UnityEngine;
using TMPro;

public class Dialogos : MonoBehaviour
{
    public bool Platica = false;
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
        if (Platica && json.jsonCargado){
            if (Input.GetKeyDown(KeyCode.Space)){
                string texto = json.MostrarSiguienteMensaje();
                
                // Asignar el texto al componente TextMeshPro
                if(texto != null && textMeshProComponent != null){
                    textMeshProComponent.text = texto;
                }
                else
                {
                    // Finalizar diálogo
                    Platica = false;
                    Panel_Texto.SetActive(Platica);
                    textMeshProComponent.text = "";
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Platica = true;
            Panel_Texto.SetActive(Platica);
            if (Panel_Texto != null){
                Transform hijo = Panel_Texto.transform.Find("Texto");
                textMeshProComponent = hijo.GetComponent<TextMeshProUGUI>();
            }
            json.CargarJSON(); // Cargar solo al iniciar diálogo
        }
    }
}
