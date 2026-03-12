using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float delay = 2f;

    Movement m;
    [SerializeField]AudioClip crash;
    [SerializeField]AudioClip success;

    //For Particles
    [SerializeField]ParticleSystem crashParticles;
    [SerializeField]ParticleSystem successParticles;
    AudioSource a;

    bool isTransitioning = false;

    void Start()
    {
        m = GetComponent<Movement>();
        a = GetComponent<AudioSource>();
    }
     void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                {
                    break;
                }
            case "Finish":
                {
                    StartSuccessSequence();
                    break;
                }
            default:
                {
                    //Create Method to reload the Scene
                    StartCrashSeqeunce();
                    break;
                }
        }
    }

    void StartCrashSeqeunce()
    {
        isTransitioning = true;
        a.Stop();
        a.PlayOneShot(crash); 
        crashParticles.Play();
        
        m.enabled = false; // Disable the movement script while we are in delay of 1 sec 
        Invoke("Respawn", delay); // Respawn after 1 second
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        a.Stop();
        a.PlayOneShot(success);
        successParticles.Play();
        m.enabled = false; // Disable the movement script while we are in delay of 1 sec
        Invoke("NextLevel",delay); // Jump to the next level after 1 second
    }

    //function to respawn 
    void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    //Jump on to the next level
    void NextLevel()
    {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}
