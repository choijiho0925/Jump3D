using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // �̵��ӵ�
    public float jumpForce; // ���� ��
    public float runSpeed; // �޸��� �ӵ�
    public LayerMask groundLayerMask; // ��

    private bool isRunning;
    private Vector2 movementInput; // ������ ����

    [Header("Look")]
    public float minPov; // �ּ� �þ߰�
    public float maxPov; // �ִ� �þ߰�
    public float cameraSensitivity; // ī�޶� �ΰ���
    private float currentCameraPov; // ���� ī�޶��� �þ߰�

    private Vector2 mouseDelta; // ���콺 ��

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
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���        
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
            movementInput = context.ReadValue<Vector2>(); // Ű���带 ������ �߿� InputAction���� Ű������ Vector2 �о �ҷ�����
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            movementInput = Vector2.zero; // Ű���带 ���� �� ������ ����
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // y�� * ������, �� �ִ� ���(���ڱ� �� �Ͷ߸��� ���� ��) 

            CharacterManager.Instance.Player.condition.uiCondition.stamina.Subtract(20); // ������ �� ���¹̳� 10 ����
        }
    }
    
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // InputAction���� ���콺�� Vector2 �о �ҷ�����(y���� ����, x���� �¿�)
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
        Vector3 velocity = (transform.forward * movementInput.y + transform.right * movementInput.x) * (isRunning ? runSpeed : moveSpeed); // �ӵ����� : �̵�����(y���� �յ�, x���� �¿�) * �ӵ�
        velocity.y = rb.velocity.y; // ���� Ȥ�� �߷¿� ������ ���� ����
        rb.velocity = velocity; // ���� ����
    }

    private void CameraLook()
    {
        currentCameraPov += mouseDelta.y * cameraSensitivity; // ���� ȸ�� ��(y�� * �ΰ���)
        currentCameraPov = Mathf.Clamp(currentCameraPov, minPov, maxPov); // ī�޶��� �þ߰��� �ּ�, �ִ� ������ ����

        cameraContainer.localEulerAngles = new Vector3(-currentCameraPov, 0, 0); // ���Ϸ� ȸ��, -currentCameraPov�� -�� ���̴� ������ ���콺 y���� ���� �Ҷ� �Ʒ��� ���ϰ� �ҷ���, Rotation X�� ���Ϸ� �̵�
        transform.eulerAngles += new Vector3(0, mouseDelta.x * cameraSensitivity, 0); // �¿� ȸ����, (x�� * �ΰ���) Rotation Y�� �¿�� �̵�
    }

    private void Run()
    {
        var stamina = CharacterManager.Instance.Player.condition.uiCondition.stamina;

        // ���¹̳� �Һ� ��
        if (isRunning && movementInput != Vector2.zero && IsGrounded())
        {
            if (stamina.currentValue > 0)
            {
                stamina.Subtract(10f * Time.deltaTime);
                CharacterManager.Instance.Player.condition.lastStaminaUseTime = Time.time; // �ֱ� ��� �ð� ������Ʈ
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
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down), // ���� �ٸ� 4���� ray�� ���� �ٸ� �յ��¿� 0.2 ������ ��ġ����
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down), // 0.1��ŭ ������ �Ʒ��� ray�� �߻�
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down), // 0.1��ŭ ������ ray�� �߻��ϴ� ������ �� �پ ray�� ��� �Ǹ�
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down) // �׶��忡�� ������ ray�� �߻��Ͽ� ������ �� �� �� �ֱ� �����̴�.
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, groundLayerMask)) // �׶��� ���̾ �߰��ϴ°� �ƴ� �÷��̾ ���������ν� 
            {                                                    // �÷��̾� ���̾ ������ �������� �׶��� ���̾�� ����
                return true;
            }
        }
        return false;
    }
}