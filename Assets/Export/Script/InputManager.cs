using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour {

	/// <summary>
	/// コントローラのイベントをこんな感じで集約すると良い（委譲先）
	/// </summary>

    public event Action OnClickStartButton;

    void Update(){
        // Daydreamボタンを押した瞬間True
        if(GvrControllerInput.HomeButtonDown){
            Debug.Log("HomeButtonDown");
        }

        // ポジションリセンターした瞬間True（Daydreamボタンの長押し）
        if(GvrControllerInput.Recentered){
            Debug.Log("Recentered");
        }

        // アプリボタンを押した瞬間True
        if(GvrControllerInput.AppButtonDown){
            Debug.Log("AppButtonDown");
        }
        // アプリボタンを押した指を離した瞬間True
        if(GvrControllerInput.AppButtonUp){
            Debug.Log("AppButtonUp");
        }

        // タッチパッドにタッチした瞬間True
        if(GvrControllerInput.TouchDown){
            Debug.Log("TouchDown");
        }
        // タッチパッドから指が離れた瞬間True
        if(GvrControllerInput.TouchUp){
            Debug.Log("TouchUp");
        }

    }
}
