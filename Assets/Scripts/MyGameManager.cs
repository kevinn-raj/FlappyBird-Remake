using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private Vector3 envInitPos;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int HiScore = 0;
    
    [SerializeField]
    private GameObject env;

    [SerializeField]
    private Bird bird;

    [HideInInspector]
    public UI ui;

    public bool skipGameOverLobby = false;
    public bool autoGeneratePipes = false;
    public enum GameStates
    {
       Playing, GameOver, WaitingToPlay
    }
    public GameStates state = GameStates.WaitingToPlay;

    public int nPipes = 20;
    // Start is called before the first frame update
    void Start()
    {
        if (env != null)
            envInitPos = env.transform.position;
        else
            Debug.LogError("Environment object not assigned.");

        if(bird == null)
        {
            // Get the Sibling bird corresponding to the Gamemanager
            bird = transform.parent.GetComponentInChildren<Bird>();
        }
        
        // Load the lobby first
        Lobby();
        if(skipGameOverLobby)
        {
            Play();
        }
    }
    public void Lobby()
    {
        state = GameStates.WaitingToPlay;
        // Resets some properties
        Restart();
    }

    public void Play()
    {
        // Modify the state of the game
        state = GameStates.Playing;

        // Start the Scrolling of the Environment object
        env.GetComponent<Scroll>().m_isMoving = true;

        // Enable bird's RigidBody
        bird.GetComponent<Rigidbody2D>().simulated = true;

        // Generate the Pipes
        if(env.GetComponentInChildren<RandomPipes>() != null) 
            env.GetComponentInChildren<RandomPipes>().Generate(n:nPipes);
    }
    public void GameOver()
    {
        // Modify the state of the game
        state = GameStates.GameOver;

        // Stop the Scrolling of the Environment object
        env.GetComponent<Scroll>().m_isMoving = false;

        // Retrieve the background and the ground UV_Scroll component
        UV_Scroll bg = env.transform.Find("Background").GetComponent<UV_Scroll>();
        UV_Scroll ground = env.transform.Find("Ground").GetComponent<UV_Scroll>();
        bg.m_isScrolling = false;
        ground.m_isScrolling = false;

        // Locks Bird's x position
        bird.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

        // Disable the Bird's Controller
        bird.GetComponent<Controller>().enabled = false;
    
    }
    public void Restart()
    {

        // Unlock Bird's x position
        bird.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        // Enable the Bird's Controller
        bird.GetComponent<Controller>().enabled = true;
        // Reset the bird's position
        bird.transform.localPosition = bird.m_defaultPosition;
        // Disable bird's RigidBody
        bird.GetComponent<Rigidbody2D>().simulated = false;

        // Retrieve the background and the ground UV_Scroll component
        // Activate Scrolling
        UV_Scroll bg = env.transform.Find("Background").GetComponent<UV_Scroll>();
        UV_Scroll ground = env.transform.Find("Ground").GetComponent<UV_Scroll>();
        bg.m_isScrolling = true;
        ground.m_isScrolling = true;

        // Reset the score
        SetScore(0);
        // Reset the Environment object
        env.transform.position = envInitPos;
        // Deactivate the movement of the environment
        env.GetComponent<Scroll>().m_isMoving = false;
        // Delete the pipes
        if (env.GetComponentInChildren<RandomPipes>() != null)
            env.GetComponentInChildren<RandomPipes>().DestroyPipes();
        else
            Debug.LogWarning("No Random pipes generator");
    }

    // if the pipes are few enough, generate new ones
    public void AutoGeneratePipes() 
    {
        if (autoGeneratePipes && env.GetComponentInChildren<RandomPipes>() != null)
        {
            env.GetComponentInChildren<RandomPipes>().Generate(n: nPipes, resetPrevPos: false);
        }
    }
    private void SetUIScore()
    {
        // Set the UI score text,
        if (ui != null)
        {
            ui.score.text = Convert.ToString(score);
        }
    }
    public int IncreaseScore()
    {
        SetScore(score + 1);

        // High score mecanics
        HiScore = score > HiScore ? score : HiScore;
        
        return score;
    }
    public int GetScore()
    {
        return score;
    }

    public void SetScore(int theScore)
    {
        score = theScore;
        SetUIScore();
    }

    public int GetHiScore()
    {
        return HiScore;
    }

    // Update is called once per frame
    void Update()
    {

        if (skipGameOverLobby)
        {
            if (state == GameStates.GameOver)
            {
                Restart();
                Play();
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            // Go to the Lobby
            if (state == GameStates.GameOver)
            {
                // Make the idle animation play
                bird.GetComponentInChildren<Animator>().SetTrigger("Idle");

                Lobby();
            }// If already there then play
            else if (state == GameStates.WaitingToPlay)
            {
                state = GameStates.Playing;
                Play();
            }
        }
    }
}
