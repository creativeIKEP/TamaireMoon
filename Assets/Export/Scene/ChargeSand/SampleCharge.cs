using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharge : MonoBehaviour {

    ChargeManager chargeManager;

	// Use this for initialization
	void Start () {
        chargeManager = ChargeManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            int currentCount = chargeManager.getCount();
            chargeManager.setCount(++currentCount);

            Debug.Log("（プラス後の）チャージ量：" + chargeManager.getCount());
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            int currentCount = chargeManager.getCount();
            chargeManager.setCount(--currentCount);

            Debug.Log("（マイナス後の）チャージ量：" + chargeManager.getCount());
        }
	}
}
