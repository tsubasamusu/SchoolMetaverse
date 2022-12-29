using UnityEngine;

namespace SchoolMetaverse
{
    public class GameData : MonoBehaviour,ISetUp
    {
        [SerializeField]
        private Transform spawnTran;//スポーン地点

        [SerializeField]
        private GameObject objPlayerPrefab;//プレイヤーのプレファブ

        /// <summary>
        /// 「スポーン地点」の取得用
        /// </summary>
        public Transform SpawnTran { get => spawnTran; }

        /// <summary>
        /// 「プレイヤーのプレファブ」の取得用
        /// </summary>
        public GameObject ObjPlayerPrefab { get => objPlayerPrefab; }

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
