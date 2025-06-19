using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MensajeData
{
    public string mensaje;
}

[Serializable]
public class MensajesContainer
{
    public Dictionary<string, MensajeData> mensajes;
}

