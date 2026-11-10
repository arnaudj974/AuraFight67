using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonPlayCamp;
    [SerializeField] private Button buttonPlayInfi;
    [SerializeField] private Button buttonPlayPvp;
    [SerializeField] private TextMeshProUGUI titre;

    void Awake()
    {
        buttonQuit.onClick.AddListener(Quit);
        buttonPlayCamp.onClick.AddListener(() => LoadGameScene(GameManager.Mode.Campagne));
        buttonPlayInfi.onClick.AddListener(() => LoadGameScene(GameManager.Mode.Infini));
        buttonPlayPvp.onClick.AddListener(() => LoadGameScene(GameManager.Mode.PvP));
    }

    void Start()
    {
        StartCoroutine(ChangeFont());
        StartCoroutine(ChangeRotation());
    }

    public void LoadGameScene(GameManager.Mode mode)
    {
        GameManager.Instance.mode = mode;
        SceneManager.LoadSceneAsync("Game");
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
