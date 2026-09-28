using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject buttonQuit;
    public GameObject buttonPlay;
    public TextMeshProUGUI titre;
    private Coroutine corout;
    private Coroutine corout2;

    void Start()
    {
        StartChangeFont();
        StartChangeRotation();
    }

    void Update()
    {

    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("GamePvE");
    }

    public void LoadGameScenePvP()
    {
        SceneManager.LoadSceneAsync("GamePvP");
    }
    public void LoadGameSceneInfinie()
    {
        SceneManager.LoadSceneAsync("GameInfinie");
    }
    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void StartChangeFont()
    {
        corout = StartCoroutine(ChangeFont());
    }
    public void StopChangeFont()
    {
        StopCoroutine(corout);
    }

    public IEnumerator ChangeFont()
    {
        float size = 120f;
        while (true)
        {
            while (size <= 150f)
            {
                yield return new WaitForSeconds(0.02f);
                titre.fontSize = size;
                size++;
            }
            while (size >= 120f)
            {
                yield return new WaitForSeconds(0.02f);
                titre.fontSize = size;
                size--;
            }
        }
    }
    public void StartChangeRotation()
    {
        corout2 = StartCoroutine(ChangeRotation());
    }
    public void StopChangeRotation()
    {
        StopCoroutine(corout2);
    }
    public IEnumerator ChangeRotation()
    {
        float rotate = 0f;
        while (true)
        {
            while (rotate <= 15f)
            {
                yield return new WaitForSeconds(0.02f);
                titre.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotate);
                rotate++;
            }
            while (rotate >= -10f)
            {
                yield return new WaitForSeconds(0.02f);
                titre.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotate);
                rotate--;
            }
        }
    }
    public IEnumerator Animate()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * 0.8f, 1f);
            t = Mathf.SmoothStep(0f, 1f, t); // ease in/out
            titre.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(-10f, 15f, t));
            titre.fontSize = Mathf.Lerp(120f, 150f, t);
            yield return null;
        }
    }
}
