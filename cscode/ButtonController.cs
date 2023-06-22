using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


//�� ��ũ��Ʈ�� ��ư ������ ǥ���ϴ� ����


public class ButtonController : MonoBehaviour
{
    private ButtonController Buttoncontroller;
    private GameObject DisplayInput;//����ڰ� �Է��� ���ڸ� ����Ƽ ȭ�鿡 ǥ�����ִ� ����
    private TextMesh InputNumbers; // ����ڰ� �Է��� ���ڸ� ����Ƽ ȭ�鿡 ǥ������ �ؽ�Ʈ�� ������ TextMesh ������Ʈ

    string enteredNumber;
    string question;
    private void Start()
    {

        Buttoncontroller = GetComponent<ButtonController>();
        InputNumbers = GameObject.FindGameObjectWithTag("DisplayUserInput").GetComponent<TextMesh>();//�� �±׸� �� 3D ������Ʈ�� ���ڸ� ������.. �ٵ� ���� �̷��� �ؾ��ұ�? 

    }
    private void Update()
    {
        ButtonClick();
    }


    public void ButtonClick()//��ư Ŭ���� �� �ְ� �ϴ� �Լ�
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Button")) // ��ư ������Ʈ�� ������ ��
                {
                    int buttonValue = int.Parse(hit.collider.gameObject.name); // ��ư�� �̸��� ���ڷ� ��ȯ�Ͽ� ��ư ������ ���
                    ButtonPressed(buttonValue);
                }
            }
        }
    }

    //        string stringquestion = string.Join("", qusetion);


    private void ChangeInputText(string newText)//�Է¶��� ���ڸ� �ٲٱ� ���� �Լ�
    {
        InputNumbers.text = newText;//Textmash�� ���ڿ��� �ִ� ��
    }


    private void ButtonPressed(int buttonValue)//0-9,�Է�,���� ��ư �����ϰ� �ϴ� �Լ�
    {
        if (buttonValue >= 0 && buttonValue <= 9)
        {
            enteredNumber += buttonValue.ToString(); // �Է��� ��ư ���� ��й�ȣ�� �߰�
            
        }
        else if (buttonValue == 10) // ��ü ���� ��ư
        {
            enteredNumber = ""; // ��й�ȣ �ʱ�ȭ
        }
        else if (buttonValue == 11) // �Է� ��ư
        {
            IsCorrectCheck(enteredNumber);//���ȴٴ°͸� üũ�ϰ� ���� ���δ� �ٸ� ��ũ��Ʈ���� �˻��� ��. �� ����� �ڸ��� �÷��� �ϱ� ����
        }
        ChangeInputText(enteredNumber);//��й�ȣ ǥ�ø� ���� ���� �������� ������Ʈ
    }
    private void IsCorrectCheck(string enteredNumber)
    {
        // �Է��� ���� Ȯ�� �� ó�� ������ ����
        if (enteredNumber == question)
        {
            enteredNumber = "����";
        }
        else
        {
            enteredNumber = "�ٽ� �õ�";
            if (enteredNumber == question)
            {
                enteredNumber = "����";
            }
            else
            {
                enteredNumber = "����, ���� ����";
            }
        }
    }
}



//�Լ� ����

/*
 ���� �ڵ�� ���콺 ���� ��ư�� Ŭ���� �� Update �Լ��� ȣ��ǰ�, Physics.Raycast�� ����Ͽ� ��ư ������Ʈ�� �浹 �˻縦 �����մϴ�. 
��ư ������Ʈ�� ������ ���� ���� �̸����� ������ �־�� �մϴ�. ��ư ������Ʈ���� "Button" �±׸� �߰��ؾ� �ϸ�, �ش� �±׸� ���� ������Ʈ�� �浹�ϸ� ��ư�� ���� ������ �����մϴ�.

��ư�� ���� ������ ButtonPressed �Լ��� ȣ��ǰ�, �Էµ� ��ư ���� ���� ��й�ȣ�� �߰��ϰų� �����ϴ� ���� ������ �����մϴ�. 
���� IsCorrectCheck �Լ����� �Էµ� ��й�ȣ�� Ȯ���ϰ� �˸��� ó���� �����մϴ�.

��й�ȣ�� ǥ���ϴ� ����� �������� ���ÿ� ���� �پ��ϰ� ������ �� �ֽ��ϴ�. UpdatePasswordDisplay �Լ��� ����Ͽ� ��й�ȣ�� ǥ���ϴ� ����� ���ϴ� ��� �����Ͻʽÿ�.
*/

/*
 
�����ؾ� �ϴ� �� : ���� ���� ��ư�� ������ ������ ���۵Ǵ°�. 
�Ҹ��� �ҷ��ָ� �Ǵ� ȭ�鿡 ���� �ϳ��� ǥ���Ǹ� �װ� �ܿ�� ���������� ���� �Ͱ� ���������� ���� ��. cogfit �˻�ó��. 
1�ʾ� �����ְ� �ܿ�� �� ���� ��ȣ�� ���ʴ�� ����, ��ȣ�� �������� �Ἥ ���ߴ� ��

 */




//���� ���� ��ư ������ 3D ������Ʈ�� ���� �ϳ��� ������ �ϱ�. ���ӿ�����Ʈ�� ���� ���, �ؽ�Ʈ ������ ���ϰ� �ϱ�. ���� �ؽ�Ʈ�� �ϳ� ���� ����
//���� �ؽ�Ʈ�� foreach �ؼ� 3d ������Ʈ�� ������ �ϱ�. ��� ������ 3d ������Ʈ ������

//���� ǥ���ϴ� �Լ� : ���� ���� ��ư ������ setactivate true -> true�� ������ �ؼ� �÷��� ����, ���� 1�ʾ� ��Ÿ����.  


