using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SchoolMetaverse
{
    [RequireComponent(typeof(PhotonView))]
    public class GameData : MonoBehaviour, IPunObservable
    {
        [HideInInspector]
        public string playerName;//プレイヤーの名前

        public static GameData instance;//インスタンス

        private PhotonView photonView;//PhotonView

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

            //PhotonViewを取得する
            photonView = GetComponent<PhotonView>();

            //Requestに変更する
            photonView.OwnershipTransfer = OwnershipOption.Request;
        }

        /// <summary>
        /// デバイスにプレイヤーの名前を保存する
        /// </summary>
        public void SetDevicePlayerName()
        {
            //プレイヤーの名前を保存する
            PlayerPrefs.SetString("PlayerName", playerName);
        }

        /// <summary>
        /// 同期する
        /// </summary>
        /// <param name="stream">PhotonStream</param>
        /// <param name="info">PhotonMessageInfo</param>
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //stream.SendNext(playerControllers);
            }
            else
            {
                //playerControllers = (PlayerController[])stream.ReceiveNext();
            }
        }

        /// <summary>
        /// 所有権を要請する
        /// </summary>
        public void RequestOwnership() { photonView.RequestOwnership(); }
    }
}
