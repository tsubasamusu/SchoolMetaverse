using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameData : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Transform spawnTran;//スポーン地点

        /// <summary>
        /// 「スポーン地点」の取得用
        /// </summary>
        public Transform SpawnTran { get => spawnTran; }

        public static GameData instance;//インスタンス 

        /// <summary>
        /// GameDataの初期設定を行う
        /// </summary>
        public void SetUp()
        {
            //TODO:GameDataクラスの初期設定処理
        }

        /// <summary> 
        /// Startメソッドより前に呼び出される 
        /// </summary> 
        private void Awake()
        {
            //以下、シングルトンに必須の記述 
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
