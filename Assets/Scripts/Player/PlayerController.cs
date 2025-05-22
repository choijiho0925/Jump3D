using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // 이동속도
    public float jumpForce; // 점프 힘
    public float runSpeed; // 달리기 속도
    public LayerMask groundLayerMask; // 땅

    private bool isRunning;
    private Vector2 movementInput; // 움직임 방향

    [Header("Look")]
    public float minPov; // 최소 시야각
    public float maxPov; // 최대 시야각
    public float cameraSensitivity; // 카메라 민감도
    private float currentCameraPov; // 현재 카메라의 시야각

    private Vector2 mouseDelta; // 마우스 값

    public Transform cameraContainer;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 잠금        
    }

    private void FixedUpdate()
    {
        if (IsGrounded()) 
        {
            Move(); 
            Run();
        }
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            movementInput = context.ReadValue<Vector2>(); // 키보드를 누르는 중에 InputAction에서 키보드의 Vector2 읽어서 불러오기
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            movementInput = Vector2.zero; // 키보드를 땠을 때 움직임 정지
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // y축 * 점프힘, 힘 주는 방식(갑자기 펑 터뜨릴때 자주 씀) 

            CharacterManager.Instance.Player.condition.uiCondition.stamina.Subtract(20); // 점프할 때 스태미나 10 감소
        }
    }
    
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // InputAction에서 마우스의 Vector2 읽어서 불러오기(y축은 상하, x축은 좌우)
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isRunning = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRunning = false;
        }
    }

    private void Move()
    {
        Vector3 velocity = (transform.forward * movementInput.y + transform.right * movementInput.x) * (isRunning ? runSpeed : moveSpeed); // 속도계산식 : 이동방향(y축은 앞뒤, x축은 좌우) * 속도
        velocity.y = rb.velocity.y; // 점프 혹은 중력에 영향을 받지 않음
        rb.velocity = velocity; // 최종 적용
    }

    private void CameraLook()
    {
        currentCameraPov += mouseDelta.y * cameraSensitivity; // 상하 회전 값(y값 * 민감도)
        currentCameraPov = Mathf.Clamp(currentCameraPov, minPov, maxPov); // 카메라의 시야각을 최소, 최대 값으로 제한

        cameraContainer.localEulerAngles = new Vector3(-currentCameraPov, 0, 0); // 상하로 회전, -currentCameraPov에 -를 붙이는 이유는 마우스 y축을 위로 할때 아래로 향하게 할려고, Rotation X는 상하로 이동
        transform.eulerAngles += new Vector3(0, mouseDelta.x * cameraSensitivity, 0); // 좌우 회전값, (x값 * 민감도) Rotation Y는 좌우로 이동
    }

    private void Run()
    {
        var stamina = CharacterManager.Instance.Player.condition.uiCondition.stamina;

        // 스태미나 소비 중
        if (isRunning && movementInput != Vector2.zero && IsGrounded())
        {
            if (stamina.currentValue > 0)
            {
                stamina.Subtract(10f * Time.deltaTime);
                CharacterManager.Instance.Player.condition.lastStaminaUseTime = Time.time; // 최근 사용 시간 업데이트
            }
            else
            {
                isRunning = false;
            }
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down), // 서로 다른 4개의 ray를 각기 다른 앞뒤좌우 0.2 간격의 위치에서
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down), // 0.1만큼 위에서 아래로 ray를 발사
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down), // 0.1만큼 위에서 ray를 발사하는 이유는 딱 붙어서 ray를 쏘게 되면
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down) // 그라운드에서 안으로 ray를 발사하여 인지를 못 할 수 있기 때문이다.
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, groundLayerMask)) // 그라운드 레이어를 추가하는게 아닌 플레이어만 제외함으로써 
            {                                                    // 플레이어 레이어를 제외한 나머지를 그라운드 레이어로 인지
                return true;
            }
        }
        return false;
    }
}