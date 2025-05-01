using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public float count;
    public GameObject ray;
    public GameObject player;
    public float offsetDistance = 2f;
    public bool trans=false;
    public PlayerMovement rm;
    public GameObject modelo;
    public GameObject enemigo;
    
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
        rm=player.GetComponent<PlayerMovement>();
        modelo=GameObject.Find("Model");
    }

    // Update is called once per frame
    void Update()
    {
        enemigo=GameObject.FindWithTag("Poseido");
        count+= Time.deltaTime;
        if (count>=1 && trans==false){
           lanzar();
        }
        if (count>=6 && trans==true){
            modelo.SetActive(true);
            Destroy(enemigo);
            player.tag="Player";
            trans=false;
        }
    }
    void lanzar(){
        Vector3 adjustedPosition=player.transform.position+player.transform.forward * offsetDistance;
        if (Input.GetKeyDown(KeyCode.E)){
            Instantiate(ray,adjustedPosition, player.transform.rotation);
            count=0; 
        }  
    }
}
