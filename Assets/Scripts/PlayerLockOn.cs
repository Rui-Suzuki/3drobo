using UnityEngine;
using UnityEngine.UI;

// ロックオン制御のスクリプト
// 2018.05.03 R.Suzuki 新規作成
public class PlayerLockOn : MonoBehaviour {

    // 定数定義
    private const int unlockDistance = 100;     // ロックを解除する距離
    private const int curRotateSpeed = 100;     // ロックオンカーソルの回転速度

    // 公開変数
    public Image lockOnImg;

    // 非公開変数
    private GameObject target        = null;    // ターゲットオブジェクト
    private bool isSearch;                      // サーチモードON/OFF

	// Use this for initialization
	void Start () {
        isSearch = false;
        lockOnImg.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Lock"))    // ロックボタン押下
        {
            isSearch = !isSearch;   // ON/OFF切り替え
            if (!isSearch)          // サーチモードではない
            {
                Debug.Log("Lock off");
                target = null;      // ターゲットロックを解除
            }
            else                    // サーチモード有効
            {
                Debug.Log("Lock on");
                target = FindClosestEnemy();
            }
        }

        if (isSearch == true)
        {
            if (target != null) // ロック中
            {
                // スムーズに敵の方に向く
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);    // ターゲット方向の指定
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      targetRotation,
                                                      Time.deltaTime * 10);     // 現在と目標角度の間を第三引数で補間
                transform.rotation = new Quaternion(0,                          // X、Z固定でYとWで回転
                                                    transform.rotation.y,
                                                    0,
                                                    transform.rotation.w);

                // カメラをターゲットに向ける 流れは上記と同じ
                Transform cameraParent = Camera.main.transform.parent;
                Quaternion targetRotation2 = Quaternion.LookRotation(target.transform.position - cameraParent.position);
                cameraParent.localRotation = Quaternion.Slerp(cameraParent.localRotation,
                                                              targetRotation2,
                                                              Time.deltaTime * 10);
                cameraParent.localRotation = new Quaternion(cameraParent.localRotation.x,
                                                            0,
                                                            0,
                                                            cameraParent.localRotation.w);
            }
            else
            {
                target = FindClosestEnemy();    // ロック中ではなければ検索
            }

            if (target != null)
            {
                if (IsOutOfRange(target))       // 範囲外ならロック解除
                {
                    target = null;
                }
            }

            if (target != null)
            {
                lockOnImg.transform.rotation = Quaternion.identity;
                lockOnImg.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);   // ターゲットの表示位置にカーソルを合わせる
            }
            else
            {
                lockOnImg.transform.Rotate(0, 0, Time.deltaTime * curRotateSpeed);
            }
            lockOnImg.enabled = isSearch;       // 表示を更新
        }
    }

    // 最も近い敵を取得
    private GameObject FindClosestEnemy()
    {
        GameObject[] gameObjArray;
        gameObjArray = GameObject.FindGameObjectsWithTag("Enemy");  // 敵一覧を取得

        float distance = Mathf.Infinity;        // 最大
        Vector3 position = transform.position;  // 現在地

        GameObject closest = null;              // 最も近い敵
        foreach (GameObject gameObj in gameObjArray)
        {
            Vector3 diff = gameObj.transform.position - position;   // 敵と現在地の距離
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)         // 検索中の物で一番近い
            {
                closest = gameObj;              // 一番近い敵を更新
                distance = curDistance;         // 一番近い距離を更新
            }
        }
        return closest;
    }

    // 敵がロックオン対象外か？
    private bool IsOutOfRange(GameObject gameObj)
    {
        bool ret = true;
        if (gameObj != null)
        {
            if (Vector3.Distance(gameObj.transform.position,
                                transform.position) <= unlockDistance )
            {
                ret = false;    // 範囲内
            }
        }
        return ret;
    }
}
