using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class basket_tetik : MonoBehaviour
{
    
    RaycastHit basket;
    public SphereCollider top;
    public ParticleSystem pota_efekt,pota_sag_efekt,pota_sol_efekt,sekme_efekt;
    public GameObject pota_engel;
    public Animator pota;
    
     void Start() 
     {
         
         
     }
    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position,transform.right,out basket,0.5f)||Physics.Raycast(transform.position+new Vector3(0,0,0.2f),transform.right,out basket,0.5f))
        {
            if(basket.collider.gameObject.tag=="player")
            {
                pota_engel.SetActive(false);
                Debug.Log("basket oldu") ;
                top.material.bounciness=0.4f;
                Invoke("yenidenbaslat",4f);
                efekt_baslat();
                
            }
            
        }
       // Debug.DrawRay(transform.position,transform.right*0.5f,Color.green);
       // Debug.DrawRay(transform.position+new Vector3(0,0,0.2f),transform.right*0.5f,Color.green);
    }
    public void  yenidenbaslat()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        // Scene sahne = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(sahne.name);
    }
    void efekt_baslat()
    {
        var main = pota_sol_efekt.main;
        main.loop =true;
        var main1 = pota_sag_efekt.main;
        main1.loop=true;
        pota_efekt.Play();
        pota_sag_efekt.Play();
        pota_sol_efekt.Play();
        pota.Play("renk_degisim");

    }
}
