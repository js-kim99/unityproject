using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2-5자리 랜덤 숫자 셋팅 다 만들어놓고 갖다 쓰기만 할 수 있게 해야함
/// 진행상황 : 2-5자리 랜덤으로 만들고, 다른 스크립트에서 쓰기 가능
/// 남은거 : 2-5자리 세트 숫자 다 합치면 14개인데, 0부터 9 이므로 각각 최대 2개씩만 쓰이게 만들기
/// </summary>

//랜덤 숫자 생성을 위한 스크립트

public class GenerateRandomint : MonoBehaviour
{
    private GenerateRandomint generaterandomint;
    private int Straight_Minlength = 2;
    private int Straight_Maxlength = 5;
    private int Reverse_Minlength = 2;
    private int Reverse_Maxlength = 3;

    public List<string> StraightNumbers;
    public List<string> ReverseNumbers;


    void Start()
    {
        generaterandomint = GetComponent<GenerateRandomint>();
        StraightNumbers = MakeStraightNumbers();
        ReverseNumbers = MakeReverseNumbers();

    }

    // Update is called once per frame
    void Update()
    {

    }



   /// <summary>
   /// 입력된 매개변수의 값 만큼 숫자열 반환
   /// </summary>
   /// <param name="QuestionNumberLength"></param>
   /// <returns></returns>
    private string CreateSequence(int QuestionNumberLength) 
    {
        HashSet<int> digitSet = new HashSet<int>();//한 숫자열 안에는 중복되는게 없도록 HashSet 사용
        while (digitSet.Count < QuestionNumberLength)//2-5자리 문제 각각 생성하기 위함. 반복문 한번에 랜덤한 숫자 1개 생성되어서 QuestionNumberLength만큼의 자릿수 구성
        {
            System.Random random = new System.Random();//시드값을 랜덤으로 만들기 위한 코드
            System.Random randomint = new System.Random(random.Next(10000));//시드값 1만개이므로 숫자 배열 1만가지 생성

            int digit = randomint.Next(10);//0-9 정수 생성해서 digit에 추가하는데 한 숫자열 안에는 중복되는게 없도록 HashSet 사용해서 생성
            digitSet.Add(digit);
        }
        return digitSet.ToString();

    }


    /// <summary>
    /// 매개변수로 들어온 길이 만큼의 숫자열 생성
    /// 숫자는 2-5자리까지 가는데 서로 2번씩만 겹치도록. 2에서 5까지 더하면 14이니까 10개 숫자가 2번 이내로 나와야 함
    /// 이거는 나중에 구현하고, 일단 한 세트 안에서 숫자 안겹치도록 하기는 아래 코드와 같음. 
    /// 인덱스 0-3으로 해서 4개 만들기?
    /// </summary>
    public List<string> MakeStraightNumbers()
    {
        List<string> Questionlist = new List<string>();

        for ( int i = Straight_Minlength; i < Straight_Maxlength; i++)//2-5까지 반복.
        {
            Questionlist.Add(CreateSequence(i));//2자리부터 5자리까지 반복
        }
        foreach(string str in Questionlist)
        {
            Debug.Log(str);//2~5자리 잘 만들어졌는지 확인
        }
        Debug.Log("exe MakeStraightNumbers");
        
        return Questionlist;//정방향 질문 리스트 자료형
    }

    /// <summary>
    /// 역방향 문자열을 위한 함수
    /// </summary>
    /// <returns></returns>
    public List<string> MakeReverseNumbers()
    {
        List<string> Questionlist = new List<string>();

        for (int i = Reverse_Minlength; i < Reverse_Maxlength; i++)//2-3까지 반복.
        {
            Questionlist.Add(CreateSequence(i));//2자리부터 3자리까지 반복
        }
        foreach (string str in Questionlist)
        {
            Debug.Log(str);//2~3자리 잘 만들어졌는지 확인
        }
        return Questionlist;//역방향 질문 리스트 자료형
    }


}
