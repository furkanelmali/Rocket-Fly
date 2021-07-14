using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationspeed = 100f;
    [SerializeField] AudioClip engine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audi;

    

    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

         Keybinding();
         Keybindingspace();

    }

    void Keybindingspace()
    {
        
        if(Input.GetKey(KeyCode.Space))
        {

            if (!audi.isPlaying)
            {
                
                audi.PlayOneShot(engine);
                rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            }
            else
            {
                rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            }

            if (!mainBooster.isPlaying) 
            {
                mainBooster.Play();
            }

                      
        }
        else
        {
            mainBooster.Stop();
            audi.Stop();

        }

    }

    void Keybinding() 
    {
        
        if (Input.GetKey(KeyCode.LeftArrow ))
        {  
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
            rb.freezeRotation = true; 
            applyRotation(rotationspeed);
            rb.freezeRotation = false;

        }
        

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
            rb.freezeRotation = true;
            applyRotation(-rotationspeed);
            rb.freezeRotation = false;
        }

        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    public void applyRotation(float rotationspeed) 
    {

        transform.Rotate(Vector3.forward * rotationspeed * Time.deltaTime);

    }

    
}
    