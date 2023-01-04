using Photon.Pun;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Transform spawnTran;//スポーン地点

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//初期設定用インターフェイスのリスト

        /// <summary>
        /// シーン遷移直後に呼び出される
        /// </summary>
        private void Start()
        {
            //プレイヤーのゲームオブジェクトを生成する
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTran.position, Quaternion.identity);

            //生成したオブジェクトの初期設定を行う
            objPlayer.GetComponent<PlayerController>().SetUp();

            //カメラの親を設定する
            Camera.main.transform.SetParent(objPlayer.transform);

            //カメラの位置を設定する
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //BGMを再生する
            SoundManager.instance.PlaySound(SoundDataSO.SoundName.BGM,true, GameData.instance.bgmVolume);

            //各クラスの初期設定を行う
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
