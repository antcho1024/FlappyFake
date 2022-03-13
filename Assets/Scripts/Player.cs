using System.Collections;           //네임스페이스 설정(C# 컨테이너용)
using System.Collections.Generic;   //네임스페이스 설정(C# 컨테이너용, 제네릭)
using UnityEngine;                  //네임스페이스 설정(Unity 용)
using UnityEngine.InputSystem;      //Unity 새 인풋 시스템을 쓰기 위한 네임스페이스

/// 접근제한자 : public(공용, 공공의), protected(보호된), private(개인적인)
/// public : 누구나 쓸 수 있다.
/// private : 가지고 있는 자만 쓸 수 있다. C# 디폴트
/// protected : 나와 나를 상속받은 자만 쓸 수 있다.
/// 키워드 : C#에서 사용을 예약해 놓은 단어들(파란색으로 보이는 부분들)
/// class : 일종의 틀. 어떤 기능을 하고 어떤 데이터를 가질 수 있는지 설정해 놓은 일종의 설계도
/// 객체 : 클래스를 인스턴스 한 것.클래스를 실체화 한 것
/// 변수 : 데이터를 저장하는 곳. 데이터 타입

//class MyTest
//{
    
//}

//Player라는 이름의 클래스를 만드는데 public이라 프로그램 내의 모두가 접근할 수 있고
//MonoBehaviour라는 클래스를 상속받았다.
public class Player : MonoBehaviour     
{
    // jumpPower라는 이름의 float 타입의 변수를 만드는데. public이고 저장되는 초기값은 10.0이다.
    public float jumpPower = 10.0f;

    public float spinSpeed = 5.0f;
    // rigid라는 이름의 Rigidbody2D 타입의 변수를 만드는데, private이고 저장되는 초기값은 null이다.
    private Rigidbody2D rigid = null;

    //플레이어의 사망여부를 기록해놓은 변수
    private bool isDead = false; // false 가 살아있는거
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    // 게임 오브젝트가 만들어진(Instance) 후 실행되는 함수
    private void Awake() 
    {
        //cashing을 통해 무거운 작업을 최소한으로 하기 위해 Awake에서 찾음
         rigid = GetComponent<Rigidbody2D>();   //Rigidbody2D 컴포넌트를 찾아서 rigid에 저장해라.
        GameManager.Inst.MyPlayer = this;
    }

    // 매 프레임 마다 호출되는 함수
    //private void Update()
    //{
    //    //// Input Manager를 통해 입력처리를 하는 방법
    //    //if( Input.GetButton("Jump") ) 
    //    //{            
    //    //    rigid.AddForce(Vector2.up * jumpPower);
    //    //}
    //    //if(Input.GetKeyDown(KeyCode.Space))
    //    //{

    //    //}
    //}

    // 스페이스 버튼을 눌었을 때 실행될 함수
    // 기능은 리지드바디를 통해 위쪽으로 힘을 더한다.
    public void JumpUp(InputAction.CallbackContext context)
    {
        //context.started;      //키를 눌렀을 때
        //context.performed;    //키를 길게 눌렀었을 때(차징류)
        //context.canceled;     //키를 땠을 때

        if(!isDead)
        {
            if (context.started)    //키를 눌렀을 때만 아래의 코드를 실행하라
            {
                //rigidbody에 힘을 가해라. 위쪽 방향으로 jumpPower만큼.
                rigid.AddForce(Vector2.up * jumpPower);

                //Debug.Log("Jump!");
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name + "충돌");
        OnDead();

        if (collision.gameObject.CompareTag("Ground")) //.Tag == "Ground" 보다는 CompareTag가 더 좋다
        {
            OnFalldown(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Sky"))
        {
            OnEnterSky();
        }
        else
        {
            OnBirdStrike(collision);
        }
    }

    private void OnEnterSky()
    {
        Scroller scroller = GameObject.FindObjectOfType<Scroller>();
        scroller.ScrollSwitch=false;

        rigid.angularVelocity = 0.0f;
        rigid.AddForce(Vector2.right,ForceMode2D.Impulse);
        rigid.AddTorque(-5.0f);
    }

    private void OnBirdStrike(Collision2D collision)
    {
        //collision.contactCount;//
        //collision.contracts[0].point//
        rigid.AddForce(new Vector2(-1, 1) * 2, ForceMode2D.Impulse);
        rigid.AddTorque(spinSpeed);     // 리지드바디를 통해 회전력 추가
    }

    public void OnDead()
    {
        isDead = true;
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.gravityScale = 1.0f; // 죽었을 때 빠르게 떨어지게 하기위한 중력 적용 받음
    }
    private void OnFalldown(GameObject ground) // 바닥에 떨어졌을때
    {
        //Debug.Log(" 땅 부딪침 ");
        // 하이라키에서 뒤지는 거
        // FindObjectOfType : 특정 타입의 컴포너느를 가지고 있는 첫번째 게임 오브젝트를 찾아주는 함수
        // FindObjectsOfType : 특정 타입의 컴포너느를 가지고 있는 모든 게임 오브젝트를 찾아주는 함수
        // GameObject.Find : 파라메터로 받은 문자열과 같은 이름을 가진 게임 오브젝트를 찾아주는 함수 -> 문자열 비교라 Bad
        // GameObject.FindGameObjectWithnTag : 파라메터로 받은 문자열과 같은 태그를 가진 첫번째 게임 오브젝트를 찾아주는 함수
        // GameObject.FindGameObjecstWithnTag : 파라메터로 받은 문자열과 같은 태그를 가진 모든 게임 오브젝트를 찾아주는 함수
        Scroller scroller = ground.GetComponent<Scroller>();
        scroller.ScrollSwitch = false;
    }
}
