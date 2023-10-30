using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public string m_ScoreTrigTag = "Score_trigger";
    public string m_PipeTag = "Pipe";
    public string m_GroundTag = "Ground";

    public MyGameManager gameManager;
    public Vector3 m_defaultPosition; // local

    // Start is called before the first frame update
    void Start()
    {
        // Assign the GameManager
        if (gameManager == null)
        {
            gameManager = transform.parent.GetComponentInChildren<MyGameManager>();
            Debug.LogWarning("No MyGameManager assigned!");
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it's the score trigger, increase the score
        if (collision.gameObject.CompareTag(m_ScoreTrigTag))
        {
           gameManager.IncreaseScore();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // In collision with any pipe or the ground
        if (collision.gameObject.CompareTag(m_PipeTag) || collision.gameObject.CompareTag(m_GroundTag))
        {
            //Debug.Log(collision.gameObject.name);


            // Make the collided collider and its sibling transparent to collisions
            // Unless it's the ground
            Transform parent = collision.transform.parent;
            Collider2D[] collider2Ds = parent.GetComponentsInChildren<Collider2D>();
            foreach(Collider2D c in collider2Ds) {
                if (!collision.gameObject.CompareTag(m_GroundTag))
                    c.isTrigger = true;
            }

            //gameManager.state = MyGameManager.GameStates.GameOver;
            // Call GameOver in the GameManager
            gameManager.GameOver();


        }
    }
}
