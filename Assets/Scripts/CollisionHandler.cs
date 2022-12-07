using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _successSound;

    AudioSource audioSource;

    void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Nothing happens");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        audioSource.PlayOneShot(_successSound, 1f);

        GetComponent<Movement>().enabled = false;

        Invoke("LoadNextLevel", _levelLoadDelay);
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(_crashSound, 1f);
   
        GetComponent<Movement>().enabled = false;

        gameObject.tag = "Crashed";

        Invoke("ReloadLevel", _levelLoadDelay);
        Debug.Log("234");
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  
         int nextSceneIndex = currentSceneIndex + 1;
         if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
         {
                nextSceneIndex = 0;
         }
         if (gameObject.tag != "Crashed")
         {
            SceneManager.LoadScene(nextSceneIndex);
         }
    } 
}
