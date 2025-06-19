using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
[System.Serializable]
public class MensajeList
{
    public List<string> mensajes; // Cambiado a lista de strings directos
}

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    private List<string> listaMensajes; // Lista de strings
    private int indiceActual = 0;
    public bool jsonCargado = false; // Público para acceso externo

    public void CargarJSON()
    {
        if (jsonFile == null || jsonCargado) return;

        try
        {
            MensajeList datos = JsonUtility.FromJson<MensajeList>(jsonFile.text);
            listaMensajes = datos.mensajes;
            jsonCargado = true;
            Debug.Log("JSON cargado correctamente");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error cargando JSON: {e.Message}");
        }
    }

    public string MostrarSiguienteMensaje()
    {
        if (!jsonCargado || listaMensajes == null) return null;

        if (indiceActual < listaMensajes.Count)
        {
            return listaMensajes[indiceActual++];
        }
        return null; // Fin de los mensajes
    }

    public void ReiniciarMensajes()
    {
        indiceActual = 0;
    }
}