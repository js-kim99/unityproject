using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


//이 스크립트는 버튼 눌러서 표시하는 역할


public class ButtonController : MonoBehaviour
{
    private ButtonController Buttoncontroller;
    private GameObject DisplayInput;//사용자가 입력한 숫자를 유니티 화면에 표시해주는 역할
    private TextMesh InputNumbers; // 사용자가 입력한 숫자를 유니티 화면에 표시해줄 텍스트를 저장할 TextMesh 컴포넌트

    string enteredNumber;
    string question;
    private void Start()
    {

        Buttoncontroller = GetComponent<ButtonController>();
        InputNumbers = GameObject.FindGameObjectWithTag("DisplayUserInput").GetComponent<TextMesh>();//이 태그를 쓴 3D 오브젝트에 글자를 보여줌.. 근데 굳이 이렇게 해야할까? 

    }
    private void Update()
    {
        ButtonClick();
    }


    public void ButtonClick()//버튼 클릭할 수 있게 하는 함수
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Button")) // 버튼 오브젝트를 눌렀을 때
                {
                    int buttonValue = int.Parse(hit.collider.gameObject.name); // 버튼의 이름을 숫자로 변환하여 버튼 값으로 사용
                    ButtonPressed(buttonValue);
                }
            }
        }
    }

    //        string stringquestion = string.Join("", qusetion);


    private void ChangeInputText(string newText)//입력란의 글자를 바꾸기 위한 함수
    {
        InputNumbers.text = newText;//Textmash에 문자열을 넣는 것
    }


    private void ButtonPressed(int buttonValue)//0-9,입력,삭제 버튼 동작하게 하는 함수
    {
        if (buttonValue >= 0 && buttonValue <= 9)
        {
            enteredNumber += buttonValue.ToString(); // 입력한 버튼 값을 비밀번호에 추가
            
        }
        else if (buttonValue == 10) // 전체 삭제 버튼
        {
            enteredNumber = ""; // 비밀번호 초기화
        }
        else if (buttonValue == 11) // 입력 버튼
        {
            IsCorrectCheck(enteredNumber);//눌렸다는것만 체크하고 정답 여부는 다른 스크립트에서 검사할 것. 그 결과로 자릿수 올려야 하기 때문
        }
        ChangeInputText(enteredNumber);//비밀번호 표시를 누른 숫자 누적으로 업데이트
    }
    private void IsCorrectCheck(string enteredNumber)
    {
        // 입력한 숫자 확인 및 처리 로직을 구현
        if (enteredNumber == question)
        {
            enteredNumber = "정답";
        }
        else
        {
            enteredNumber = "다시 시도";
            if (enteredNumber == question)
            {
                enteredNumber = "정답";
            }
            else
            {
                enteredNumber = "실패, 게임 종료";
            }
        }
    }
}



//함수 구성

/*
 위의 코드는 마우스 왼쪽 버튼이 클릭될 때 Update 함수가 호출되고, Physics.Raycast를 사용하여 버튼 오브젝트와 충돌 검사를 수행합니다. 
버튼 오브젝트는 각각의 숫자 값을 이름으로 가지고 있어야 합니다. 버튼 오브젝트에는 "Button" 태그를 추가해야 하며, 해당 태그를 가진 오브젝트와 충돌하면 버튼을 누른 것으로 간주합니다.

버튼을 누를 때마다 ButtonPressed 함수가 호출되고, 입력된 버튼 값에 따라 비밀번호를 추가하거나 삭제하는 등의 동작을 수행합니다. 
이후 IsCorrectCheck 함수에서 입력된 비밀번호를 확인하고 알맞은 처리를 수행합니다.

비밀번호를 표시하는 방식은 개발자의 선택에 따라 다양하게 구현할 수 있습니다. UpdatePasswordDisplay 함수를 사용하여 비밀번호를 표시하는 방식을 원하는 대로 수정하십시오.
*/

/*
 
구현해야 하는 것 : 게임 시작 버튼을 누르면 게임이 시작되는것. 
소리를 불러주면 또는 화면에 숫자 하나씩 표현되면 그걸 외우고 정방향으로 쓰는 것과 역방향으로 쓰는 것. cogfit 검사처럼. 
1초씩 보여주고 외우게 한 다음 번호를 차례대로 쓰고, 번호를 역순으로 써서 맞추는 것

 */




//게임 시작 버튼 누르면 3D 오브젝트에 숫자 하나씩 나오게 하기. 게임오브젝트로 변수 잡고, 텍스트 변수를 변하게 하기. 문제 텍스트는 하나 따로 생성
//문제 텍스트를 foreach 해서 3d 오브젝트에 나오게 하기. 출력 끝나면 3d 오브젝트 가리기

//문제 표시하는 함수 : 게임 시작 버튼 눌리면 setactivate true -> true는 변수로 해서 플래그 역할, 문제 1초씩 나타내기.  


