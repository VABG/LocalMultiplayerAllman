using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootballUI : MonoBehaviour
{

    [SerializeField] Text p1Score;
    [SerializeField] Text p2Score;
    [SerializeField] Text gameTime;
    [SerializeField] Text gameOverText;

    public void UpdateScores(int p1, int p2)
    {
        p1Score.text = p1.ToString();
        p2Score.text = p2.ToString();
    }

    public void SetGameOverText(string text)
    {
        gameOverText.text = text;
    }

    public void UpdateTime(int time)
    {
        int minutes = time / 60;
        int seconds = time - (minutes * 60);
        if (minutes != 0)
        {
            gameTime.text = minutes + " minute " + seconds + " seconds";
        }
        else
        {
            gameTime.text = seconds + " seconds";

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
