using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // 定数定義
    enum battleState        // 戦闘の状態
    {
        battleStart = 0,    // 開始前
        battlePlay,         // 戦闘中
        battleEnd,          // 終了
    }

    private const double timeEnableMsg = 3.0;    // メッセージの表示時間

    // 公開変数
    public Image messageStart;  // 開始メッセージの画像
    public Image messageWin;    // 勝利メッセージの画像
    public Image messageLose;   // 敗北メッセージの画像
    public static int score;    // 点数を外から加算する用

    // 非公開変数
    private battleState state;  // ゲームの状態
    private float timer;        // メッセージの表示時間カウント用
    private int clearScore;     // クリア条件のスコア

    // Use this for initialization
    void Start () {
        state = battleState.battleStart;
        timer = 0;
        messageStart.enabled = true;
        messageWin.enabled   = false;
        messageLose.enabled  = false;
        score = 0;
        clearScore = EnemyInstantiate.instantiateValue;
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case battleState.battleStart:   // 開始時
                timer += Time.deltaTime;
                if (timeEnableMsg < timer)
                {
                    messageStart.enabled = false;
                    state = battleState.battlePlay;
                    timer = 0;
                }
                break;
            case battleState.battlePlay:    // プレー中
                if (clearScore <= score)    // クリアスコアをこえた
                {
                    state = battleState.battleEnd;
                    messageWin.enabled = true;
                }
                if (PlayerAp.armorPoint <= 0) // ゲームオーバー
                {
                    state = battleState.battleEnd;
                    messageLose.enabled = true;
                }
                break;
            case battleState.battleEnd:     // 終了処理
                timer += Time.deltaTime;
                if (timeEnableMsg < timer)
                {
                    Time.timeScale = 0;     // 動きを止める
                    if (Input.GetButtonDown("Fire1"))
                    {
                        SceneManager.LoadScene("Title");
                        Time.timeScale = 1; // 動きを再開
                    }
                }
                break;
            default:
                break;
        }
	}
}
