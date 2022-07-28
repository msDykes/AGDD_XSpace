using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] AudioClip crashSound = null;
    [SerializeField] AudioClip successSound = null;

    AudioSource audioSource = null;
    bool isTransitioning = false;

    void Start()
    {
        audioSource=GetComponent<AudioSource>(); 
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly!!");
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
        isTransitioning = true;
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = sceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
