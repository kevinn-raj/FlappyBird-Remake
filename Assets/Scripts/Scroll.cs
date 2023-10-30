using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float m_velocity = 1f;
    public bool m_isMoving = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Offset the object
        if (m_isMoving)
        {
            transform.position += new Vector3(m_velocity*Time.deltaTime, 0, 0);
        }
    }
}
