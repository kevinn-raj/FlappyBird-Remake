using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float m_Jump=.2f;
    public Rigidbody2D m_rb;
    public bool controllable = true;

    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigibody
        m_rb = GetComponent<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>(); 
    }
    public void Jump()
    {
        // Trigger the Jump animation
        m_Animator.SetTrigger("Jump");

        // First Zero the velocity.y 
        m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        m_rb.velocity = new Vector2(m_rb.velocity.x, 0f);

        // Freeze the rb first
        m_rb.constraints = RigidbodyConstraints2D.None;

        // Create a force then apply to the bird
        Vector2 force = new Vector2(0, m_Jump);
        m_rb.AddForce(force, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && controllable) 
            Jump(); 
    }
}
