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
        private Transform playersTran;//プレイヤーの親の位置情報

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//初期設定用インターフェイスのリスト

        /// <summary>
        /// シーン遷移直後に呼び出される
        /// </summary>
        /// <returns>待ち時間</returns>
        private IEnumerator Start()
        {
            //プレイヤーのゲームオブジェクトを生成する
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTranDetail.transform.position, Quaternion.identity);

            //生成したオブジェクトの初期設定を行う
            objPlayer.GetComponent<PlayerController>().SetUp();

            //生成したオブジェクトの親を設定する
            objPlayer.transform.SetParent(playersTran);

            //生成したオブジェクトを非活性化する
            objPlayer.SetActive(false);

            //スポーン可能になるまで待つ
            yield return new WaitUntil(() => spawnTranDetail.CheckCanSpawn());

            //生成したオブジェクトを活性化する
            objPlayer.SetActive(true);

            //カメラの親を設定する
            Camera.main.transform.SetParent(objPlayer.transform);

            //カメラの位置を設定する
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //各クラスの初期設定を行う
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
