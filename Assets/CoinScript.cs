using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public Text Coin_text;

    void Start()
    {
        Coin_text.text = "코인 : " + PlayerController.coin + " / " + PlayerController.maxCoin;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 180f * Time.deltaTime));   // 코인 회전
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.coin++;
            Destroy(this.gameObject);
            Coin_text.text = "코인 : " + PlayerController.coin + " / " + PlayerController.maxCoin;
        }
    }
}
