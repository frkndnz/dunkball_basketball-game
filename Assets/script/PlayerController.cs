using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Rigidbody rb ;
    public Transform cam,pota,hedef;
    public ParticleSystem sekme_efekt;
    public float speed;
    public float y_speed;
    public float x_z_speed;

    bool top_y_yükseklik=true;
    bool pota_altı_kontrol=true;
    
    Vector3 pota_mesafe,hız_mesafe;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Debug.Log("mesafe"+pota_mesafe.magnitude);
        
    }
    
    void Update()
    {
        
        
        
    }
    private void FixedUpdate() 
     {
        if(rb.transform.position.y<=1f)
        {
            hız_hesapla();
        }
        if(Input.touchCount > 0)
        {
            Touch tıklama = Input.GetTouch(0);
            
            if(tıklama.phase == TouchPhase.Moved   ) //hareket ettiğimizde
            {
                Debug.Log("hareket ediyor");
                float delta_x = tıklama.deltaPosition.x;
                float delta_y = tıklama.deltaPosition.y;
                
                transform.LookAt(hedef.transform);
                Vector3 sağa_git =transform.right+transform.position;
                Vector3 sola_git =transform.position-transform.right;
                Vector3 geri_git =transform.position-new Vector3(0,0,-1);
             
                if(delta_x>0f && rb.transform.position.y<=1.7f)// sağa hareket
                {
                    Vector3 sağ_vektör= sağa_git-transform.position;
                    Vector3 sağ_vektor_normalıze=1.4f*sağ_vektör.normalized;
                    rb.velocity = new Vector3(sağ_vektor_normalıze.x*100*Time.deltaTime,100*Time.deltaTime,sağ_vektor_normalıze.z*100*Time.deltaTime);
                    
                    

                }
                else if(delta_x<0f && rb.transform.position.y<=1.7f) //sola hareket
                {
                    Vector3 sol_vektör = sola_git-transform.position;
                    Vector3 sol_vektor_normalıze =1.4f*sol_vektör.normalized;
                    rb.velocity = new Vector3(sol_vektor_normalıze.x*100*Time.deltaTime,100*Time.deltaTime,sol_vektor_normalıze.z*100*Time.deltaTime);
                }
                
                if(delta_y<0 && rb.transform.position.y<=1.7f && Mathf.Abs(delta_y)> Mathf.Abs(delta_x)) //geri hareket
                {
                    Vector3 geri_vektör = geri_git-transform.position;
                    Vector3 geri_vektör_normalize=2f*geri_git.normalized;
                    rb.velocity=new Vector3(geri_vektör_normalize.x*100*Time.deltaTime,100*Time.deltaTime,geri_vektör_normalize.z*100*Time.deltaTime);
                }
                 Debug.Log(tıklama.deltaPosition);
                
            }
            else if(tıklama.phase == TouchPhase.Stationary  )// dokunup beklerken
            {
                CancelInvoke("hız_hesapla");
                Debug.Log("dokunuyor");
                rb.transform.localRotation=Quaternion.Lerp(rb.rotation,cam.rotation,0.2f);
                
                pota_mesafe = pota.transform.position-rb.transform.position;
                Vector3 normalize_pota = pota_mesafe.normalized;

               // hız_degistir();
                if(rb.transform.position.y<=3.5f && top_y_yükseklik)
                {
                    //rb.velocity=new Vector3(0,0,0);
                    //rb.AddForce(new Vector3(normalize_pota.x*Time.deltaTime*x_z_speed,Time.deltaTime*y_speed,normalize_pota.z*x_z_speed*Time.deltaTime),ForceMode.Impulse);
                    rb.velocity=new Vector3(normalize_pota.x*Time.deltaTime*x_z_speed,Time.deltaTime*y_speed,normalize_pota.z*x_z_speed*Time.deltaTime);
                    if(rb.transform.position.y>=3.4f )
                    {
                        top_y_yükseklik=false;
                    }
                }
                

            }
            else if(tıklama.phase == TouchPhase.Ended) //parmağını çektiğinde
            {
                Debug.Log("Dokunma bitti");
                top_y_yükseklik=false;
                
                
            }
            Debug.Log("hızımız ="+rb.velocity.magnitude);
            Debug.Log("velocity y"+rb.velocity.y);
        }
        

        if(rb.transform.position.y<=1f)
        {
            top_y_yükseklik=true;
        }

        
        

     }
    
    private void OnTriggerStay(Collider other) 
    {
         if(rb.velocity.magnitude<=4.2f && other.tag == "ground" )
         {
             rb.velocity=Vector3.up*Time.deltaTime*speed;
            
            
         }
        if(other.tag=="arkaduvar")
        {
            rb.AddForce(new Vector3(0,0,-75*Time.deltaTime),ForceMode.Impulse);
        }
        if(other.tag=="ground" )
        {
            sekme_efekt.transform.position=rb.transform.position;
            sekme_efekt.Play();
            
        }
        

         
    }
    
        
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag =="ground")
        {
            top_y_yükseklik=true;
            //sekme_efekt.transform.position=rb.transform.position;
            //sekme_efekt.Play();
            //rb.velocity=Vector3.up*Time.deltaTime*speed;
            
        }
        
        
    }
    
    public void hız_hesapla()
    {
        hız_mesafe=pota.transform.position-rb.transform.position;

        if(hız_mesafe.magnitude <2.7f)
        {
            y_speed=210f;
            x_z_speed=115f;
        }
        else
        {
            y_speed=250f;
            x_z_speed=175f;
        }
    }
    
    
}
