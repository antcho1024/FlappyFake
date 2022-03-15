/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType : byte // 문자를 숫자처럼 쓰기 위함, 크기 지정
{
    NORMAL = 0,
    BLUE,
    RED,
    INVALID
}

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 1.5f;

    private const float SCORE_POSITION = 1.5f;
    private bool getScore = false;
    private EnemyType type = EnemyType.INVALID;
    public EnemyType type
    {
        get
        {
            return type;
        }
        set 
        {
            if(type == EnemyType.INVALID)
            {
              type = value;    // 한번만 쓸 수 있다.
            }
           
        }
    }

    //    //this.transform.position;    //위치 (0,0,0)
    //    //this.transform.rotation;    //회전 (0,0,0)
    //    //this.transform.localScale;  //크기배율 (1,1,1)

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        //적 새가 일정 x좌표 이하로 넘어가면 점수 +1
        if(!getScore)
        {
            if (this.transform.position.x < SCORE_POSITION)
            {
                if(!GameManager.Inst.MyPlayer.IsDead)
                {
                    GameManager.Inst.Score += 1; // static 클래스인 GameManager에 점수 +1 기록
                    getScore = true; // 점수는 한번만 +1이 되도록 설정
                    Debug.Log("점수 : " + GameManager.Inst.Score);

                    //Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
                    //scoreText.text = $"Score : {GameManager.Inst.Score}";
                }
                
            }
        }
        
    }

}*/

///////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType : byte
{
    NORMAL = 0,
    BLUE,
    RED,
    INVALID
}

public class Enemy : MonoBehaviour
{
    //이 게임 오브젝트는 게임이 시작되면 -x축 방향으로 일정한 속도로 이동한다.
    //이동하는 속도는 moveSpeed라는 변수에 저장되어 있다.
    //12:35분까지 실습 진행(쉬는시간 포함)

    //힌트
    //transform에 position에 위치값이 저장되어 있음
    //transform에 어떤 함수가 있음

    public float moveSpeed = 1.5f;

    private const float SCORE_POSITION = 1.5f;
    private bool getScore = false;
    private EnemyType type = EnemyType.INVALID;
    public EnemyType Type
    {
        get
        {
            return type;
        }
        set
        {
            if (type == EnemyType.INVALID)
            {
                type = value;   // 한번만 쓸 수 있다.
            }
        }
    }


    //private void Start()
    //{
    //    //this.transform.position;    //위치 (0,0,0)
    //    //this.transform.rotation;    //회전 (0,0,0)
    //    //this.transform.localScale;  //크기배율 (1,1,1)

    //    // (0,1,1)+(1,3,1) = (1,4,2)
    //    // (1,2,3) * 2 = (2,4,6)
    //}

    // 오브젝트가 활성화 될때 실행되는 함수
    private void OnEnable()
    {
        getScore = false;
    }

    // 매프레임 호출
    private void Update()
    {
        //왼쪽 방향으로 초당 moveSpeed만큼 이동하도록 설정
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        //적 새가 일정 x좌표 이하로 넘어가면 점수+1
        if (!getScore)
        {
            //if ((!getScore) && (transform.position.x < SCORE_POSITION))
            if (transform.position.x < SCORE_POSITION)
            {
                if (!GameManager.Inst.MyPlayer.IsDead)
                {
                    //Debug.Log("점수 +1");
                    GameManager.Inst.Score += 1;    //static 클래스인 GameManager에 점수 +1 기록
                    getScore = true;    // 점수는 한번만 +1이 되도록 설정
                    //Debug.Log($"현재 점수 : {GameManager.Inst.Score}");
                }
            }
        }
    }

}


