using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ataque_Aux : MonoBehaviour
{
    public float count;
    public float fuerzaExpulsion=50;
    public GameObject player;
    public float distanciaExpulsion = 5f; 
    public float duracionEmpuje = 0.5f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count+=Time.deltaTime;
        if (count>=1){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other) {
        Rigidbody rb =other.GetComponent<Rigidbody>();
        if (other.CompareTag("Enemy")){
            StartCoroutine(EmpujarEnemigo(other.transform));
        }
    }
    void OnTriggerStay(Collider other) {
         Rigidbody rb =other.GetComponent<Rigidbody>();
        if (other.CompareTag("Enemy")){
            StartCoroutine(EmpujarEnemigo(other.transform));
        }
    }
    IEnumerator EmpujarEnemigo(Transform enemigo)
    {
        Vector3 direccion = (enemigo.position - player.transform.position).normalized;
        //Vector3 direccion = player.transform.position;

        Vector3 posicionFinal = enemigo.position + direccion * distanciaExpulsion;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionEmpuje)
        {
            enemigo.position = Vector3.Lerp(enemigo.position, posicionFinal, tiempoTranscurrido/duracionEmpuje);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }
}
