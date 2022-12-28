using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failed;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem failedParticle;

    AudioSource audioSource;

    bool isTrasitioning = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {

        if (isTrasitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                SuccessSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    void SuccessSequence() {
        isTrasitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", 1f);
    }
    
    void CrashSequence() {
        isTrasitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(failed);
        failedParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("You blew up");
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        Debug.Log("You clear the level");
        SceneManager.LoadScene(nextSceneIndex);
    }

}
