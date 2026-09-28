using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleJ2 : MonoBehaviour
{
    private string inputAsked;
    public Coroutine corout;
    public Image arrow;
    public Bubble bulle;
    public Image image;
    private RectTransform rectThis;
    private RectTransform rectArrow;
    void Start()
    {

        rectThis = GetComponent<RectTransform>();
        rectArrow = arrow.GetComponent<RectTransform>();
        image.enabled = false;
        arrow.enabled = false;
    }

    void Update()
    {

    }

    public string GetInputAsked()
    {
        return inputAsked;
    }

    private void Show()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                inputAsked = "up";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                inputAsked = "left";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                inputAsked = "down";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 4:
                inputAsked = "right";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                inputAsked = "";
                break;
        }
        image.enabled = true;
        arrow.enabled = true;
    }

    private void Hide()
    {
        arrow.enabled = false;
        inputAsked = "";
        image.enabled = false;
    }

    public void StartBulle(int timeIntro, float waitTime, float screenTime)
    {
        corout = StartCoroutine(SpawnBulle(timeIntro, waitTime, screenTime));
    }

    public void StopBulle()
    {
        StopCoroutine(corout);
    }

    public IEnumerator SpawnBulle(int timeIntro,float waitTime,float screenTime)
    {
        yield return new WaitForSeconds(timeIntro);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Show();
            PositionRandom();
            yield return new WaitForSeconds(screenTime);
            Hide();
        }
    }

    public void Clear()
    {
        image.enabled = false;
        arrow.enabled = false;
        inputAsked = "";
    }

    public void PositionRandom()
    {
        Vector3 v;
        do
        {
            v = new Vector3(Random.Range(-130f, 130f), Random.Range(-200f, 200f), 0);
        } while (Vector3.Distance(v, bulle.GetComponent<RectTransform>().anchoredPosition) < 200f);
        rectThis.anchoredPosition = v;
        rectArrow.anchoredPosition = v;
        arrow.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
