using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopUp : MonoBehaviour
{
    public bool isEnable=false;


    // Update is called once per frame
    void Update()
    {
        if(isEnable & Input.GetKeyDown(KeyCode.F)){
            closePopup();
        }
        
    }
    private void OnEnable()
    {
        isEnable=true;

    }

    public void closePopup(){
        this.gameObject.SetActive(false);

    }

}
