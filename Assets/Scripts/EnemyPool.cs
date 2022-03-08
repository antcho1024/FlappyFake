using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
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

// 대량으로 생성 해 놓은 메모리 풀
  private GameObject[,] pool = null;



  // 기능 ! 
  // 1. 초기화
  // 2. 하나씩 받기


}
