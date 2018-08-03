using UnityEngine;

// 敵の弾のスクリプト
// 2018.05.02 R.Suzuki 新規作成
public class EnemyBullet : MonoBehaviour {

    // 公開変数
    public GameObject explosion;    // 爆発用オブジェクト

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2.0F);  // 表示から2秒後に消滅
    }

    // Update is called once per frame
    void Update()
    {
        // 弾を進める
        transform.position += transform.forward * Time.deltaTime * 100;
    }

    // 衝突判定 他のcollider/rigidbodyに接触した時
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);             // 地形と接触したら消滅
            Instantiate(explosion,           // 複製するオブジェクト
                        transform.position,  // 複製する位置   
                        transform.rotation); // 複製する角度
        }
    }
}
