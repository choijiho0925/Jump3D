using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce; // 점프 힘
    public float forwardMultiplier; // 밀어주는 힘

    public LayerMask playerLayer;

    private Rigidbody rb;

    private void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) == 0) return;
        
        //rb = collision.attachedRigidbody; // 방법 1 플레이어 rigidbody 가져오기

        rb = collision.GetComponent<Rigidbody>(); //  방법 2 플레이어 rigidbody 가져오기

        Vector3 horizontalDir = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized; // 현재 속도에서 수평 방향 추출
        Vector3 jumpBoost = (horizontalDir * forwardMultiplier) + (Vector3.up * jumpForce); // 점프 힘 계산 

        rb.AddForce(jumpBoost, ForceMode.VelocityChange); // 기존 속도에 추가 힘 적용 (VelocityChange는 즉시 속도 변화를 만듬)
    }
}