using UnityEngine;

// キャラの回転クラス
// 2018.04.30 R.Suzuki 新規作成 香穂のキーボードバンバン攻撃に耐えながら…
public class PlayerRotate : MonoBehaviour {

    // 非公開変数
    private GameObject cameraParent;        // カメラ
    private Quaternion defaultCameraRot;    // デフォルトのカメラの角度
    private float timer = 0;                // タイマー

	// Use this for initialization
	void Start () {
        cameraParent = Camera.main.transform.parent.gameObject;
        defaultCameraRot = cameraParent.transform.localRotation;    // カメラの初期方向を保持
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);                // プレイヤーを回転
        GameObject cCameraParent = Camera.main.transform.parent.gameObject;  // カメラの親オブジェクトを取得
        cCameraParent.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);    // カメラを回転

        if (Input.GetButton("CamReset"))
        {
            timer = 0.5f;
        }

        if (0 < timer)
        {
            cameraParent.transform.localRotation = Quaternion.Slerp(cameraParent.transform.localRotation,
                                                                    defaultCameraRot,
                                                                    Time.deltaTime * 10);
            timer -= Time.deltaTime;
        }
    }
}
