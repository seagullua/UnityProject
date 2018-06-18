using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public float speed = 1;
    bool isGrounded;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    Rigidbody2D myBody = null;
    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //        Перевіряємо        чипроходитьлініячерезColliderзшаромGround
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //        Намалюватилінію(длярозробника)
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {            //            Якщо            кнопку        ще        тримають    
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false; this.JumpTime = 0;
            }
        }

        float value = Input.GetAxis("Horizontal");
        Animator animator = GetComponent<Animator>();

        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
            Vector2 vel = myBody.velocity;
            vel.x = value * speed; myBody.velocity = vel;
        }
        else
        {
            animator.SetBool("run", false);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }


    }

}
