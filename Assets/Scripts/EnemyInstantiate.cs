using UnityEngine;

// 2018.06.19 R.Suzuki 新規作成 家で作ってたら怒られたので学校で。。。
public class EnemyInstantiate : MonoBehaviour {

    // 定数定義
    private const int enemyNumMax       = 20;   // 敵の数
    private const int timeInit          = 3;    // タイマーの初期値
    private const float intervalInit    = 4.0f; // 生成間隔の初期値
    private const float timerAddVal     = 0.05f; // タイマーの短縮度合い

    // 公開変数
    public static int instantiateValue  = 20;   // 生成する残数
    public GameObject enemy;                    // 敵オブジェクト

    // 非公開変数
    private float timer;                        // 時間計算用変数
    private float instantiateInterval;          // 生成する間隔
    private int   enemyNum;                     // 生成する残数

    private void Awake()
    {
        enemyNum = instantiateValue;            // 残機設定
    }

    // Use this for initialization
    void Start () {
        instantiateInterval = intervalInit;     // 生成間隔設定
        timer = timeInit;                       // 生成間隔計算用タイマー初期化
	}

	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;     // 経過時間を減算

        if (timer < 0)               // 規定時間経過
        {
            if (0 < enemyNum)        // 敵残機が残っている
            {
                // 敵を配置
                Instantiate(enemy, new Vector3(Random.Range(10.0f, 200.0f),     // X
                                               Random.Range(10, 30.0f),         // Y
                                               Random.Range(10.0f, 200.0f)),    // Z
                                               Quaternion.identity);
                enemyNum--;          // 残機を減らす
            }

            instantiateInterval -= timerAddVal; // 生成を加速
            instantiateInterval = Mathf.Clamp(instantiateInterval, 1.0f, float.MaxValue);

            timer = instantiateInterval;
        }
    }
}
