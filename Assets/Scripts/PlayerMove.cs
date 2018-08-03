using UnityEngine;
using UnityEngine.UI;

// プレイヤー操作クラス
// 2018.04.29 R.Suzuki 新規作成 香穂のキーボードバンバン攻撃に耐えながら…
public class PlayerMove : MonoBehaviour {

    // 定数定義
    private const float boostSpeed      = 2.0F;     // ブーストの加速倍率
    private const float speed           = 18.0F;    // 移動速度
    private const float moveSpeedMax    = 20;       // 通常の最大速度
    private const float boostSpeedMax   = 40;       // ブースト時の最大速度
    private const float addSpeedNormal  = 1;        // 通常の加速度
    private const float addSpeedBoost   = 2;        // ブースト時の加速度
    private const float jumpSpeed       = 8.0F;     // ジャンプの速度
    private const float gravity         = 20.0F;    // 落下速度
    private const int   maxMoveY        = 100;      // Y軸方向最高到達点
    private const int   boostPointMax   = 100;      // ブーストの最大容量
    private const int   boostPointMin   = 1;        // ブーストの最小容量

    // 公開変数
    public Image gaugeImage;                        // ブーストゲージ表示用
    public int boostPoint;                          // ブーストの容量

    // 非公開変数
    private Vector3 moveDirection = Vector3.zero;   // 移動方向
    private Vector3 moveSpeed;                      // 移動速度
    private bool isBoost;                           // ブースト中かどうか

	// Use this for initialization
	void Start () {
        boostPoint = boostPointMax;
        moveSpeed = Vector3.zero;
        isBoost = false;
    }

    // 左右方向の移動
    private void moveHorizontal(CharacterController controller)
    {
        Vector3 targetSpeed = Vector3.zero; // 目標速度
        Vector3 addSpeed = Vector3.zero;    // 加速度

        // 左右移動の目標速度と加速速度の設定
        if (Input.GetAxis("Horizontal") == 0)    // 非押下時
        {
            targetSpeed.x = 0;  // 目標速度0
            if (controller.isGrounded)          // 接地中
            {
                addSpeed.x = addSpeedNormal;
            }
            else                                // 空中
            {
                addSpeed.x = addSpeedNormal / 4;
            }
        }
        else                                    // 水平方向キー押下時
        {
            if (isBoost)                        // ブースト中
            {
                targetSpeed.x = boostSpeedMax;  // ブーストの最大速が目標速度
                addSpeed.x = addSpeedBoost;
            }
            else                                // 通常時
            {
                targetSpeed.x = moveSpeedMax;
                addSpeed.x = addSpeedNormal;
            }
            targetSpeed.x *= Mathf.Sign(Input.GetAxis("Horizontal"));   // Sign 正数:1、負数:-1
        }

        // 左右移動の速度
        moveSpeed.x = Mathf.MoveTowards(moveSpeed.x,    // 現在値
                                        targetSpeed.x,  // 目標値
                                        addSpeed.x);    // 加速値
        moveDirection.x = moveSpeed.x;
    }

    // 前後方向の移動
    private void moveVertical(CharacterController controller)
    {
        Vector3 targetSpeed = Vector3.zero; // 目標速度
        Vector3 addSpeed = Vector3.zero;    // 加速度

        if (Input.GetAxis("Vertical") == 0)
        {
            targetSpeed.z = 0;
            if (controller.isGrounded)      // 接地中
            {
                addSpeed.z = addSpeedNormal;
            }
            else
            {
                addSpeed.z = addSpeedNormal / 4;
            }
        }
        else
        {
            if (isBoost)                    // ブースト中
            {
                targetSpeed.z = boostSpeedMax;
                addSpeed.z = addSpeedBoost;
            }
            else                            // 通常時
            {
                targetSpeed.z = moveSpeedMax;
                addSpeed.z = addSpeedNormal;
            }
            targetSpeed.z *= Mathf.Sign(Input.GetAxis("Vertical"));   // Sign 正数:1、負数:-1
        }

        // 水平方向移動の速度
        moveSpeed.z = Mathf.MoveTowards(moveSpeed.z,    // 現在値
                                        targetSpeed.z,  // 目標値
                                        addSpeed.z);    // 加速値
        moveDirection.z = moveSpeed.z;
    }
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection.y = 0;
        }
        if (Input.GetButton("Boost") && boostPointMin < boostPoint)  // ブースト押下中かつブーストが残っている
        {
            boostPoint -= 1;    // ブーストを消費
            isBoost = true;     // ブースト中フラグオン
        }
        else
        {
            isBoost = false;
        }
        moveHorizontal(controller); // 左右方向の移動
        moveVertical(controller);   // 水平方向の移動
        moveDirection = transform.TransformDirection(moveDirection);

        bool isJump = false;
        if (Input.GetButton("Jump") && boostPointMin < boostPoint)  // ジャンプ＋垂直方向ブースト
        {
            if (maxMoveY < transform.position.y) // 最高到達点より上にいる
            {
                moveDirection.y = 0;             // 動かさない
            }
            else
            {
                moveDirection.y += gravity * Time.deltaTime;
            }
            isJump = true;
            boostPoint -= 1;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;    // 自由落下
        }

        if (isBoost == false && isJump == false)
        {
            boostPoint += 2;    // ブースト容量回復
        }
        boostPoint = Mathf.Clamp(boostPoint, 0, boostPointMax);
        controller.Move(moveDirection * Time.deltaTime);
        gaugeImage.transform.localScale = new Vector3((float)boostPoint / boostPointMax, 1, 1);
    }
}
