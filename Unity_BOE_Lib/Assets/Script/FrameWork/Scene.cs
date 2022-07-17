using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 각 씬에 prefab(SceneLoad)에 있는 스크립트 씬 전환 및 관리를 하는 클래스
/// </summary>
public class Scene : MonoBehaviour
{
    protected Scene() { }

    public Slider LoadingSlider;
    public GameObject LoadingObj;
    public Text ProgressText;

    Animator SceneAni;
    string Scenename;

    void Start()
    {
        SceneAni = GetComponent<Animator>();
    }

    public void FadeScene(string name)
    {
        SceneAni.SetTrigger("Fade_Out");
        Scenename = name;
    }

    public void LoadingScene(string name)
    {
        StartCoroutine(LoadAsynchronously(name));
    }

    IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingSlider.value = progress;
            ProgressText.text = progress * 100f + "%";

            LoadingObj.SetActive(true);

            yield return null;
        }
    }
}
