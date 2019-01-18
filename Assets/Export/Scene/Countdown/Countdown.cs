using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    [SerializeField] Text countDownText;
    [SerializeField] float maxCount = 60;
    float elapsedTime = 0;
    float leftTime;

    bool isTimeOver = false;

    void Update () {
        if(isTimeOver){
            return;
        }
        leftTime = maxCount - elapsedTime;
        countDownText.text = "残り時間： " + Mathf.Round(leftTime) + "秒";
        
        if(leftTime <= 0){
            isTimeOver = true;
            countDownText.text = "FINISH!";
        }
        elapsedTime += Time.deltaTime;
    }
}
