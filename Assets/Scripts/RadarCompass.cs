using UnityEngine;
using UnityEngine.UI;

// レーダーのコンパス
// 2018.05.06 R.Suzuki 新規作成
public class RadarCompass : MonoBehaviour {

    // 公開変数
    public Image compassImage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // コンパスの回転 プレーヤーのY軸の回転情報コンパスのZ軸に渡す
        compassImage.transform.rotation = Quaternion.Euler(compassImage.transform.rotation.x,
                                                           compassImage.transform.rotation.y,
                                                           transform.eulerAngles.y);
		
	}
}
