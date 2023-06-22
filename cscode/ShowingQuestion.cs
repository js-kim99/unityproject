using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//문제를 만들고 문제 전용 3D 오브젝트에 표시함
/// <summary>
/// 목표 : 함수만 딱 쓰면 정방향 2-5자리 순서대로 출력. 
/// 
/// 나중에 틀린것 여부도 하려면 중간 중간에 탈출 조건 넣어줘야 하므로 매개변수 들어가야 될 것 같기도?
/// 
/// </summary>


public class ShowingQuestion : MonoBehaviour
{
    private ShowingQuestion showingquestion;
    private GenerateRandomint GenerateRandomint;//랜덤 숫자 만들기
    //private GameObject QuestionDisplay = new GameObject("QuestionDisplay");//원 안에 문제 표시.. 이것도 태그로 만들기?

    private GameObject DisplayQuestion;//사용자가 입력한 숫자를 유니티 화면에 표시해주는 역할
    private TextMesh QuestionNumbers; // 사용자가 입력한 숫자를 유니티 화면에 표시해줄 텍스트를 저장할 TextMesh 컴포넌트
    List<string> Questionlist = new List<string>();//2-6자리 문제가 들어갈 리스트 변수 선언


    //int MaxQuestionLength = 4;//문제 갯수

    void Start()
    {
        showingquestion = GetComponent<ShowingQuestion>();

        DisplayQuestion = GameObject.FindGameObjectWithTag("DisplayQuestionText");//이 태그를 쓴 3D 오브젝트에 글자를 보여줌
        QuestionNumbers = DisplayQuestion.GetComponent<TextMesh>();//변수로 받은 숫자를 유니티 화면에 표시할 역할. 얘를 3D 텍스트와 연결해야 함

        //Questionlist = GenerateRandomint.MakeStraightNumbers();//2-5자리 문제가 들어갈 리스트. 게임 시작할때마다 새로 생성되는 세트들

    }

    // Update is called once per frame
    void Update()
    {
        // 작동하는건 게임 시작 버튼 누르는 것과 문제 맞추는 것에 종속되므로 여기서 Update는 안쓸 것 같음
    }


    /// <summary>
    /// 텍스트를 바꾸는 함수
    /// </summary>
    /// <param name="newText"></param>
    private void ChangeQuestionText(string newText)//문제 보여주는 곳의 글자를 바꾸기 위한 함수. 문제 텍스트 바꿀 때 무조건 이거 쓰면 됨
    {
        QuestionNumbers.text = newText;//Textmash에 문자열을 넣는 것
    }



    /// <summary>
    /// 
    /// 한 문제에 대해 하나씩 출력되도록 보여주는 함수
    /// 문제를 표시하도록 만드는 것
    /// 한 시퀀스 가져오면 그걸 하나씩 접근해야 함. foreach 하면 될듯
    /// 
    /// </summary>
    public void ShowOneSequenceinDisplay(string question)
    {

        foreach (char c in question)//인덱스 n번째 시퀀스를 하나씩 불러옴
        {
            Debug.Log("이번 시퀀스는");
            Debug.Log(question);


            ChangeQuestionText(c.ToString());//char 이니까 string으로 바꾸자
        }



    }
    
    //문제를 맞추면 하나씩 넘어가야 함. 
    //함수 쪼개기 : 1) 한 시퀀스를 다 보여주는 것 2) 


}
