using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 1.5f;

    private const float SCPRE_POSITION = 1.5f;
    private bool getScore = false;

    //    //this.transform.position;    //위치 (0,0,0)
    //    //this.transform.rotation;    //회전 (0,0,0)
    //    //this.transform.localScale;  //크기배율 (1,1,1)

        private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        //적 새가 일정 x좌표 이하로 넘어가면 점수 +1
        if(!getScore)
        {
            if (this.transform.position.x < SCPRE_POSITION)
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

}
