using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class BirdAgent : Agent
{
    public Bird bird;

    [Description("Early end the episode after some condition")]
    public bool EarlyEndEpisode = true;
    [Description("Max score before early ending the episode")]
    public int MaxScore = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(!bird) 
            bird = GetComponent<Bird>();
        else
        {   
            // Disbable control when the AI plays
            bird.GetComponent<Controller>().controllable = false;
        }
    }

    public override void  OnEpisodeBegin()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EarlyEndEpisode && bird.gameManager.GetScore() >= MaxScore)
        {
            bird.gameManager.GameOver();
            EndEpisode(); 
        }

        AddReward(1f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it's the score trigger, increase the score
        if (collision.gameObject.CompareTag(bird.m_ScoreTrigTag))
        {
            AddReward(1);
            //Debug.Log(GetCumulativeReward());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // In collision with any pipe or the ground
        if (collision.gameObject.CompareTag(bird.m_PipeTag) || collision.gameObject.CompareTag(bird.m_GroundTag))
        {
            AddReward(-1);
            EndEpisode();
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var act = actionBuffers.DiscreteActions;

        // Action 0 = 1 : Nothing, 0 : Jump
        if (act[0] == 0)
        {
            bird.GetComponent<Controller>().Jump();
        }
    }

    //  Controlled by the user
    public override void Heuristic(in ActionBuffers actionsOut)
    {

        //// Get the discrete action
        //var act = actionsOut.DiscreteActions;

        //// Action = 0 : Nothing, 1 : Jump
        //act[0] = Convert.ToInt16(Input.GetButton("Jump"));
    }
}
