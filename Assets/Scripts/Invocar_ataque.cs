using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invocar_ataque : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemigo;
    public EnemigoIA enemigoIA;
    public GameObject ataque;
    public bool primera=true;
    void Start()
    {
        enemigoIA=enemigo.GetComponent<EnemigoIA>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemigoIA.aparecer_ataque && primera)
        {
            primera = false;
            Instantiate(ataque, enemigo.transform.position, enemigo.transform.rotation);
        }
        if (enemigoIA.recuperar)
        {
            primera=true;
        }
    }
}
