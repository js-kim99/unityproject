
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Pannel;
    public Button button;

    public bool isGameStart;



    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

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
