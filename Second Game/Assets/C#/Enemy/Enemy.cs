using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //��ȡ�Զ�Ѱ·���
    private NavMeshAgent enemyAgent;
    private EnemyState childState = EnemyState.RestingState;
    private EnemyState state = EnemyState.NormalState;

    public float restTime = 2;//��Ϣʱ��
    private float restTimer = 0;//��ʱ��

    //��ȡ����
    private GameObject player;

    public float moveToPlaerDistance;//������ҵľ��룬���䷶Χ
    public float attckDistance;//��������

    private float sideLookAngle = 310f;//�޸ĵĽǶ�

    //�����ӵ���
    public Transform BulletPoint;
    //�����ӵ�Ԥ����
    public GameObject BulletPre;
    //������
    private float Cd = 1f;
    //��ʱ�������Ƿ�ﵽ��������
    private float timer = 0;

    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState
    }

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();

        //��ʼ��ֵ
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position,this.transform.position) <= moveToPlaerDistance)
        {
            state = EnemyState.FightingState;
        }else if(Vector3.Distance(player.transform.position, this.transform.position) > moveToPlaerDistance){
            state = EnemyState.NormalState;
        }

        if(state == EnemyState.NormalState)
        {
            if (childState == EnemyState.RestingState)
            {
                GetComponent<Animator>().SetBool("IsMoving",false);
                GetComponent<Animator>().SetBool("Shoot", false);
                GetComponent<Animator>().SetBool("IsRuning", false);
                restTimer += Time.deltaTime;
                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            }else if (childState == EnemyState.MovingState)
            {
                GetComponent<Animator>().SetBool("IsMoving", true);
                GetComponent<Animator>().SetBool("Shoot", false);
                GetComponent<Animator>().SetBool("IsRuning", false);
                if (enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;//��ʱ������
                    childState = EnemyState.RestingState; 
                }
            }
        }
        else if(state == EnemyState.FightingState)
        {
            GetComponent<Animator>().SetBool("IsRuning",true);
            GetComponent<Animator>().SetBool("IsMoving", false);
            GetComponent<Animator>().SetBool("Shoot", false);
            enemyAgent.SetDestination(player.transform.position);
            
            if (Vector3.Distance(player.transform.position, this.transform.position) <= attckDistance)
            {
                //�����߼�
                enemyAgent.speed = 0;
                GetComponent<Animator>().SetBool("Shoot",true);
                timer += Time.deltaTime;
                if (timer > Cd)
                {
                    //���ü�ʱ��
                    timer = 0;

                    //�����ӵ�
                    Instantiate(BulletPre, BulletPoint.position, BulletPoint.rotation);
                }
                LookAtPlayer();
                
            }
        }
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-2, 2f), 0, Random.Range(-2, 2f));
        return transform.position + randomDir.normalized * Random.Range(5, 10);
    }

    private void LookAtPlayer()
    {
        // ����ӵ��˵���ҵķ���  
        Vector3 direction = player.transform.position - transform.position;

        // ȷ��������������  
        if (direction != Vector3.zero)
        {
            // ��ת�������������  
            Quaternion rotation = Quaternion.LookRotation(direction);

            // ����һ����ʾ��Y����ת����Ԫ��  
            Quaternion sideLookRotation = Quaternion.AngleAxis(-sideLookAngle, Vector3.up);

            // ��������ת������������ 
            Quaternion finalRotation = sideLookRotation * rotation;

            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * 5f); // ƽ����ת�������5f����ת�ٶȣ����Ը�����Ҫ����  
        }
    }
}
