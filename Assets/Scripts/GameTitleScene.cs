using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 2018.06.20 R.Suzuki 新規作成 W杯初戦勝利
public class GameTitleScene : MonoBehaviour {
    // 公開変数
    public Text blinkText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main");
        }
        blinkText.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1)); // 点滅
	}
}
