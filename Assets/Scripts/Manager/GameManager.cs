using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TNRD;
using UniRx.Triggers;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private SpawnTranDetail spawnTranDetail;//SpawnTranDetail

        [SerializeField]
        private PictureManager pictureManager;//PictureManager

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//初期設定用インターフェイスのリスト

        private bool joinedRoom;//ルームに参加したかどうか

        /// <summary>
        /// ゲーム開始直後に呼び出される
        /// </summary>
        /// <returns>待ち時間</returns>
        private IEnumerator Start()
        {
            //プレイヤーのスポーンが完了するまで待つ
            yield return StartCoroutine(SpawnPlayer());

            //各クラスの初期設定を行う（ルーム参加後）
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }

        /// <summary>
        /// プレイヤーをスポーンさせる
        /// </summary>
        /// <returns>待ち時間</r
        public IEnumerator SpawnPlayer()
        {
            //ルームに参加するまで待つ
            yield return new WaitUntil(() => joinedRoom);

            //プレイヤーのゲームオブジェクトを生成する
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTranDetail.transform.position, Quaternion.identity);

            //生成したオブジェクトの初期設定を行う
            objPlayer.GetComponent<PlayerController>().SetUp();

            //スポーン可能になるまで待つ
            yield return new WaitUntil(() => spawnTranDetail.CheckCanSpawn());

            //カメラの親を設定する
            Camera.main.transform.SetParent(objPlayer.transform);

            //カメラの位置を設定する
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);
        }
    }
}
