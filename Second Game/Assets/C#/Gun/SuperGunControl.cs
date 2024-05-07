using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperGunControl : MonoBehaviour
{
    public GameObject SuperGun;
    public GameObject Gun;
    //关联火焰点
    public Transform FirePoint;
    //关联火焰预制体
    public GameObject FirePre;
    //关联枪声音效w
    public AudioClip clip;
    //关联换子弹音效
    public AudioClip Check;
    //关联子弹UI
    public Text BulletText;
    //子弹个数
    private int BulletNum = 10;
    //开火间隔
    private float Cd = 0.3f;
    //计时器（看是否达到开火间隔）
    private float timer = 0;

    public bool isReload = false;

    //声音播放组件
    private AudioSource gunPlayer;

    void Start()
    {
        //获取播放器组件
        gunPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cd && BulletNum > 0 && Input.GetMouseButton(0))
        {
            //重置计时器
            timer = 0;
            //创建火焰
            Instantiate(FirePre, FirePoint.position, FirePoint.rotation);
            //子弹个数减一,刷新UI
            BulletNum--;
            BulletText.text = BulletNum + "";
            //播放枪声
            gunPlayer.PlayOneShot(clip);
            //如果打完最后一发子弹，自动换子弹
            if (BulletNum == 0)
            {
                //播放动画
                GetComponent<Animator>().SetTrigger("Reload");
                //播放换子弹音效
                gunPlayer.PlayOneShot(Check);
                isReload = true;
                //1.5s后换子弹
                Invoke("ReLoad", 1.5f);
                isReload = false;
            }
            if (Input.GetMouseButtonDown(0) && isReload == false)
            {
                //摄像机射出一条射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //存储射线结果
                RaycastHit hit;
                //检测是否与物体相交
                bool isCollide = Physics.Raycast(ray, out hit);
                if (isCollide)
                {
                    MonsterControl monsterControl = hit.transform.GetComponent<MonsterControl>();
                    if (monsterControl)
                    {
                        monsterControl.Hp();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && BulletNum != 10)
        {
            //清空弹夹
            BulletNum = 0;
            //播放动画
            GetComponent<Animator>().SetTrigger("Reload");
            //播放换子弹音效
            gunPlayer.PlayOneShot(Check);
            isReload = true;
            //1.5s后换子弹
            Invoke("ReLoad", 1.5f);
            isReload = false;

        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Gun.SetActive(true);
            SuperGun.SetActive(false);
        }
    }
    void ReLoad()
    {
        //重置子弹个数
        BulletNum = 10;
        //刷新UI
        BulletText.text = BulletNum + "";
    }
}
