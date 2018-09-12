using UnityEngine;

// 敵キャラクターの制御スクリプト
// 2018.05.02 R.Suzuki 新規作成
public class Enemy : MonoBehaviour {
    // 定数定義
    private const float reactDistance    = 30;      // 敵が反応し始める距離
    private const float shotIntervalMax  = 1.0F;    // 弾発射間隔の閾値
    private const int apMax              = 100;     // APの最大値
    private const int enemyMoveTime      = 3;       // 敵が動き出す時間
    private const int rotateSpeedHigh    = 10;      // 高速旋回
    private const int rotateSpeedLow     = 5;       // 低速旋回
    private const int moveEnemySpeed     = 20;      // 敵の移動速度

    // 公開変数
    public GameObject shot;                 // 弾
    public GameObject exprosion;            // 爆発
    public int ap;                          // AP

    // 非公開変数
    private int damage = 100;               // ダメージ量
    private GameObject target;
    private float shotInterval = 0;         // 経過時間計測用
    private float timer = 0;                // 敵の行動変更タイマー
    private bool enemyMoveFlg = false;      // 敵が動くかどうかのフラグ

	// Use this for initialization
	void Start () {
        target = GameObject.Find("PlayerTarget");   // ターゲットを検索
        ap = apMax;                                 // AP初期化
	}
	
	// Update is called once per frame
	void Update () {
        judgeEnemyMove();

        float distance
            = Vector3.Distance(target.transform.position, transform.position);   // プレーヤーと敵の距離
        if (distance <= reactDistance)           // 反応する距離内
        {
            rotateWithSlerp(rotateSpeedHigh);
            shotInterval += Time.deltaTime;      // 経過時間を加算
            if (shotIntervalMax < shotInterval)  // 経過時間が閾値を過ぎていた場合
            {
                Instantiate(shot, transform.position, transform.rotation);
                shotInterval = 0;
            }
        }
        else
        {
            if (enemyMoveFlg == true)
            {
                rotateWithSlerp(rotateSpeedLow);
                transform.position += transform.forward * Time.deltaTime * moveEnemySpeed;
            }
        }
    }

    // 衝突判定 他のcollider/rigidbodyに接触した時
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            damage = collision.gameObject.GetComponent<PlayerBullet>().damage;  // 弾からダメージを取得
            ap -= damage;

            if (ap <= 0) // APが0以下で消滅
            {
                Destroy(gameObject);
                Instantiate(exprosion,              // 複製するオブジェクト
                            transform.position,     // 複製する位置
                            transform.rotation);    // 複製する時の角度
                GameManager.score++;                // リザルト用のスコアを加算
            }
        }
    }

    // 敵が動くかどうかのフラグ判定
    private void judgeEnemyMove()
    {
        timer += Time.deltaTime;

        if (enemyMoveTime < timer)  // 一定時間経過後、敵が動き出す
        {
                                                                                                           // enemyMoveFlg = true;
        }
    }

    // スムーズにターゲット方向に旋回(補間しつつ旋回)
    private void rotateWithSlerp(int time)
    {
        Quaternion targetRotation            // 現在地からターゲット位置に向いた状態の向き
            = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,     // 現在の向き
                                              targetRotation,         // 目標の向き
                                              Time.deltaTime * time); // 回転スピード
    }
}
