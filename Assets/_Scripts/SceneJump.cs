using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneJump : MonoBehaviour
{
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private bool asyncLoading;

    public static SceneJump instance;

    private bool changing = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        changing = false;
    }
    public void ChangeScene(int index)
    {
        if (!changing)
        {
            changing = true;
            Time.timeScale = 1;
            transition.SetTrigger("Start");
            StartCoroutine(LoadLevel(index));
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);

        Scene activeScene = SceneManager.GetActiveScene();

        if (asyncLoading)
        {
            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);

            // Wait until the level finishes loading
            while (!asyncLoadLevel.isDone)
                yield return null;

            // Wait a frame so every Awake and Start method is called
            yield return new WaitForEndOfFrame();
        }
        else
        {
            SceneManager.LoadScene(levelIndex);
        }

        
        
    }
}
