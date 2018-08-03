using UnityEngine;

// 銃口操作のスクリプト
// 2018.05.02 R.Suzuki 新規作成
public class MuzzleAction : MonoBehaviour {

    // 非公開変数
    private float destroyTime = 0.1F;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, destroyTime);  // 時間経過で破棄
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
