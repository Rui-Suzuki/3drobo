using UnityEngine;

// 弾丸用スクリプト
// 2018.05.01 R.Suzuki 子守り中
public class PlayerBullet : MonoBehaviour {

    // 定数定義
    private const int damageMin = 1; // 最小の威力

    // 公開変数
    public GameObject explosion;    // 爆発用オブジェクト
    public int damage = 200;        // ダメージの初期値

    // Use this for initialization
    void Start () {
        Destroy(gameObject, 2.0F);  // 表示から2秒後に消滅
	}
	
	// Update is called once per frame
	void Update () {
        // 弾を進める
        transform.position += transform.forward * Time.deltaTime * 100;

        // 威力の減衰処理
        damage--;
        if (damage <= damageMin)
        {
            damage = damageMin;
        }
	}

    // 衝突判定 他のcollider/rigidbodyに接触した時
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain") // 地面と接触
        {
            DestroyCom();
        }

        if (collision.gameObject.tag == "Enemy")	// 敵と接触
        {
            DestroyCom();        }
    }

    // 消滅の共通処理
    private void DestroyCom()
    {
        Destroy(gameObject);    // 地形と接触したら消滅
        Instantiate(explosion,           // 複製するオブジェクト
                    transform.position,  // 複製する位置   
                    transform.rotation); // 複製する角度
    }
}
