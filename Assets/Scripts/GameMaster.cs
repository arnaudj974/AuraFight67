using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private Coroutine corout;
    public TextMeshProUGUI txtCentral;
    public Button btnNext;
    public int timeIntro = 3;
    public float timeGame = 10f;
    public bool gameFinie = false;
    public bool gameStarted = false;
    public int res=3;
    public Sprite spriteGreen;
    public Sprite spriteRed;
    void Start()
    {

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
        btnNext.gameObject.SetActive(false);
        txtCentral.fontSize = 300;
        for (int i = timeIntro; i > 0; i--) //decompte de depart
        {
            txtCentral.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        transform.localScale = new Vector3(0, 0, 0);
        gameStarted = true;
        txtCentral.fontSize = 200;
        yield return new WaitForSeconds(timeGame); //temps du jeu
        gameFinie = true;
        btnNext.gameObject.SetActive(true);
        switch (res)
        {
            case 0: btnNext.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
                btnNext.GetComponent<Image>().sprite = spriteGreen;
                break;
            case 1:case 2 :
                btnNext.GetComponentInChildren<TextMeshProUGUI>().text = "RETRY";
                btnNext.GetComponent<Image>().sprite = spriteRed;
                break;
        }
        txtCentral.text = StringResult();
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
}
