using Photon.Pun;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpBeforeJoinRoomList = new();//初期設定用インターフェイスのリスト（ルーム参加前）

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpAfterJoinRoomList = new();//初期設定用インターフェイスのリスト（ルーム参加後）

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //各クラスの初期設定を行う（ルーム参加前）
            for (int i = 0; i < setUpBeforeJoinRoomList.Count; i++) { setUpBeforeJoinRoomList[i].Value.SetUp(); }
        }

        /// <summary>
        /// ゲームサーバーへの接続が成功した際に呼び出される
        /// </summary>
        public override void OnJoinedRoom()
        {
            //プレイヤーのゲームオブジェクトを生成する
            GameObject objPlayer =
                PhotonNetwork.Instantiate(GameData.instance.ObjPlayerPrefab.name, GameData.instance.SpawnTran.position, Quaternion.identity);

            //生成したオブジェクトの初期設定を行う
            objPlayer.GetComponent<PlayerController>().SetUp();

            //カメラの親を設定する
            Camera.main.transform.SetParent(objPlayer.transform);

            //カメラの位置を設定する
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //各クラスの初期設定を行う（ルーム参加後）
            for (int i = 0; i < setUpAfterJoinRoomList.Count; i++) { setUpAfterJoinRoomList[i].Value.SetUp(); }
        }
    }
}
