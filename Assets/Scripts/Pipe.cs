using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public string m_DestroyerTag = "Destroyer";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the pipes are few enough, generate new ones
        if(transform.parent.parent.childCount <= 5)
        {
            // name of the parent of the pair of pipes (which is the InstanceID of the RandomPipe that generated them)
            string PipesParentName = transform.parent.parent.name;

            // the generator as a GameObject from its InstanceID
            GameObject pipesGen = FindObjectByInstanceID(Convert.ToInt32(PipesParentName)); 

            // the GameManager corresponding to it
            MyGameManager gm = pipesGen.transform.parent.GetComponentInChildren<MyGameManager>();
            if (gm != null)
            {
                gm.AutoGeneratePipes();
            }
        }
        // Delete the parent of the Pipe when hits the destroyer
        if (collision.CompareTag(m_DestroyerTag))
        {
            Destroy(transform.parent.gameObject);
        }
    }


    // Method to find a GameObject by its instance ID
    public GameObject FindObjectByInstanceID(int instanceID)
    {
        UnityEngine.Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject)); // Get all GameObjects in the scene

        foreach (var obj in objects)
        {
            GameObject gameObject = obj as GameObject;
            if (gameObject != null && gameObject.GetInstanceID() == instanceID)
            {
                // Found the GameObject with the specified instance ID
                return gameObject;
            }
        }

        // If no object is found with the specified instance ID
        return null;
    }
}
