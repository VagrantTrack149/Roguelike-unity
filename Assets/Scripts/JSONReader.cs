using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

[System.Serializable]
public class Mensaje
{
    public string mensaje;
}

[System.Serializable]
public class MensajeList
{
    public List<Mensaje> mensajes;
}

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    private List<Mensaje> listaMensajes;
    private int indiceActual = 0;
    private bool jsonCargado = false;

    void Start()
    {
        CargarJSON();
    }

    void Update()
    {
        // Mostrar siguiente mensaje al presionar Espacio
        if (Input.GetKeyDown(KeyCode.Space) && jsonCargado)
        {
            MostrarSiguienteMensaje();
        }
        
        // Opcional: Reiniciar con R
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarMensajes();
        }
    }

    public void CargarJSON()
    {
        if (jsonFile != null)
        {
            var datos = JsonUtility.FromJson<MensajeList>(jsonFile.text);

            if (datos != null && datos.mensajes != null)
            {
                listaMensajes = datos.mensajes;
                jsonCargado = true;
                Debug.Log("JSON cargado correctamente. Presiona Espacio para comenzar.");
            }
            else
            {
                Debug.LogError("El archivo JSON no tiene la estructura esperada");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado un archivo JSON");
        }
    }

    public string MostrarSiguienteMensaje()
    {
        if (indiceActual < listaMensajes.Count)
        {
            string mensajeActual = listaMensajes[indiceActual].mensaje;

            //Debug.Log($"Mensaje {indiceActual + 1}: {listaMensajes[indiceActual].mensaje}");
            indiceActual++;
            Debug.Log(mensajeActual);
            return mensajeActual;
        }
        else
        {
            //Debug.Log("No hay más mensajes. Presiona R para reiniciar.");
            return null;
        }
    }

    public void ReiniciarMensajes()
    {
        indiceActual = 0;
        Debug.Log("Mensajes reiniciados. Presiona Espacio para comenzar.");
    }

    // Función pública por si necesitas llamarla desde otro script
    public void MostrarMensajeActual()
    {
        if (jsonCargado && indiceActual < listaMensajes.Count)
        {
            Debug.Log(listaMensajes[indiceActual].mensaje);
        }
    }
}