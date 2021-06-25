using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takip : MonoBehaviour
{
    public Transform player,hedef;
    public float hız = 10f;
    public Vector3 mesafe;
    void Start()
    {
        mesafe.z = this.transform.position.z-player.transform.position.z;

       
    }

    
    private void LateUpdate()
    {
       
      
        this.transform.LookAt(hedef.transform);
       
        

    }
    private void FixedUpdate() 
    {
        Vector3 transform1=new Vector3(player.position.x,this.transform.position.y,player.position.z+mesafe.z);

        this.transform.position=Vector3.Lerp(this.transform.position,transform1,0.2f);   

        

    }
}
