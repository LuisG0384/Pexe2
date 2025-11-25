using UnityEngine;
using UnityEngine.UI; 

public class FishMove : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    [Header("UI do Boost (Arraste aqui)")]
    public Slider sliderBoost;    
    public Image fillImage;       
    public Color corNormal = Color.cyan;  
    public Color corExausto = Color.red; 

    [Header("Configuração de Movimento")]
    public float speed = 6f;
    public float verticalSpeed = 4f;

    [Header("Configuração de Inclinação")]
    public float maxTiltAngle = 45f;
    public float tiltSpeed = 5f;
    private float currentTilt = 0f;

    
    float dashCount = 1000; 
    bool dashBool = false; 

    private float knockbackPower = 20f;
    private float knockbackDecay = 5f;
    private Vector3 knockbackVelocity = Vector3.zero;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float yVelocity = 0f;

    Lives Vidas;
    SpawnManagerScript manager;

    private void Awake()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag("SpawnerManager");
        if (managerObj != null) manager = managerObj.GetComponent<SpawnManagerScript>();
    }

    private void Start()
    {
        Vidas = FindAnyObjectByType<Lives>();

        
        if (sliderBoost != null)
        {
            sliderBoost.maxValue = 1000;
            sliderBoost.value = dashCount;
        }
    }

    void Update()
    {
        
        if (sliderBoost != null)
        {
            sliderBoost.value = dashCount;

            
            if (fillImage != null)
            {
                if (dashBool) fillImage.color = corExausto;
                else fillImage.color = corNormal;
            }
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float targetTilt = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            yVelocity = verticalSpeed;
            targetTilt = -maxTiltAngle;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            yVelocity = -verticalSpeed;
            targetTilt = maxTiltAngle;
        }
        else
        {
            yVelocity = 0f;
            targetTilt = 0f;
        }

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        
        if (dashBool) 
        {
            if (dashCount < 1000)
            {
                dashCount += 2; //Recuperação de boost
            }
            else
            {
                dashBool = false; 
            }
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
                if (Input.GetKey(KeyCode.Q)) 
                {
                    dashCount -= 15;
                    speed = 40f;
                }
                else 
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

            transform.rotation = Quaternion.Euler(currentTilt, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            finalMove = moveDir.normalized * speed;
        }
        else
        {
            transform.rotation = Quaternion.Euler(currentTilt, transform.eulerAngles.y, 0f);
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

        if (finalMove.sqrMagnitude > 0.001f || Mathf.Abs(yVelocity) > 0.01f)
        {
            controller.Move(finalMove * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BolaDoInimigo"))
        {
            if (Vidas != null) Vidas.OnHitTaken();

            Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
            knockbackVelocity = new Vector3(knockbackDir.x, 0, knockbackDir.z) * knockbackPower;

            if (manager != null) manager.Diminui();
        }
    }
}