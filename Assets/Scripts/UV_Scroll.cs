using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UV_Scroll : MonoBehaviour
{
    [Tooltip("1/(Scale X) * Velocity.x * Tilling")]
    public float m_U_velocity = 0.1f;
    public Renderer m_Renderer;
    public bool m_isScrolling = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isScrolling)
        {
            // Scroll on U axis of the UV
            m_Renderer.material.mainTextureOffset += new Vector2(m_U_velocity * Time.deltaTime, 0);
        }
    }
}
