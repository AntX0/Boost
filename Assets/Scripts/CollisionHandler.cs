using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _successSound;

    [SerializeField] private ParticleSystem _crashParticles;
    [SerializeField] private ParticleSystem _successParticles;

    AudioSource audioSource;
    BoxCollider boxCollider;

    bool isTransitoning = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ExecuteCheatCommands();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitoning) { return; }

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
        isTransitoning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(_successSound, 1f);
        _successParticles.Play();

        GetComponent<Movement>().enabled = false;

        Invoke("LoadNextLevel", _levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitoning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(_crashSound, 1f);
        _crashParticles.Play();

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

    private void ExecuteCheatCommands()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKey(KeyCode.C))
        {
            DissableCollisions();
        }
    }

    private void DissableCollisions()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }
}