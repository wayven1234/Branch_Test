using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody = null;   // Rigidbody2D ����
    private float axisH = 0.0f;         // ���� �Է� ��

    public float speed = 3.0f;
    public float jump = 9.0f;           // ������
    public LayerMask groundLayer;       // �����Ҽ� �ִ� ���̾�
    private bool goJump = false;        // ���� ���� �÷���
    private bool onGround = false;      // ���鿡 �� �ִ� �÷���


    private void Start()
    {
        // ��ũ��Ʈ�� �ִ� ���� ������Ʈ�� Rigidbody2D ������Ʈ�� ������ rbody ������ �Ҵ�
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        axisH = Input.GetAxis("Horizontal");    // ���� �Է� ���� axisH ������ ����

        if (axisH > 0.0f)   // ������ �̵�
        {
            Debug.Log("������ �̵�");
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
        else if (axisH < 0.0f)  // ���� �̵�
        {
            Debug.Log("���� �̵�");
            transform.localScale = new Vector2(-1.0f, 1.0f);        // �¿� ����
        }

        // ĳ���� �����ϱ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        goJump = true;  // ���� ���� �÷��� ����
        Debug.Log("��������!");
    }

    private void FixedUpdate()
    {
        // ���� ����
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f),
            groundLayer);

        if (onGround || axisH != 0) // ���� �� or �ӵ��� 0�� �ƴ� / �ӵ� �����ϱ�
        {
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }

        if (onGround && goJump) // ���� ������ ���� Ű ����
        {
            Debug.Log("���� ����!");
            Vector2 jumpPw = new Vector2(0.0f, jump); // ������ ����
            // ForceMode2D.Impulse: �������� �� ���� , ForceMode2D.Force: �������� �� ����
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); // �������� �� ����
            goJump = false; // ���� ���� �÷��� ����
        }
    }
}
