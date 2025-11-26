using UnityEngine;

public class InimigueMove : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    float speed = 15f;
    float verticalSpeed = 15f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float yVelocity = 0f;
    Vector3 finalMove = Vector3.zero;

    
    [Header("Configuração de Inclinação")]
    public float maxTiltAngle = 45f; 
    public float tiltSpeed = 3f;     
    private float currentTilt = 0f;
    

    int cont = 0;
    float horizontal = 0; 
    float vertical = 0;

    [HideInInspector] public bool isTargetDetected = false;
    [HideInInspector] public Transform Target;

    void Start()
    {
        
        horizontal = UnityEngine.Random.Range(-1, 2);
        vertical = UnityEngine.Random.Range(-1, 2);
    }

    void Update()
    {
        float targetTilt = 0f;

        if (isTargetDetected && Target != null)
        {

            float x = Target.position.x - transform.position.x;
            if (x > 0) x = 1; else x = -1;
            float z = Target.position.z - transform.position.z;
            if (z > 0) z = 1; else z = -1;

            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);


            float yDiff = Target.position.y - transform.position.y;

            if (yDiff > 1.0f) targetTilt = -maxTiltAngle;
            else if (yDiff < -1.0f) targetTilt = maxTiltAngle;



            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            finalMove = (Target.position - transform.position).normalized * speed;
            controller.Move(finalMove * Time.deltaTime);


            transform.rotation = Quaternion.Euler(currentTilt, angle, 0f);
        }
        else if (cont < 500)
        {


            cont++;
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            if (yVelocity > 0.1f) targetTilt = -maxTiltAngle;
            else if (yVelocity < -0.1f) targetTilt = maxTiltAngle;
            Vector3 moveCalculation = Vector3.zero;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);


                transform.rotation = Quaternion.Euler(currentTilt, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                moveCalculation = moveDir.normalized;
            }
            else
            {

                transform.rotation = Quaternion.Euler(currentTilt, transform.eulerAngles.y, 0f);
            }

            moveCalculation.y = yVelocity;

            if (moveCalculation.sqrMagnitude > 0.001f || Mathf.Abs(yVelocity) > 0.01f)
            {
                controller.Move(moveCalculation * Time.deltaTime);
            }
        }
        else if (cont >= 20)
        {
            cont = 0;
            horizontal = UnityEngine.Random.Range(-1, 2) * speed;
            vertical = UnityEngine.Random.Range(-1, 2) * speed;
            yVelocity = Random.Range(-1, 2) * verticalSpeed;
        }


        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        transform.rotation = Quaternion.Euler(currentTilt, transform.eulerAngles.y, 0f);
    }
}