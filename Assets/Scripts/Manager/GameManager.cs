using Photon.Pun;
using System;
using System.Collections.Generic;
using TNRD;
using UniRx;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SpawnTranDetail spawnTranDetail;//SpawnTranDetail

        [SerializeField]
        private PictureManager pictureManager;//PictureManager

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//初期設定用インターフェイスのリスト

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        private void Start()
        {
            //プレイヤーのゲームオブジェクトを生成する
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTranDetail.transform.position, Quaternion.identity);

            //PlayerControllerを取得する
            PlayerController playerController=objPlayer.GetComponent<PlayerController>();

            //所有権を要請する
            GameData.instance.RequestOwnership();

            //生成したオブジェクトの初期設定を行う
            playerController.SetUp();

            //カメラの親を設定する
            Camera.main.transform.SetParent(objPlayer.transform);

            //カメラの位置を設定する
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //各クラスの初期設定を行う（ルーム参加後）
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
