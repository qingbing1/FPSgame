using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private int hp = 5; //角色血量

    public Text hpText;//关联血量UI

    private CharacterController cc;

    public float moveSpeed;//移动速度
    public float jumpSpeed;//跳跃速度

    private float horizontalMove, verticalMove;//获取按键值的变量

    private Vector3 dir;//方向

    public float gravity;//重力

    private Vector3 velocity;//y轴加速度

    public float checkRadius;//检测点的半径
    public Transform groundCheck;//检测点的中心位置
    public LayerMask groundLayer;//检测图层
    public bool isGround;//存储Physics.CheckSphere的返回值

    private AudioSource footPlayer;
    private void Start() 
    {
        cc = GetComponent<CharacterController>();
        footPlayer = GetComponent<AudioSource>();//获取声音组件
        Cursor.lockState = CursorLockMode.Locked;//锁定鼠标
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

        //存储移动方向
        dir = transform.forward * verticalMove + transform.right * horizontalMove;

        cc.Move(dir * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGround) 
        {
            velocity.y = jumpSpeed;
        }

        velocity.y -= gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        //是否按下移动键
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal != 0 || vertical != 0) && isGround)
        {
            //移动了，按下了某个方向键,并且当前没有播放音乐
            if (footPlayer.isPlaying == false)
            {
                //播放脚步声
                footPlayer.Play();
            }
        }
        else
        {
            //没有移动，不播放脚步声
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
        Debug.Log("主角血量"+hp);
        if (hp == 0)
        {
            Die();  
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;//解锁鼠标
        SceneManager.LoadScene("Die");
    }
}
