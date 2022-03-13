using System;
using UnityEngine;
using UnityEngine.UI;

// 싱글폰으로 만들 GameManager
// 싱글톤 : 클래스의 객체가 단 하나만 존재하게 프로그래밍하는 디자인 패턴

// static : 정적. 프로그램 실행 전에 결정되어 있는 것들.
// static 변수 
// 변수가 저장되는 메모리 위치가 실행전에 결정된다.
// 따라서 해당 변수를 가지는 클래스의 모든 객체가 static변수의 값은 공유한다.

// dynamic : 동적. 프로그램 실행 중(runtime)에 변경되는 것들.
public class GameManager// 모노 상속 안받은 그냥 씨샵 클래스 게임오브젝트에 넣을 수 없음
{
    private static GameManager instance = null; // 프로그램 전체에서 단 하나만 존재한다.
    private GameManager() { } //생성자가 private -> 생성 가능한 곳이 클래스 내부에서만 생성 가능 -> 객체를 안만들어도 되는 static 뭐시기..

    //private Text scoreText = null; // score가 찍힐 UI text용 참조
    private ImageNumber imageNumber=null;

    public static GameManager Inst
    {
        get // 읽기만 가능 set은 안돼
        {
            if(instance==null) // 아직 객체 생성이 한번도 이루어지지 않았을 때
            {
                instance = new GameManager(); // 한번도 안일어났으면 그때 첨으로 객체 생성
                //instance.scoreText = GameObject.Find("ScoreText").GetComponent<Text>();// scoreText를 찾아서 변수 채우기 넣을 것
                instance.imageNumber=GameObject.Find("ImageNumber").GetComponent<ImageNumber>();
            }   
            return instance; // return까지 왔다는 것은 instance에 이미 무엇인가 할당이 되어있음  
        }
    }


    private void RefreshScore()
    {
        //scoreText의 텍스트 갱신
        //scoreText.text = $"Score : {score}";
        imageNumber.Number=score;
    }
    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            RefreshScore(); //값의 변화가 있으면 자동으로 화면 갱신
        }
    }

    Player player = null;
    public Player MyPlayer
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }


    // ( 변수가 아니라 ) property 작성
    // static 맴버 변수를 변경할 거라 static이어야한다.
    // static 함수는 객체를 생성하지 않아도 되기 때문에  static으로 선언한다.
    
}

