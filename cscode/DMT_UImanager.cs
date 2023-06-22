using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DMT_UImanager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Pannel;
    public Button button;
    public bool isGameStart;
    void Start()
    {
        isGameStart = false;//시작 전에 매번 이렇게 초기화 됨
        button.onClick.AddListener(OnButtonClick);//여기서 게임 시작 버튼 눌리면 true 됨
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonClick()
    {
        button.gameObject.SetActive(false);
        Pannel.SetActive(false);
        isGameStart = true;
    }
}
