using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private const int DEFAULT_POOL_SIZE = 16;
    private const int RANDOM_INDEX = -1;
    private static EnemyPool instance=null;
    private int[] queueSize = null; //세로 생성 되는 적 이름에 붙인 인덱스 번호 + 해당 큐의 현재 크기

    // 생성할 적의 종류
   public GameObject [] enemyPrefebs=null;// 갯수는 유니티에서 변경 가능
   private Queue<GameObject>[] pools = null; // 적 종류별로 만든 Queue를 저장할 배열

    // 대량으로 생성 해 놓은 메모리 풀

    public static EnemyPool Inst
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance=this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject); //다른 씬이 로드 되더라도 삭제 ㅚ지 않는다,
        }
        else
        {
            // 이미 인스턴스가 만들어진게 있다.
            if(instance!=this) // 이미 만들어진 것이 나와 다르다.ㄴ
            {
                Destroy(this.gameObject);
        
            }
        }
    }



// 초기화 (pool 변수를 채운다)
  public  void Initialize()
  {
      // pool 변수를 채운다
      // 예시 ) (한 페이지 최대 16마리 )*(3종류) = 48개 = instance를 48번 한다.

      pools = new Queue<GameObject>[enemyPrefebs.Length];//3 : 적종류의 수만큼 배열 크기 확정
      queueSize = new int[enemyPrefebs.Length]; // 정수 3개 담는 배열. 각 풀의 크기 초기화

      for(int i=0; i< enemyPrefebs.Length; i++)
      {
        //  pools[i] = new Queue<GameObject>(DEFAULT_POOL_SIZE);  // 큐를 하나 생성 -> DEFAULT_POOL_SIZE 얘 안써도 되지만 알고 있으니까 쓰는게 좋음
        //  for(int j=0; j < DEFAULT_POOL_SIZE ;j++) //종류별로 16개씩 만들겠다
        // {
        //      GameObject obj = GameObject.Instantiate(enemyPrefebs[i],this.transform); //생성하고 Enemypool 의 자식으로 추가
        //      obj.name = $"{enemyPrefebs[i].name}_{queueSize[i]}"; // 안하면 이름이 다 똑같음 그냥 알아보기 편하라고 이름을 변경해줌
        //      obj.SetActive(false); // 비활성화 상태로 변경해줌 -> 아름 회색으로 뜸
        //      pools[i].Enqueue(obj); // 큐에 생성한 오브잭트 삽입
        //     //pools[i].Enqueue(GameObject.instantiate(enemyPrefebs[i],this.transform));
        //      queueSize[i]++;
        //  }

        PoolExpand(i,DEFAULT_POOL_SIZE,true);
      }
  }
  // 풀에서 오브젝트를 하나 가져오는 함수 (index는 생성할 종류, 기본적으로는 랜덤)
  public GameObject GetEnemy(int index =RANDOM_INDEX )// 디폴트값 있음
  {
      GameObject result =null; //리턴용 변수
      int target = index;      // 타겟에 실제 생성할 종류 설정


    // 랜덤 생성되는 경우
      //인덱스가 랜덤인덱스(-1)이면 랜덤으로 생성
      //인덱스가 지원가능한 종류보다 큰 수 혹은 -1보다 작은 수가 들어오면 랜덤으로 생성
      if(index == RANDOM_INDEX || index >= enemyPrefebs.Length || index < RANDOM_INDEX)
      {
          target = Random.Range(0,enemyPrefebs.Length);
      }

        // 큐에 사용 가능한 오브젝트가 있는지 확인 (큐가 비어있는지 확인)
      if( pools[target].Count >0)
      {
          // 큐에 오브젝트가 있다.
          result = pools[target].Dequeue(); // 빠져서 리턴해줌
          result.SetActive(true);// 오브젝트 활성화
      }
      else
      {
          // 큐에 오브젝트가 없다.
          // 확장 작업
          PoolExpand(target, queueSize[target]<<1); // 큐가 두배로 커지고 오브젝트도 추가되는 함수
          result = GetEnemy(target);
      }
      return result;

  }

  // Pool을 처음 생성하거나 크기를 확장하고 싶을때(pool에 오브젝트가 바닥났을 때) 호출하는 함수
    private void PoolExpand(EnemyType type, int poolSize, bool init = false)
    {
        int index = -1;
        //switch (type)
        //{
        //    case EnemyType.NORMAL:
        //        index = 0;
        //        break;
        //    case EnemyType.BLUE:
        //        index = 1;
        //        break;
        //    case EnemyType.RED:
        //        index = 2;
        //        break;
        //    case EnemyType.INVALID:
        //        break;
        //}
        index = (int)type;
        PoolExpand(index, poolSize, init);
    }

    //풀을 처음 생성하거나 크기를 확장하고 싶을 떄 (pool에 오브젝트가 바닥났을 때) 호출하는 함수
  private void PoolExpand(int poolIndex, int poolsize, bool init = false ) 
  {
    if(-1 < poolIndex && poolIndex < enemyPrefebs.Length) 
    {
        //poolIndex가 적절한 범위다
        pools[poolIndex] = new Queue<GameObject>(poolsize); // 비어있는 풀 생성. 초기 크기 지정
        int makeSize = poolsize; // 실제 Instanticate할 객수 설정
        if(!init)                //Initialize 함수에서 호출 했을 경우가 아니면
        {                        // Queue 크기의 절반만큼만 생성 (이미 만들어져 있던 오브젝트 만큼은 생성할 필요가 없음)
            makeSize>>=1;// >>1 : /2 랑 같은건데 연산 더 빠름 2배 4배 8배 16배 .. 일떄
        }
        for(int i=0;i < makeSize;i++ ) // 오브젝트 하나씩 생성
        {
            GameObject obj = GameObject.Instantiate(enemyPrefebs[poolIndex],this.transform);
            obj.name = $"{enemyPrefebs[poolIndex].name}_{queueSize[poolIndex]}"; // 안하면 이름이 다 똑같음 그냥 알아보기 편하라고 이름을 변경해줌
            obj.SetActive(false);          // 비활성화 상태로 변경해줌 -> 아름 회색으로 뜸
            Enemy enemy = obj.GetComponent<Enemy>();
            switch(poolIndex)
                {
                    case 0:
                        enemy.Type = EnemyType.NORMAL;
                        break;
                    case 1:
                        enemy.Type = EnemyType.BLUE;
                        break;
                    case 2:
                        enemy.Type = EnemyType.RED;
                        break;
                    default:
                        break;
                }               

                pools[poolIndex].Enqueue(obj);  //큐에 오브젝트 삽입
                queueSize[poolIndex]++;         //queueSize 증가
        }
    }
  }

    //파라미터로 받은 enemy를 pool로 돌려주는 함수
    public void ReturnEnemy(GameObject enemy) // get 이랑 쌍으로 했던일 반대로
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemy.SetActive(false);
        Queue<GameObject> targetPool = pools[(int)enemyScript.Type];
        targetPool.Enqueue(enemy);
    }

//1. 새가 천장에 부딪쳐도 죽음
//2. 메모리 풀 초기화 함수 완성
//3. pool에서 오브젝트를 하나 가져오는 함수 
//4. pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구 되었을 때 처리하는 함수
//5. Spawner를 Pool 을 이용해 오브젝트 받기
//6. 사용이 끝난 오브젝트를 pool로 반환


}
