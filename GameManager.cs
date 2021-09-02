using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject texto;
    bool pausado;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
    	if(Time.timeScale == 1){
			texto.SetActive(false);
		    Time.timeScale=0;
    	}else{
    		texto.SetActive(true);
    		Time.timeScale=1;
    		}
	    }
	}
    }
   

