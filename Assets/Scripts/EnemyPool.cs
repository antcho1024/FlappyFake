using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private const int DEFAULT_POOL_SIZE = 16;
    private const int RANDOM_INDEX = -1;
    private static EnemyPool instance=null;
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

    // 생성할 적의 종류
   public GameObject [] enemyPrefebs=null;// 갯수는 유니티에서 변경 가능
   private Queue<GameObject>[] pools = null; // 적 종류별로 만든 Queue를 저장할 배열

// 대량으로 생성 해 놓은 메모리 풀
//  private GameObject[,] pool = null;

// 초기화 (pool 변수를 채운다)
  public void Initialize()
  {
      // pool 변수를 채운다
      // 예시 ) (한 페이지 최대 16마리 )*(3종류) = 48개 = instance를 48번 한다.

      pools = new Queue<GameObject>[enemyPrefebs.Length];//3 : 적종류의 수만큼 배열 크기 확정

      for(int i=0; i< enemyPrefebs.Length; i++)
      {
          pools[i] = new Queue<GameObject>();  // 큐를 하나 생성
          for(int j=0; j < DEFAULT_POOL_SIZE ;j++) //종류별로 16개씩 만들겠다
          {
              GameObject obj = GameObject.Instantiate(enemyPrefebs[i],this.transform); //생성하고 Enemypool 의 자식으로 추가
              obj.name = $"{enemyPrefebs[i].name}_{j}"; // 안하면 이름이 다 똑같음 그냥 알아보기 편하라고 이름을 변경해줌
              obj.SetActive(false); // 비활성화 상태로 변경해줌 -> 아름 회색으로 뜸
              pools[i].Enqueue(obj); // 큐에 생성한 오브잭트 삽입
              //pools[i].Enqueue(GameObject.instantiate(enemyPrefebs[i],this.transform));
          }
      }
  }
  //pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구 되었을 떄 처리하는 함수
  private void PoolExpand()
  {// 풀에 있는 오브젝타가 다 떨어지면 확장하기

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
          PoolExpand(); // 큐가 두배로 커지고 오브젝트도 추가되는 함수
          result = GetEnemy(target);
      }
      return result;

  }

// 과제
//1. 새가 천장에 부딪쳐도 죽음
//2. 메모리 풀 초기화 함수 완성
//3. pool에서 오브젝트를 하나 가져오는 함수 
//4. pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구 되었을 때 처리하는 함수
//
//


}
