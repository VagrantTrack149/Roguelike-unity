using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public Animator anima;
    public float duration =0;
    public GameObject[] Enemies;
    public int radio=15;
    public Vida_enemy vida_Enemy;
    public RayOBJ rayOBJ;
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
            if (Vector3.Distance(Enemy.transform.position, transform.position) < radio)
            {
                if (rayOBJ.poseido==false){
                    vida_Enemy = Enemy.GetComponent<Vida_enemy>();
                    vida_Enemy.Daño(10);
                    //Destroy(Enemy);
                }
            }
        }
    }
    public void Destruirse(){
        Destroy(gameObject); 
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if (rayOBJ.poseido==false){
                vida_Enemy = other.GetComponent<Vida_enemy>();
                vida_Enemy.Daño(10);
                //Destroy(other.gameObject);
                Debug.Log(other);
            }
        }    
    }
}
