using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kago : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        //衝突判定
        if (collision.gameObject.tag == "Ball")
        {
            //スコア処理を追加
            FindObjectOfType<Score>().AddPoint(10);

            //Ballを消す
            Destroy(collision.gameObject);
        }

        //衝突判定
        if (collision.gameObject.tag == "enemyBall")
        {
            //スコア処理を追加
            FindObjectOfType<EnemyScore>().AddPoint(10);

            //Ballを消す
            Destroy(collision.gameObject);
        }
    }


}
