using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce; // ���� ��
    public float forwardMultiplier; // �о��ִ� ��

    public LayerMask playerLayer;

    private Rigidbody rb;

    private void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) == 0) return;
        
        //rb = collision.attachedRigidbody; // ��� 1 �÷��̾� rigidbody ��������

        rb = collision.GetComponent<Rigidbody>(); //  ��� 2 �÷��̾� rigidbody ��������

        Vector3 horizontalDir = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized; // ���� �ӵ����� ���� ���� ����
        Vector3 jumpBoost = (horizontalDir * forwardMultiplier) + (Vector3.up * jumpForce); // ���� �� ��� 

        rb.AddForce(jumpBoost, ForceMode.VelocityChange); // ���� �ӵ��� �߰� �� ���� (VelocityChange�� ��� �ӵ� ��ȭ�� ����)
    }
}