using System.Collections.Generic;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameData : MonoBehaviour
    {
        [HideInInspector]
        public string playerName;//プレイヤーの名前

        [HideInInspector]
        public List<PlayerController> playerControllers = new();//プレイヤーのリスト

        public static GameData instance;//インスタンス

        /// <summary> 
        /// Startメソッドより前に呼び出される 
        /// </summary> 
        private void Awake()
        {
            //以下、シングルトンに必須の記述 
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

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
        public void SetDevicePlayerName()
        {
            //プレイヤーの名前を保存する
            PlayerPrefs.SetString("PlayerName", playerName);
        }
    }
}
