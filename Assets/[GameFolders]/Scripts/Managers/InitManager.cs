using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
public class InitManager : Singleton<InitManager>
{
    void Start()
    {
        Invoke("LoadMenuScene", 0.5f);
    }
    public void LoadMenuScene()
    {
        StartCoroutine(LoadMenuSceneCo());
    }
    private IEnumerator LoadMenuSceneCo()
    {
        yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);
        yield return SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
        Destroy(gameObject);
    }

}
