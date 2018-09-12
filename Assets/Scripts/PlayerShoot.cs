using UnityEngine;

// シューティング用スクリプト
// 2018.05.01 R.Suzuki 新規作成 続子守り中
public class PlayerShoot : MonoBehaviour {

    // 定数定義
    private const float shotIntervalMax = 0.5f;  // 発射間隔

    // 公開変数
    public GameObject shot;                       // 弾用オブジェクト
    public GameObject muzzle;                     // 銃口オブジェクト
    public GameObject muzzleFlash;                // 銃口の発光オブジェクト

    // 非公開変数
    private float shotInterval = 0;               // 発射間隔計算用変数
    private AudioSource audioSrc;                 // 発射音

	// Use this for initialization
	void Start () {
        audioSrc = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        shotInterval += Time.deltaTime;              // 経過時間を加算
        if (shotIntervalMax < shotInterval)          // 発射間隔をこえていれば発射処理
        {
            if (Input.GetButton("Fire1"))            // 発射する
            {
                Instantiate(shot,                    // 複製するオブジェクト 弾
                    muzzle.transform.position,       // 複製する位置 銃口の位置に合わせる
                    Camera.main.transform.rotation); // 複製する向き カメラの向きに合わせる

                Instantiate(muzzleFlash,             // 複製するオブジェクト 銃口の発光
                    muzzle.transform.position,       // 複製する位置 銃口
                    transform.rotation);             // 複製する向き
                audioSrc.PlayOneShot(audioSrc.clip); // 発射音再生

                shotInterval = 0;                    // 発射後、初期化する
            }
        }
	}
}
