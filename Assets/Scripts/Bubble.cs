using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public float screenTime=0.7f;
    public float waitTime=0.2f;
    private char inputAsked;
    public Coroutine corout;
    public TextMeshProUGUI txt;
    private Image image;
    private RectTransform rectThis;
    private RectTransform rectTxt;
    void Start()
    {
        image = GetComponent<Image>();
        rectThis = GetComponent<RectTransform>();
        rectTxt = txt.GetComponent<RectTransform>();
        image.enabled = false;
    }

    void Update()
    {
        
    }

    public char GetInputAsked()
    {
        return inputAsked;
    }

    private void Show()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                inputAsked = 'w';
                txt.text = "Z";
                break;
            case 2:
                inputAsked = 'a';
                txt.text = "Q";
                break;
            case 3:
                inputAsked = 's';
                txt.text = "S";
                break;
            case 4:
                inputAsked = 'd';
                txt.text = "D";
                break;
            default:
                inputAsked = ' ';
                break;
        }
        PositionRandom();
        image.enabled = true;
    }

    private void Hide()
    {
        txt.text = "";
        inputAsked = ' ';
        image.enabled = false;
    }

    public void StartBulle(int timeIntro)
    {
        corout = StartCoroutine(SpawnBulle(timeIntro));
    }

    public void StopBulle()
    {
        StopCoroutine(corout);
    }

    public IEnumerator SpawnBulle(int timeIntro)
    {
        yield return new WaitForSeconds(timeIntro);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Show();
            yield return new WaitForSeconds(screenTime);
            Hide();
        }
    }

    public void Clear()
    {
        image.enabled = false;
        txt.text = "";
    }

    public void PositionRandom()
    {
        Vector3 v = new Vector3(Random.Range(-130f, 130f), Random.Range(-200f, 200f), 0);
        rectThis.anchoredPosition = v;
        rectTxt.anchoredPosition = v;
        txt.color= Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
