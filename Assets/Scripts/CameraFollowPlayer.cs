using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;    
    public Vector3 offset= new Vector3(3, 28,-18);
    // Start is called before the first frame update
    private void LateUpdate() {
        if (target != null){
            transform.position=target.position+offset;
        }else{
            SceneManager.LoadScene(0);
        }
            
    }
}
