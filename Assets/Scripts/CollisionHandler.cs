using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebug();
    }

    void RespondToDebug()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other) {

        if(isTransitioning || collisionDisabled){ return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("All fine this thing is friendly");
                break;
            case "Finish":
                StartSuccessLevel();    
                break;
            default: 
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.PlayOneShot(death);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
        
    }

    void StartSuccessLevel() {
        isTransitioning = true;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void  ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
