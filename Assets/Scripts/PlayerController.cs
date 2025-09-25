using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody = null;   // Rigidbody2D 변수
    private float axisH = 0.0f;         // 수평 입력 값

    public float speed = 3.0f;
    public float jump = 9.0f;           // 점프력
    public LayerMask groundLayer;       // 착지할수 있는 레이어
    private bool goJump = false;        // 점프 개시 플래그
    private bool onGround = false;      // 지면에 서 있는 플래그


    private void Start()
    {
        // 스크립트가 있는 게임 오브젝트의 Rigidbody2D 컴포넌트를 가져와 rbody 변수에 할당
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        axisH = Input.GetAxis("Horizontal");    // 수평 입력 값을 axisH 변수에 저장

        if (axisH > 0.0f)   // 오른쪽 이동
        {
            Debug.Log("오른쪽 이동");
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
        else if (axisH < 0.0f)  // 왼쪽 이동
        {
            Debug.Log("왼쪽 이동");
            transform.localScale = new Vector2(-1.0f, 1.0f);        // 좌우 반전
        }

        // 캐릭터 점프하기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        goJump = true;  // 점프 개시 플래그 설정
        Debug.Log("점프눌림!");
    }

    private void FixedUpdate()
    {
        // 착지 판정
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f),
            groundLayer);

        if (onGround || axisH != 0) // 지면 위 or 속도가 0이 아님 / 속도 갱신하기
        {
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }

        if (onGround && goJump) // 지면 위에서 점프 키 눌림
        {
            Debug.Log("점프 개시!");
            Vector2 jumpPw = new Vector2(0.0f, jump); // 점프력 벡터
            // ForceMode2D.Impulse: 순간적인 힘 적용 , ForceMode2D.Force: 지속적인 힘 적용
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); // 순간적인 힘 적용
            goJump = false; // 점프 개시 플래그 해제
        }
    }
}
