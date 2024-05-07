using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private int hp = 5; //��ɫѪ��

    public Text hpText;//����Ѫ��UI

    private CharacterController cc;

    public float moveSpeed;//�ƶ��ٶ�
    public float jumpSpeed;//��Ծ�ٶ�

    private float horizontalMove, verticalMove;//��ȡ����ֵ�ı���

    private Vector3 dir;//����

    public float gravity;//����

    private Vector3 velocity;//y����ٶ�

    public float checkRadius;//����İ뾶
    public Transform groundCheck;//���������λ��
    public LayerMask groundLayer;//���ͼ��
    public bool isGround;//�洢Physics.CheckSphere�ķ���ֵ

    private AudioSource footPlayer;
    private void Start() 
    {
        cc = GetComponent<CharacterController>();
        footPlayer = GetComponent<AudioSource>();//��ȡ�������
        Cursor.lockState = CursorLockMode.Locked;//�������
    }

    private void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position,checkRadius,groundLayer);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        //�洢�ƶ�����
        dir = transform.forward * verticalMove + transform.right * horizontalMove;

        cc.Move(dir * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGround) 
        {
            velocity.y = jumpSpeed;
        }

        velocity.y -= gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        //�Ƿ����ƶ���
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal != 0 || vertical != 0) && isGround)
        {
            //�ƶ��ˣ�������ĳ�������,���ҵ�ǰû�в�������
            if (footPlayer.isPlaying == false)
            {
                //���ŽŲ���
                footPlayer.Play();
            }
        }
        else
        {
            //û���ƶ��������ŽŲ���
            footPlayer.Stop();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 8;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
         {
            moveSpeed = 5;
         }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Hp();
        }
    }

    void Hp()
    {
        hp--;
        hpText.text = hp + "";
        Debug.Log("����Ѫ��"+hp);
        if (hp == 0)
        {
            Die();  
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;//�������
        SceneManager.LoadScene("Die");
    }
}
