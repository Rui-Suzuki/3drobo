using UnityEngine;

// 2018.05.21 R.Suzuki 新規作成 やっと半分？
public class PlayerBoostEffect : MonoBehaviour {

    // 公開変数
    public GameObject boostLight;   // ブーストのエフェクト用

	// Use this for initialization
	void Start () {
        boostLight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        bool isBoost = false;
        if (Input.GetButton("Boost") // ブースト
        || Input.GetButton("Jump"))  // ジャンプ
        {
            isBoost = true;          // ブースト有効
        }
        boostLight.SetActive(isBoost);
	}
}
