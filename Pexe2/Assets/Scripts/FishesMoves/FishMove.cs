using UnityEngine;

public class FishMove : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    public float speed = 6f;
    public float verticalSpeed = 4f;
    float dashCount = 100;
    bool dashBool = false;

    private float knockbackPower = 20f;
    private float knockbackDecay = 5f; 
    private Vector3 knockbackVelocity = Vector3.zero;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float yVelocity = 0f;

    Lives Vidas;
    private void Start()
    {
        Vidas = FindAnyObjectByType<Lives>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            yVelocity = verticalSpeed; 
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            yVelocity = -verticalSpeed; 
        }
        else
        {
            yVelocity = 0f;
        }

        if (dashBool)
        {
            if (dashCount < 1000) dashCount++;
            else dashBool = false;
        }
        else
        {
            if (dashCount <= 0)
            {
                dashBool = true;
                speed = 6f;
            }
            else
            {
                if(Input.GetKey(KeyCode.Q))
                {
                    dashCount-=15;
                    speed = 40f;
                }else
                {
                    if (dashCount < 1000) dashCount++;
                    speed = 6f;
                }
            }
        }

       Vector3 finalMove = Vector3.zero;


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            finalMove = moveDir.normalized * speed;
        }

        finalMove.y = yVelocity;

        if (knockbackVelocity.sqrMagnitude > 0.01f)
        {
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
            finalMove += knockbackVelocity;
        }
        else
        {
            knockbackVelocity = Vector3.zero;
        }
        

        if (finalMove.sqrMagnitude > 0.001f)
        {
            controller.Move(finalMove * Time.deltaTime);
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("BolaDoInimigo"))
        {
            Vidas.OnHitTaken();
            Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
            knockbackVelocity = new Vector3(knockbackDir.x, 0, knockbackDir.z) * knockbackPower;
        }
    }

}