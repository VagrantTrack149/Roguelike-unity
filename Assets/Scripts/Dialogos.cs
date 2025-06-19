using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Dialogos : MonoBehaviour
{
    public bool Platica = false, Platica_Stil = false;
    public JSONReader json;
    public TMP_Text textMeshProComponent; // Referencia asignada desde el Inspector
    public GameObject Panel_Texto;
    public PlayerMovement playerMovement;
    void Start()
    {
        // Asigna el componente de texto directamente desde el objeto
        Panel_Texto = GameObject.Find("Fondo_dialogo");
        GameObject player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
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
                playerMovement.Movement = false;
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
                    playerMovement.Movement = true;
                    // Finalizar diálogo
                    Platica = false;
                    Panel_Texto.SetActive(Platica);
                    json.ReiniciarMensajes();
                    textMeshProComponent.text = "";
                    if (Platica_Stil)
                    {
                        Platica = true;
                        json.CargarJSON();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Platica_Stil = false;
            Platica = false;
            Panel_Texto.SetActive(Platica);
            playerMovement.Movement = true;
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
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Platica_Stil = true;
        }
        else
        {
            Platica_Stil = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Platica_Stil = false;
        Platica = false;
        Panel_Texto.SetActive(Platica);
        playerMovement.Movement = true;
    }
}
