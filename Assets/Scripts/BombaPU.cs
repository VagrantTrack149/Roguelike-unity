using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaPU : MonoBehaviour
{
    public GameObject bombaPrefab;
    public GameObject player;
    public float count;
    public float offsetDistance = 2f;
    void Start() {
    }
    void Update()
    { 
        Vector3 adjustedPosition=player.transform.position+player.transform.forward * offsetDistance;
        count+= Time.deltaTime;
        if (count>=1){
            if (Input.GetKeyDown(KeyCode.Space)){
                Instantiate(bombaPrefab, adjustedPosition, player.transform.rotation);
                count=0;
            }
        }     
          
    }
}
