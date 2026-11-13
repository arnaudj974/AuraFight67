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
        buttonPlayCamp.onClick.AddListener(() => LoadGameScene(Mode.Campagne,1));
        buttonPlayInfi.onClick.AddListener(() => LoadGameScene(Mode.Infini,1));
        buttonPlayPvp.onClick.AddListener(() => LoadGameScene(Mode.PvP,1));
    }

    void Start()
    {
        StartCoroutine(ChangeFont());
        StartCoroutine(ChangeRotation());
    }

    public void LoadGameScene(Mode mode, int lvl)
    {
        GameManager.Instance.mode = mode;
        GameManager.Instance.lvl = lvl;
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
}
