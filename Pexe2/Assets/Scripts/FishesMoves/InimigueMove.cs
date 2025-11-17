using UnityEngine;

public class InimigueMove : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    public float speed = 6f;
    public float verticalSpeed = 4f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float yVelocity = 0f;
    Vector3 finalMove = Vector3.zero;

    int cont = 0;
    float horizontal = UnityEngine.Random.Range(-1, 2);
    float vertical = UnityEngine.Random.Range(-1, 2);

    [HideInInspector] public bool isTargetDetected = false;
    [HideInInspector] public Transform Target;

    void Update()
    {
        if(isTargetDetected)
        {
            float x = Target.position.x - transform.position.x;
            if (x > 0) x = 1; else x = -1;
            float z = Target.position.z - transform.position.z;
            if (z > 0) z = 1; else z = -1;

            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            finalMove = Target.position - transform.position;
            controller.Move(finalMove * Time.deltaTime);
        }
        else if (cont < 500)
        {
            cont++;
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            Vector3 finalMove = Vector3.zero;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                finalMove = moveDir.normalized * speed;
            }

            finalMove.y = yVelocity;

            if (finalMove.sqrMagnitude > 0.001f)
            {
                controller.Move(finalMove * Time.deltaTime);
            }
        }
        else if (cont >= 20)
        {
            cont = 0;
            horizontal = UnityEngine.Random.Range(-1, 2);
            vertical = UnityEngine.Random.Range(-1, 2);
            yVelocity = Random.Range(-1, 2) * verticalSpeed;
        }
    }
}