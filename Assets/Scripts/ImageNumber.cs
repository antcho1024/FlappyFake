using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    // 숫자 하나를 받아서 자식 오브젝트인 Num100, 10 1의 스프라이트를 변경해 숫자를 표현하는 클래스

    private const int DIGIT_SIZE=3;
    public Image[] digits = new Image[DIGIT_SIZE];
    public Sprite[] numberSprites = new Sprite[10]; // 10진수 표현하는 거라 10개

    public int number = 0; // 이미지로 변환 될 숫자

    
    public int Number
    {
        set
        {
            number = value;
            MakeImageNumber();
        }
    }
    public void MakeImageNumber()
    {
        
        if(number>999)
        {
            number=999;
        }
        int tmp=number;
        int divN=100;
        for(int i=0;i<DIGIT_SIZE;i++)
        {
            int digitNum = tmp/divN;
            tmp=tmp%divN;
            divN=divN/10;

            // 각 자리수 구함
            //digit[] : index0 에 100 자리. index1 에 10 자리. index2 에 1 자리.
            digits[i].sprite=numberSprites[digitNum];
        }
    }
     
     //인스팩터 창에서 값이 성공적으로 변경했을 때 실행되는 함수
    private void OnValidate()
    {
        MakeImageNumber();
    }

}
