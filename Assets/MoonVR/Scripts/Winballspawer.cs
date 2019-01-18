using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winballspawer : MonoBehaviour
{
    [SerializeField] private Transform ballSpawnPosition;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject enemyBallPreafabs;

    [SerializeField] private float force = 2.50f;
    [SerializeField] private Text ballCountText;

    private void Start()
    {
        StartCoroutine(FuncCoroutine());
        ChargeManager.Instance.setCount(ChargeManager.Instance.maxCount);
    }

    IEnumerator FuncCoroutine()
    {
        while (true)
        {
            // Do anything
#if UNITY_STANDALONE_WIN
        SpawnBall();
#endif
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {

        // クリックボタンは玉を放つ
        if (GvrControllerInput.ClickButtonDown)
        {
            SpawnBall();
        }

        //アプリボタンはチャージ
        if (GvrControllerInput.AppButton)
        {
            ChargeBall();
        }

        if (ballCountText)
        {
            ballCountText.text = "残弾 : " + ChargeManager.Instance.getCount();
        }
    }

    private void SpawnBall()
    {
        if (ballPrefab && ballSpawnPosition)
        {
            // チャージの玉の残量が０以上の場合
            if (0 < ChargeManager.Instance.getCount())
            {
                // 玉を生成して発射
                GameObject ball = Instantiate(ballPrefab, ballSpawnPosition);
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(ballSpawnPosition.forward * force, ForceMode.Impulse);
                Destroy(ball, 60);
                ball.transform.parent = null;
                ball = null;

                ChargeManager.Instance.setCount(
                    ChargeManager.Instance.getCount() - 1);
            }
        }
    }

    private void ChargeBall()
    {
        ChargeManager.Instance.setCount(1);
    }

 
}

