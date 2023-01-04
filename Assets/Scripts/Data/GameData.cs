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
        public float lookSensitivity = 5f;//視点感度（0～10）

        [HideInInspector]
        public float bgmVolume = 1f;//BGMの音量

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
            //デバイスに保存されているデータを取得する
            if (PlayerPrefs.HasKey("PlayerName")) playerName = PlayerPrefs.GetString("PlayerName");
            if (PlayerPrefs.HasKey("LookSensitivity")) lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity");
            if (PlayerPrefs.HasKey("BgmVolume")) lookSensitivity = PlayerPrefs.GetFloat("BgmVolume");
        }

        /// <summary>
        /// デバイスにプレイヤーの名前を保存する
        /// </summary>
        public void SavePlayerNameInDevice() { PlayerPrefs.SetString("PlayerName", playerName); }

        /// <summary>
        /// デバイスに視点感度を保存する
        /// </summary>
        public void SavelookSensitivityInDevice() { PlayerPrefs.SetFloat("LookSensitivity", lookSensitivity); }

        /// <summary>
        /// デバイスにBGMの音量を保存する
        /// </summary>
        public void SaveBgmVolumeInDevice() { PlayerPrefs.SetFloat("BgmVolume", bgmVolume); }
    }
}
