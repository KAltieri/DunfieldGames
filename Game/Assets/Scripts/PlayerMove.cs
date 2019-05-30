using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float dashMoveSpeed;
    public float dashCoolDown;
    public float dashLerpSpeed;
    private float coolDownTimer;
    private float DashTimer;
    private Rigidbody2D rb;
    private Vector3 moveVelocity;
    private Vector3 savedVelocity;
    private bool isLerp;

    float lerp(float v0, float v1, float t)
    {
        return (float) (1.0 - t) * v0 + t * v1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLerp)
        {
            if (DashTimer >= dashLerpSpeed)
            {
                isLerp = false;
                DashTimer = 0.0f;
            }
            else
            {
                moveVelocity.x = lerp(moveVelocity.x, savedVelocity.x, DashTimer);
                moveVelocity.y = lerp(moveVelocity.y, savedVelocity.y, DashTimer);
                DashTimer += Time.deltaTime;
            }
        }
        else
        {
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 1.0f);
            moveVelocity = moveInput.normalized * speed;
        }
        if (Input.GetButton("Jump") && coolDownTimer <= 0) //Jump = Space Bar
        {
            coolDownTimer = dashCoolDown;
            savedVelocity = moveVelocity * dashMoveSpeed;
            isLerp = true;
            DashTimer = 0.0f;
            //moveVelocity = moveVelocity * dashMoveSpeed;
        }
        coolDownTimer -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        rb.MovePosition((Vector3)rb.position + moveVelocity * Time.fixedDeltaTime);         
    }
}
