using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*    public GameObject enemyPrefeb;
        public GameObject enemyPrefeb_red;
        public GameObject enemyPrefeb_blue;


        //private Transform target;

        private float timeAfterSpawn;

        private void Start()
        {
            timeAfterSpawn = 0f;
            //target = FindObjectOfType<Player>().transform;

        }
        private void Update()
        {
            timeAfterSpawn += Time.deltaTime;

            if(timeAfterSpawn>2f)
            {
                timeAfterSpawn = 0f;


                GameObject enemy = Instantiate(enemyPrefeb);
                GameObject enemy_red = Instantiate(enemyPrefeb_red);
                GameObject enemy_blue = Instantiate(enemyPrefeb_blue);
                enemy.transform.position = new Vector2(5, Random.Range(-1.0f, 1.5f));
                enemy_red.transform.position = new Vector2(5, Random.Range(-1.0f, 1.5f));
                enemy_blue.transform.position = new Vector2(5, Random.Range(-1.0f, 1.5f));
                Destroy(enemy, 4.0f);
                Destroy(enemy_red, 4.0f);
                Destroy(enemy_blue, 4.0f);
            }
        }
    */ // 내가 쓴 것 닷지 따라서~

    public GameObject[] enemyPrefebs = null;    // 생성할 적 새의 프리펩을 저장할 배열
    public float spawnStartDelay = 2.0f;        // 시작
    public float spawnInterval = 1.0f;          //

    private const int MAX_SPACE_COUNT = 6;
    private const float SPACE_HEIGHT = 0.4f;
    private const float LIFETIME = 5.0f;

    // 최초의 update 실행 직전에 한번 호출
    private void Start()
    {
        StartCoroutine(Spawn());              // Spawn함수를 코루틴으로 실행
    }

    // 조건 추가 : 6칸을 랜덤으로 ON/OFF 결정. 0-4 번을 랜덤으로 선택하고 선택된 칸과 그 다음 칸은 OFF 로 설정. on으로 생성된 부분만 새를 생성.
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnStartDelay);  // 실행했을 때 spawnStartDelay만큼 우선 대기
        while (true)
        {
            //bool[] flags = GetFlagsBoolType();
            int flags = GetFlags();
            int singleFlag = 1;
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                if((flags&singleFlag)!=0)
                {
                    EnemyGenerate(i);
                }
                singleFlag <<= 1;

            }
            yield return new WaitForSeconds(spawnInterval);

        }


    }
    private int GetFlags()
    {
        int flags = 0; //리턴용 변수
        while (flags == 0) 
        {
            int random = (int)(Random.value * 64.0f); // 화이트 보드 1번 64 이상 아무 수
            random &= ((1<<MAX_SPACE_COUNT)-1); //random $ 111111
            int mask = 0b_0011;
            mask = mask << Random.Range(0, MAX_SPACE_COUNT - 1);

            mask = ~mask;
            flags = random & mask;            
        } 
        
        return flags;
    }

    private static void GetFlagsBoolType() // static void-> bool[]??
    {
        bool result = false;
        bool[] flags = new bool[MAX_SPACE_COUNT];
        while (result == false)
        {
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                if (Random.Range(0, 2) == 1)
                    flags[i] = true;
            }

            int index = Random.Range(0, MAX_SPACE_COUNT - 1);

            flags[index] = false;
            flags[index + 1] = false;
            for (int i = 0; i < MAX_SPACE_COUNT; i++) // 모두 false 인 경우 방지를 위해 검사
            {
                if (flags[i] == true)
                {
                    result = true;
                    break;
                }
            }
        }
    }

    private void EnemyGenerate(int index) // 적 생성
    {
        int enemyIndex = Random.Range(0, enemyPrefebs.Length);  // 어떤 종류의 적을 생성할지 결정
        GameObject enemy = GameObject.Instantiate(enemyPrefebs[enemyIndex], this.transform);
        //int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        enemy.transform.Translate(Vector2.down * index * SPACE_HEIGHT);
        Destroy(enemy, LIFETIME);
    }
}
