using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootballManager : MonoBehaviour
{
    int secondsCounter = 0;
    int secondsCounterLast = 0;

    [SerializeField] float gameTime = 90;
    float gameTimer = 0;
    int team1Score = 0;
    int team2Score = 0;
    bool justScored = false;
    bool gameOver = false;

    [SerializeField] float breakAfterScore = 2.0f;
    [SerializeField] FootballUI ui;
    float timer = 0;

    [SerializeField] AudioSource goalSound;
    [SerializeField] AudioSource respawnSound;
    
    [SerializeField] ParticleSystem respawnPFX;
    [SerializeField] ParticleSystem goalPFX;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Update gameover state
        if (gameOver)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                SceneManager.LoadScene(0);
            }
            return;
        }
        gameTimer -= Time.deltaTime;
        
        // Update UI Timer
        secondsCounter = (int)gameTimer;
        if (secondsCounter != secondsCounterLast)
        {
            secondsCounterLast = secondsCounter;
            ui.UpdateTime(secondsCounter);
        }

        // End game
        if (gameTimer <= 0)
        {
            EndGame();
        }

        // Update scored state
        if (justScored)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Reset ball position
                justScored = false;
                timer = breakAfterScore;

                Ball[] balls = FindObjectsOfType<Ball>();
                for (int i = 0; i < balls.Length; i++)
                {
                    balls[i].transform.position = new Vector3(0, .5f + i, 0);
                    balls[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                respawnPFX.Play();
                respawnSound.Play();

                // Find players and reset
                Player[] players = FindObjectsOfType<Player>();
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].Reset();
                }
                FindObjectOfType<DoorActivator>().Reset();
            }
        }
    }

    private void EndGame()
    {
        gameOver = true;
        gameTimer = 10.0f;

        // Write game over message in UI
        if (team1Score == team2Score) ui.SetGameOverText("Draw!");
        else if (team1Score > team2Score) ui.SetGameOverText("Player 1 Won!");
        else if (team2Score > team1Score) ui.SetGameOverText("Player 2 Won!");

        // Find and disable player scripts
        Player[] players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].enabled = false;
        }
    }

    public void AddScore(int team, GameObject ball = null)
    {
        // Stops scoring from happening again before ball respawns
        if (justScored) return;
        justScored = true;

        // Add score
        if (team == 1) team1Score++;
        else if (team == 2) team2Score++;
        else
        {
            Debug.Log("Then who was score!?"); 
        }
        
        ui.UpdateScores(team1Score, team2Score);
        
        timer = breakAfterScore;

        // Play sound and particle effects
        goalSound.Play();
        goalPFX.Play();

        if (ball == null) return;
        ball.GetComponent<Rigidbody>().velocity *= .25f; // Lower speed
    }
}
