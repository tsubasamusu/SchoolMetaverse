using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// 変動値を管理する
    /// </summary>
    public class GameData : MonoBehaviour
    {
        [HideInInspector]
        public string playerName;//プレイヤーの名前

        [HideInInspector]
        public string[] messages = new string[ConstData.MAX_MESSAGE_LINES];//メッセージの配列

        public static GameData instance;//インスタンス

        /// <summary> 
        /// Startメソッドより前に呼び出される 
        /// </summary> 
        private void Awake() { if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); } }

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //プレイヤーの名前が既に保存されているなら、それを取得する
            if (PlayerPrefs.HasKey("PlayerName")) playerName = PlayerPrefs.GetString("PlayerName");
        }

        /// <summary>
        /// デバイスにプレイヤーの名前を保存する
        /// </summary>
        public void SavePlayerNameInDevice() { PlayerPrefs.SetString("PlayerName", playerName); }
    }
}
