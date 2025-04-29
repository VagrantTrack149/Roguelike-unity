using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public Animator anima;
    public float duration =0;
    public GameObject[] Enemies;
    public int radio=15;
    void Start(){  
        anima.SetBool("Exp", true);
        if (Enemies == null)
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        duration+=Time.deltaTime;
        //Debug.Log(duration);
        foreach (var Enemy in Enemies){
            Debug.Log(Enemy);
            if (Vector3.Distance(Enemy.transform.position,transform.position) < radio){
                Destroy(Enemy);
            }
        }
    }
    public void Destruirse(){
        Destroy(gameObject); 
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")){
            Destroy(other.gameObject);
            Debug.Log(other);
        }    
    }
}
