using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;
    [SerializeField] float delay = 2f;

    [SerializeField] ParticleSystem explosionnparticles;
    [SerializeField] ParticleSystem successparticles;
    
    AudioSource source;
        
    public bool isTransitioning = false;

    void Start()
    {
       source = GetComponent<AudioSource>();
    }

    void Update()
    {
        respondkeys();
    }

    void respondkeys() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            nextlevel();
        }
    
    }
    
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(isTransitioning) 
        {
            return;
        }
       
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly");
                    break;

                case "Fuel":
                    Debug.Log("Fuel");
                    break;

                case "Untagged":

                    crashing();
                    break;

                case "Finish":
                    finish();
                    break;
            }
       

    }

    void crashing() 
    {
        explosionnparticles.Play();
        isTransitioning = true;
        source.Stop();
        source.PlayOneShot(explosion);
        Invoke("restart", delay);
        GetComponent<Movement>().enabled = false;

    }

    void finish()
    {
        successparticles.Play();
        isTransitioning = true;
        source.Stop();
        source.PlayOneShot(success);
        Invoke("nextlevel", delay);
        GetComponent<Movement>().enabled = false;
    }


    void restart()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
    }
    void nextlevel()
    {
        
        int nextsceneindex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextsceneindex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else 
        {
            SceneManager.LoadScene(nextsceneindex);
        }
        
    }



    
}
