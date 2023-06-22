﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//https://rasino.tistory.com/343 맥북 vscode에서 한글 깨질 때 참고

public class DigitSpanGame : MonoBehaviour
{
    public GameObject DST_GamestartPannel;//태그로 바꿔야됨
    public Button DST_Gamestartbutton;//태그로 바꿔야됨

    private string enteredNumber;

    private int Straight_Minlength = 2;
    private int Straight_Maxlength = 5;
    private int Reverse_Minlength = 2;
    private int Reverse_Maxlength = 3;

    private int wrongcnt;

    //public List<string> StraightNumbers;
    //public List<string> ReverseNumbers;

    private TextMesh QuestionNumbers; //문제를 유니티 화면에 표시해줄 텍스트를 저장할 TextMesh 컴포넌트
    //List<char> Questionlist = new List<char>();//2-6자리 문제가 들어갈 리스트 변수 선언

    //List<string> question = new List<string>();

    private int MaxRoundcnt;
    private int StraightRoundcnt;//정방향 라운드 갯수
    private int roundcnt;
    private bool isRoundPassed;
    private bool isQuestionDisplayActivated;

    //private ButtonController Buttoncontroller;
    private TextMesh InputNumbers; // 사용자가 입력한 숫자를 유니티 화면에 표시해줄 텍스트를 저장할 TextMesh 컴포넌트

    List<string> Questionlist;

    public GameObject ResultPannel;
    private Text ResultText;
    private TextMesh SnRText;


    void Start()
    {
        enteredNumber = "";
        Questionlist = new List<string>() { "345", "6789", "01234","67","456" };
        //Questionlist = new List<string>() { "11", "12", "13", "14", "67", "456" };
        wrongcnt = 0;
        roundcnt = 0;
        MaxRoundcnt = Questionlist.Count-1;//인덱스라서 하나 빠져야 함
        StraightRoundcnt = 2;//0~2 인덱스까지 3개 라운드
        isRoundPassed = true;
        isQuestionDisplayActivated = true;

        QuestionNumbers = GameObject.FindGameObjectWithTag("DisplayQuestionText").GetComponent<TextMesh>();//이 태그를 쓴 3D 오브젝트에 글자를 보여줌
        InputNumbers = GameObject.FindGameObjectWithTag("DisplayUserInput").GetComponent<TextMesh>();//이 태그를 쓴 3D 오브젝트에 글자를 보여줌
        QuestionNumbers.text = "";

        SnRText = GameObject.FindGameObjectWithTag("DST_SnRText").GetComponent<TextMesh>();


        ResultText = GameObject.FindGameObjectWithTag("DST_ResultText").GetComponent<Text>();
        ResultPannel.SetActive(false);


    }



    // Update is called once per frame
    void Update()
    {
        ButtonClick();
        DST_Gamestartbutton.onClick.AddListener(OnButtonClick);

    }


    private void OnButtonClick()//UI 부분
    {
        DST_Gamestartbutton.gameObject.SetActive(false);
        DST_GamestartPannel.SetActive(false);
        GameStart();

    }


    public void ButtonClick()//버튼 클릭할 수 있게 하는 함수
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Button")) //이 이름의 태그를 가진 버튼 오브젝트를 눌렀을 때
                {
                    int buttonValue = int.Parse(hit.collider.gameObject.name); // 버튼의 이름을 숫자로 변환하여 버튼 값으로 사용
                    ButtonPressed(buttonValue);
                }
            }
        }
    }



    private void ButtonPressed(int buttonValue)//0-9,입력,삭제 버튼 동작하게 하는 함수
    {
        if (buttonValue >= 0 && buttonValue <= 9)
        {
            enteredNumber += buttonValue.ToString(); // 입력한 버튼 값을 비밀번호에 추가
            ChangeInputText(enteredNumber);//비밀번호 표시를 누른 숫자 누적으로 업데이트
            InputNumbers.text = enteredNumber;
        }
        else if (buttonValue == 10) // 전체 삭제 버튼
        {
            enteredNumber = ""; // 비밀번호 초기화
            InputNumbers.text = enteredNumber;

        }
        else if (buttonValue == 11) // 입력 버튼
        {
            IsCorrectCheck();
        }

    }
    private void IsCorrectCheck()
    {
        if ((roundcnt > StraightRoundcnt)&&wrongcnt==0)//인덱스 4,5 일 때 역방향 입력 구현을 위함
        {
            Questionlist[roundcnt] = ReverseString(Questionlist[roundcnt]);
        }
        // 입력한 숫자 확인 및 처리 로직을 구현
        if (enteredNumber == Questionlist[roundcnt])
        {
            wrongcnt = 0;
            InputNumbers.text = "정답";
            enteredNumber = "";
            roundcnt++;
            

            isRoundPassed = true;
            isQuestionDisplayActivated = true;
            if (roundcnt == Questionlist.Count)//여기서는 숫자가 하나 늘어나니까 원본 Count를 사용
            {
                ResultPannel.SetActive(true);
                ResultText.text = "결과 : 통과";
            }
        }
        else
        {
            wrongcnt++;
            if (wrongcnt == 1)
            {
                InputNumbers.text = "다시 시도";
                enteredNumber = "";
            }
            else
            {
                InputNumbers.text = "실패";
                isRoundPassed = false;
                roundcnt = -1;
                isQuestionDisplayActivated = false;
                ResultPannel.SetActive(true);
                ResultText.text = "결과 : 실패";
            }
        }
    }




    //게임 
    private void GameStart()
    {
        
        StartCoroutine(ShowOneSequenceinDisplay(Questionlist));

    }
    string ReverseString(string input)//문자열 역순으로 바꿔줌
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private void ChangeInputText(string newText)//입력란의 글자를 바꾸기 위한 함수
    {
        InputNumbers.text = newText;//Textmash에 문자열을 넣는 것
    }


    IEnumerator ShowOneSequenceinDisplay(List<string> question){
        
        for (; roundcnt <= Questionlist.Count && isRoundPassed == true;)
        {
            InfiniteLoopDetector.Run();
            if (isQuestionDisplayActivated == true)
            {
                if (roundcnt > StraightRoundcnt)
                {
                    SnRText.text = "역방향 입력 : ";
                }
                else
                {
                    SnRText.text = "정방향 입력 : ";
                }
                foreach (char c in question[roundcnt])//인덱스 n번째 시퀀스를 하나씩 불러옴
                {
                    QuestionNumbers.text = c.ToString();
                    yield return new WaitForSeconds(1.0f); // 1초 동안 문제 보여줌
                }
                QuestionNumbers.text = "";
                InputNumbers.text = "";

                isQuestionDisplayActivated = false;//문제 표시하는 숫자가 안나오도록 함
            }
            yield return null;
        }
    }

    

    //문제 하나당 하나씩 돌려야 하는 함수. 
    //매개변수로 돌아온 숫자만큼 숫자 생성. 
    private string CreateSequence(int QuestionNumberLength)
    {
        HashSet<string> digitSet = new HashSet<string>();//한 숫자열 안에는 중복되는게 없도록 HashSet 사용
        System.Random random = new System.Random();//시드값을 랜덤으로 만들기 위한 코드
        System.Random randomint = new System.Random(random.Next(10000));//시드값 1만개이므로 숫자 배열 1만가지 생성

        while (digitSet.Count < QuestionNumberLength)//2-5자리 문제 각각 생성하기 위함. 반복문 한번에 랜덤한 숫자 1개 생성되어서 QuestionNumberLength만큼의 자릿수 구성
        {            
            int digit = randomint.Next(10);//0-9 정수 생성해서 digit에 추가하는데 한 숫자열 안에는 중복되는게 없도록 HashSet 사용해서 생성
            digitSet.Add(digit.ToString());//digitSet 에는 숫자 들어가게 함. 
        }

        string CombinedDigit = string.Join("", digitSet);


        Debug.Log("CombinedDigit 자체 : " + CombinedDigit);
        Debug.Log("아래는 CombinedDigit 순회");
        foreach(var q in CombinedDigit)
        {
            Debug.Log(q);//해쉬셋 잘 만들어짐.
        }

        return CombinedDigit;//한 문제. 

    }
    public List<char> MakeStraightNumbers()
    {
        //해쉬셋 한세트 만들어지면 그걸 string으로 만들기
        List<char> Questionlist = new List<char>();

        for (int i = Straight_Minlength; i < Straight_Maxlength; i++)//2-5까지 반복.
        {
            string OneQuestion = CreateSequence(Straight_Minlength);

            foreach (char x in OneQuestion)
            {
                Questionlist.Add(x);//문제에서 하나씩 추가하기. 문자열 하나씩 추가하기. 숫자로 추가하면 이상하게 쪼개짐. 
            }
            
        }
        

        return Questionlist;//정방향 질문 리스트 자료형
    }

    public List<string> MakeReverseNumbers()
    {
        List<string> Questionlist = new List<string>();

        for (int i = Reverse_Minlength; i < Reverse_Maxlength; i++)//2-3까지 반복.
        {
            Questionlist.Add(CreateSequence(i).ToString());//2자리부터 3자리까지 반복
        }
        foreach (string str in Questionlist)
        {
            Debug.Log(str);//2~3자리 잘 만들어졌는지 확인
        }
        return Questionlist;//역방향 질문 리스트 자료형
    }





    public static class InfiniteLoopDetector
    {
        private static string prevPoint = "";
        private static int detectionCount = 0;
        private const int DetectionThreshold = 100000;

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Run(
            [System.Runtime.CompilerServices.CallerMemberName] string mn = "",
            [System.Runtime.CompilerServices.CallerFilePath] string fp = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int ln = 0
        )
        {
            string currentPoint = $"{fp}:{ln}, {mn}()";

            if (prevPoint == currentPoint)
                detectionCount++;
            else
                detectionCount = 0;

            if (detectionCount > DetectionThreshold)
                throw new Exception($"Infinite Loop Detected: \n{currentPoint}\n\n");

            prevPoint = currentPoint;
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void Init()
        {
            UnityEditor.EditorApplication.update += () =>
            {
                detectionCount = 0;
            };
        }
#endif
    }//무한루프 찾아줌
    //            InfiniteLoopDetector.Run(); -> 무한 찾으려는 반복문 안에 이거 쓰면 됨

}
