using UnityEngine;

// モーション用スクリプト
// 2018.04.30 R.Suzuki 新規作成
public class PlayerMotion : MonoBehaviour {

    // 非公開変数
    private Animator animator; // アニメーター

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>(); // アニメーターの取得
	}
	
	// Update is called once per frame
	void Update () {
        ExecHorizontal();   // 水平方向
        ExecVertical();     // 垂直方向
        animator.SetBool("Jump", Input.GetButton("Jump"));      // ジャンプ
        animator.SetBool("Boost", Input.GetButton("Boost"));    // ブースト
    }

    // 水平方向のキー入力
    private void ExecHorizontal()
    {
        float fHorizontal = Input.GetAxis("Horizontal");    // 水平方向のキー入力情報を取得
        if (fHorizontal < 0)        // 負数の場合、move_left
        {
            animator.SetInteger("Horizontal", -1);
        }
        else if (0 == fHorizontal)  // 0の場合、idle
        {
            animator.SetInteger("Horizontal", 0);
        }
        else                        // 正数の場合、move_right
        {
            animator.SetInteger("Horizontal", 1);
        }
    }

    // 垂直方向のキー入力
    private void ExecVertical()
    {
        float fVertical = Input.GetAxis("Vertical");    // 垂直方向のキー入力情報を取得
        if (fVertical < 0)        // 負数の場合、move_back
        {
            animator.SetInteger("Vertical", -1);
        }
        else if (0 == fVertical)  // 0の場合、idle
        {
            animator.SetInteger("Vertical", 0);
        }
        else                      // 正数の場合、move_forward
        {
            animator.SetInteger("Vertical", 1);
        }
    }
}
