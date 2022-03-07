using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // 조건1. 테스트 플레이어는 바닥에 닿으면 죽는다
    // 조건1. 테스트 플레이어는 적새와 닿으면 죽는다
    // 조건1. 테스트 플레이어는 죽으면 반대쪽 방향으로 구른다. z축 + 방향으로 회전
    // 조건1. 테스트 플레이어는 죽으면 조종이 안됨
    // 조건1. 테스트 플레이어는 죽으면 배경 스크롤이 멈춤 

    // 힌트 
    // OncollisionEnter // 이 스크립트가 가진 컬라이더가 다른 컬라이더와 충돌 했을 때 실행되는 함수
    // AddTorque        // Rigidbody의 맴버 함수. 회전을 더해준다.

    private Rigidbody2D rigid = null;   
   
   
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    //이 스크립트가 가진 컬라이더가 다른 컬라이더와 충돌 했을 때 실행되는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log(collision.gameObject.name + "충돌");
        OnDead(); 

        if (collision.gameObject.CompareTag("Ground")) //.Tag == "Ground" 보다는 CompareTag가 더 좋다
        {
            OnFalldown(collision.gameObject);
        }
    }
    public void OnDead()
    {
        rigid.gravityScale = 1.0f; //테스트 플레이어 전용. 죽었을 때만 중력 적용 받음
        rigid.AddTorque(3.0f);     //리지드바디를 통해 회전력 추가
    }
    private void OnFalldown(GameObject ground) // 바닥에 떨어졌을때
    {
       // Debug.Log(" 땅 부딪침 ");
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
