using UnityEngine;
using UnityEngine.UI;

// プレイヤーのAP制御
// 2010.05.13 R.Suzuki 新規作成
public class PlayerAp : MonoBehaviour {

    // 定数定義
    private const int armorPointMax = 5000; // APの最大値
    private const int damage = 100;         // ダメージ(とりあえず固定)
    private const float armorPointPerYellow = 0.4F; // APを黄色表示にする割合
    private const float armorPointPerRed = 0.2F;    // APを赤色表示にする割合

    // 公開変数
    public Image gaugeImg;                  // APゲージ表示用
    public Text  armorText;                 // AP表示用テキスト
    public int   dispArmorPoint;            // APの動的減少表示用
    public static int   armorPoint;         // APの管理用変数

    // 非公開変数

    // Use this for initialization
    void Start () {
        armorPoint = armorPointMax;         // APを最大値で初期化
        dispArmorPoint = armorPoint;        // 動的表示の値もAPの値で初期化
    }
	
	// Update is called once per frame
	void Update () {
        if (dispArmorPoint != armorPoint)   // 表示しているAPと実際のAPに差がある？
        {
            dispArmorPoint = (int)Mathf.Lerp(dispArmorPoint, armorPoint, 0.1F);
        }
        armorText.text =    // APの表示を更新
            string.Format("{0:0000} / {1:0000}", dispArmorPoint, armorPointMax);
        float perAP = (float)dispArmorPoint / armorPointMax;    // 残APの割合
        changeTextColor(perAP);           // APの表示色を変える
        gaugeImg.transform.localScale = new Vector3(perAP, 1, 1);   // APゲージの長さを変える
    }

    // AP表示の色を変える
    private void changeTextColor(float perAP)
    {
        Color setColor = Color.white;           // デフォルトは白
        if (perAP < armorPointPerRed)           // 死にかけ
        {
            setColor = Color.red;
        }
        else if (perAP < armorPointPerYellow)   // 危険域
        {
            setColor = Color.yellow;
        }
        armorText.color = setColor;             // APのテキスト
        gaugeImg.color = setColor;              // APゲージ
    }

    // 接触時
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShotEnemy")
        {
            armorPoint -= damage;
            armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax); // 0～最大値の間に値を制限
        }
    }
}
