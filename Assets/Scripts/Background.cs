using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Sprite backSchool;
    public Sprite backChurch;
    public Sprite backAutomn;
    public Sprite backWinter;
    public Image imgBack;

    void Start()
    {
        if(Mode.Campagne == GameManager.Instance.mode)
        {
            ChangeBackground(GameManager.Instance.lvl);
        }
        else
        {
            ChangeBackground(Random.Range(1, 5));
        }
    }
    public void ChangeBackground(int lvl)
    {
        switch (lvl)
        {
            case 1: imgBack.sprite = backSchool; break;
            case 2: imgBack.sprite = backAutomn; break;
            case 3: imgBack.sprite = backWinter; break;
            case 4: imgBack.sprite = backChurch; break;
        }
    }
}
