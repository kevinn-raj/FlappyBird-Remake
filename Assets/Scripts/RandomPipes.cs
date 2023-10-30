using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPipes : MonoBehaviour
{
    public GameObject m_PipePrefab;

    private float m_XDistance = 4f;
    private float m_HoleHeight = 2.5f;
    private float m_BottomMinY = 3.52f;
    private float m_TopMaxy = 10.9f;

    private float[] m_PipesYLimit = new float[2];
    public Transform m_FirstPosition;

    private Vector3 m_prevPosition;

    [SerializeField]
    public int ID; // Id of the random pipe generator

    
    void Awake()
    {
        // Has to be executed before anything else
        UpdateYLimit();
        ID = gameObject.GetInstanceID();
    }
    private float RandomY()
    {
        return Random.Range(m_PipesYLimit[0], m_PipesYLimit[1]) + transform.parent.position.y;
    }
    
    public void Generate(int n=20, bool resetPrevPos=true)
    {
        // One can generate another set without resetting the first position by making resetPrevPos as false
        // Place the first Pair of pipes
        if (resetPrevPos)
        {
            m_prevPosition = new Vector3(m_FirstPosition.position.x, RandomY(), m_FirstPosition.position.z); 
        }
        // Create a parent object for all the pipes if it's not yet there
        //Debug.Log(ID.ToString());
        GameObject PipeParent = GameObject.Find(ID.ToString());
        if (PipeParent == null)
        {
            PipeParent = new GameObject();
            PipeParent.name = ID.ToString();
            PipeParent.transform.position = Vector3.zero;
        }

        for (int i = 0; i < n; i++)
        {
            // Get the next position
            Vector3 nextPosition = m_prevPosition + new Vector3(m_XDistance,
                                                                0,
                                                                0);
            nextPosition.y = RandomY();

            // Instantiate
            GameObject instance = Instantiate(m_PipePrefab, nextPosition, Quaternion.identity);
            instance.transform.parent = PipeParent.transform;
            // Reset the prev with the next
            m_prevPosition = nextPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateYLimit()
    {
        // Assign the y-position limit of the generated pipes
        m_PipesYLimit[0] = m_BottomMinY + m_HoleHeight / 2;
        m_PipesYLimit[1] = m_TopMaxy - m_HoleHeight / 2;
    }

    public void DestroyPipes()
    {
        // Get parent and destroy its children
        if (GameObject.Find(Convert.ToString(ID)) != null)
        {
            GameObject PipeParent = GameObject.Find(Convert.ToString(ID));
            
            for(int i = 0; i<PipeParent.transform.childCount; i++)
            {
                Destroy(PipeParent.transform.GetChild(i).gameObject);   
            }
        }
        //else Debug.LogWarning("No PipeParent to delete");
    }
}
