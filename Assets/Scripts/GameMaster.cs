using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private Coroutine corout;
    public TextMeshProUGUI txtCentral;
    private TextMeshProUGUI txtBtn;
    private Image imgBtn;
    public AudioSource audio;
    public Button btnNext;
    public Button btnRetry;
    public int timeIntro = 3;
    public float timeGame = 10f;
    public bool gameFinie = false;
    public bool gameStarted = false;
    public int res=3;
    public Sprite spriteGreen;
    public Sprite spriteRed;
    void Start()
    {
        txtBtn = btnNext.GetComponentInChildren<TextMeshProUGUI>();
        imgBtn = btnNext.GetComponent<Image>();
    }

    void Update()
    {

    }

    public void StartGameMaster()
    {
        corout = StartCoroutine(LaunchGameMaster());
    }

    public void StopGameMaster()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchGameMaster()
    {
        transform.localScale = new Vector3(1, 1, 1);
        btnRetry.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(false);
        txtCentral.fontSize = 300f;
        if (audio.isPlaying) { audio.Stop(); }
        audio.Play();
        for (int i = timeIntro; i > 0; i--) //decompte de depart
        {
            txtCentral.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        btnRetry.gameObject.SetActive(true);
        transform.localScale = new Vector3(0, 0, 0);
        gameStarted = true;
        txtCentral.fontSize = 200f;
        yield return new WaitForSeconds(timeGame); //temps du jeu
        gameFinie = true;
        btnNext.gameObject.SetActive(true);
        txtCentral.text = StringResult();
        switch (res) //change le panel suivant le score du joueur
        {
            case 0:
                txtBtn.text = "NEXT";
                imgBtn.sprite = spriteGreen;
                break;
            case 1:
            case 2:
                txtBtn.text = "RETRY";
                imgBtn.sprite = spriteRed;
                break;
            case 3:  // cas de la victoire finale
                imgBtn.sprite = spriteRed;
                txtBtn.text = "QUIT";
                txtCentral.fontSize = 150f;
                txtCentral.text = "YOU CANT BEAT THE GOAT";
                btnNext.onClick.AddListener(LoadMenuScene);
                break;
        }
        transform.localScale = new Vector3(1, 1, 1);
    }

    public string StringResult()
    {
        switch (res)
        {
            case 0:return "VICTOIRE !";
            case 1:return "EGALITE !";
            case 2:return "DEFAITE !";
            default: return "";
        }
    }

    public void Clear()
    {
        gameFinie = false;
        gameStarted = false;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

}
