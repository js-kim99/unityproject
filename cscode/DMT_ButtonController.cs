using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DMT_ButtonController : MonoBehaviour
{

    
    public bool isGameStart;

    private GameObject GamestartPannel;
    private Button Gamestartbutton;

    //UI 부분

    //private List<char> GoList;
    //private List<char> NoGoList;
    string GoList, NoGoList, GoNoGoQuestion;

    private GameObject QuestionDisplayGameobject;//사용자가 입력한 숫자를 유니티 화면에 표시해주는 역할
    private TextMesh QuestionDisplay; // 사용자가 입력한 숫자를 유니티 화면에 표시해줄 텍스트를 저장할 TextMesh 컴포넌트   

    private int Go_clicked, Go_missed, NoGo_cliked;
    float Sum_GoClickTime, Avg_GoClickTime;

    private float startTime;      // 시간 측정 시작 시간
    private float buttonTime;     // 버튼이 눌린 시간
    private float elapsedTime;    // 경과 시간
    private bool buttonPressed;   // 버튼이 눌렸는지 여부

    public GameObject ResultPannel;
    private Text ResultText;

    // Start is called before the first frame update
    void Start()
    {

        GamestartPannel = GameObject.FindGameObjectWithTag("DMT_GamestartPanel");
        Gamestartbutton = GameObject.FindGameObjectWithTag("DMT_GamestartButton").GetComponent<Button>();



        ResultText = GameObject.FindGameObjectWithTag("DMT_ResultText").GetComponent<Text>();
        ResultPannel.SetActive(false);

        //GoList = new List<char>() { 'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ' };
        //NoGoList = new List<char>() { 'ㅏ', 'ㅓ', 'ㅗ', 'ㅜ', 'ㅣ', '1', '2', '3', '4', '5' };

        GoList = "ㄱㄴㄷㄹㅁ";
        NoGoList = "ㅏㅓㅗㅜㅣ12345";
        GoNoGoQuestion = "ㄱㅏ2ㄴㅗㄹ1ㅜㄷ4";

        Go_clicked = 0;
        Go_missed = 0;
        NoGo_cliked = 0;

        Sum_GoClickTime = 0;
        Avg_GoClickTime = 0;
        //Questionlength = GoNoGoQuestion.Length;
        // 태그로 특정 오브젝트를 찾습니다.

        QuestionDisplay = GameObject.FindGameObjectWithTag("DMT_QuestionText").GetComponent<TextMesh>();
        QuestionDisplay.text = "+";



        Gamestartbutton.onClick.AddListener(OnButtonClick);//여기서 게임 시작 버튼 눌리면 true 됨


    }

    // Update is called once per frame
    void Update()
    {
        ButtonClick();
    }

    private void OnButtonClick()//UI 부분
    {
        Gamestartbutton.gameObject.SetActive(false);
        GamestartPannel.SetActive(false);
        StartCoroutine(GoNoGoGame());
    }
    
    public void ButtonClick()//버튼 클릭할 수 있게 하는 함수
    {
        //string stringquestion = string.Join("", qusetion);
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("DMT_InputButton")) //이 이름의 태그를 가진 버튼 오브젝트를 눌렀을 때
                {
                    buttonPressed = true;
                    buttonTime = Time.time;
                }
            }
        }
    }



    IEnumerator GoNoGoGame()
    {
        
        QuestionDisplay.text = '+'.ToString();
        yield return new WaitForSeconds(1.5f); // 1.5초 동안 처음 십자가 화면 보여줌

        foreach (char q in GoNoGoQuestion)
        {
            buttonPressed = false;

            ///Go 에서는 누른 시간, 놓친 여부 측정
            if (GoList.Contains(q.ToString()))
            {

                QuestionDisplay.text = q.ToString();
                yield return new WaitForSeconds(0.3f); // 0.3초 동안 문제 보여줌
                QuestionDisplay.text = "";//문제 사라짐
                startTime = Time.time;
                yield return new WaitForSeconds(1.8f); // 1.8초 동안 입력 대기

                if (buttonPressed == true)
                {
                    Go_clicked++;
                    elapsedTime = buttonTime - startTime;
                    Sum_GoClickTime += elapsedTime;
                }
                else
                {
                    //Go인데 못누른 카운트 추가
                    Go_missed++;
                }

                yield return new WaitForSeconds(0.5f); // 0.5초 동안 휴식
            }
            else
            {

                QuestionDisplay.text = q.ToString();
                yield return new WaitForSeconds(0.3f); // 0.3초 동안 문제 보여줌
                QuestionDisplay.text = "";//문제 사라짐
                yield return new WaitForSeconds(1.8f); // 1초 동안 입력 대기

                if (buttonPressed == true)
                {
                    NoGo_cliked++;
                }

                yield return new WaitForSeconds(0.5f); // 0.5초 동안 휴식
                
            }
            
        }
        
        QuestionDisplay.text = "";//대기 화면 보여줌
        Counting();
        yield break;

    }


    /// <summary>
    /// 버튼이 눌리면 Go, NoGo 2가지로 나눠서 해야 함. 
    /// Go 일때는 누르면 시간 체크, 못누르면 카운트+1
    /// NoGo 일때는 누르면 카운트+1
    /// </summary>
    private void Counting()
    {
        string isPassed;
        string formattedAvg;

        Avg_GoClickTime = Sum_GoClickTime / Go_clicked;
        formattedAvg = Avg_GoClickTime.ToString("F3");

        if (Avg_GoClickTime <= 600 && Go_missed <= 2 && NoGo_cliked <= 2) 
        {
            isPassed = "통과";
        }
        else
        {
            isPassed = "실패";
        }
        ResultPannel.SetActive(true);
        ResultText.text = "결과 : "+isPassed+"\nGo 놓친 횟수 :" + Go_missed + "\nNoGo 클릭 횟수 :" + NoGo_cliked + "\nGo 누른 평균 시간 : \n" + formattedAvg + "ms";
    }

}


