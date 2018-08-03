using UnityEngine;
using UnityEngine.UI;

// レーダーのマーカー制御
// 2018.05.13 R.Suzuki 新規作成 GW前にも触った気がするが気のせいだろう
public class RadarMarker : MonoBehaviour {

    // 定数定義
    private const int enableDistanceMax = 150;  // レーダーに表示する範囲

    // 公開変数
    public Image markerImage;   // マーカーの画像

    // 非公開変数
    private Image marker;       // 複製後のオブジェクト
    private GameObject compass; // コンパスのオブジェクト
    private GameObject target;  // ターゲットのオブジェクト

	// Use this for initialization
	void Start () {
        target = GameObject.Find("PlayerTarget");
        compass = GameObject.Find("CompassMask");
        marker = Instantiate(markerImage,                   // マーカーの画像
                             compass.transform.position,    // コンパスの座標
                             Quaternion.identity) as Image;
        marker.transform.SetParent(compass.transform,       // コンパスの子オブジェクト
                                   false);                  // スケールを維持		
	}
	
	// Update is called once per frame
	void Update () {
        // マーカーをプレイヤーの相対位置に配置
        Vector3 position = transform.position - target.transform.position;
        marker.transform.localPosition = new Vector3(position.x, position.z, 0);
    }

    private void OnDestroy()
    {
        Destroy(marker);    // オブジェクト破棄時にマーカーも消す
    }
}
