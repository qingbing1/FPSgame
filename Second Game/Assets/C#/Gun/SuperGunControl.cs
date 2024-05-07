using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperGunControl : MonoBehaviour
{
    public GameObject SuperGun;
    public GameObject Gun;
    //���������
    public Transform FirePoint;
    //��������Ԥ����
    public GameObject FirePre;
    //����ǹ����Чw
    public AudioClip clip;
    //�������ӵ���Ч
    public AudioClip Check;
    //�����ӵ�UI
    public Text BulletText;
    //�ӵ�����
    private int BulletNum = 10;
    //������
    private float Cd = 0.3f;
    //��ʱ�������Ƿ�ﵽ��������
    private float timer = 0;

    public bool isReload = false;

    //�����������
    private AudioSource gunPlayer;

    void Start()
    {
        //��ȡ���������
        gunPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cd && BulletNum > 0 && Input.GetMouseButton(0))
        {
            //���ü�ʱ��
            timer = 0;
            //��������
            Instantiate(FirePre, FirePoint.position, FirePoint.rotation);
            //�ӵ�������һ,ˢ��UI
            BulletNum--;
            BulletText.text = BulletNum + "";
            //����ǹ��
            gunPlayer.PlayOneShot(clip);
            //����������һ���ӵ����Զ����ӵ�
            if (BulletNum == 0)
            {
                //���Ŷ���
                GetComponent<Animator>().SetTrigger("Reload");
                //���Ż��ӵ���Ч
                gunPlayer.PlayOneShot(Check);
                isReload = true;
                //1.5s���ӵ�
                Invoke("ReLoad", 1.5f);
                isReload = false;
            }
            if (Input.GetMouseButtonDown(0) && isReload == false)
            {
                //��������һ������
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //�洢���߽��
                RaycastHit hit;
                //����Ƿ��������ཻ
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
            //��յ���
            BulletNum = 0;
            //���Ŷ���
            GetComponent<Animator>().SetTrigger("Reload");
            //���Ż��ӵ���Ч
            gunPlayer.PlayOneShot(Check);
            isReload = true;
            //1.5s���ӵ�
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
        //�����ӵ�����
        BulletNum = 10;
        //ˢ��UI
        BulletText.text = BulletNum + "";
    }
}
