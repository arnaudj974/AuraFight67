using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int mult = 1;
    private TextMeshProUGUI txt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        score += points * mult;
        txt.text = score.ToString();
    }
}
