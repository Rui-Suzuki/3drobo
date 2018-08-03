using UnityEngine;

// パーティクル終了時に自動的に消滅させるスクリプト
// 2018.05.01 R.Suzuki 新規作成
public class ParticleAutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.main.duration);  // 本だとparticleSystem.durationだが、duplicated
    }
	
	// Update is called once per frame
	void Update () {
	}
}
