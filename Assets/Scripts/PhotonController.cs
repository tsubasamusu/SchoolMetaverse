using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject objPlayerPrefab;//プレイヤーのオブジェクトのプレファブ

    private bool isConnecting;//マスターサーバーに接続しているかどうか

    /// <summary>
    /// Startメソッドより前に呼び出される
    /// </summary>
    private void Awake()
    {
        //マスタークライアントがシーンをロードしたら、それ以外のクライアントも同じシーンをロードする
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //マスターサーバーに接続したなら
        if (PhotonNetwork.IsConnected)
        {
            //ランダムなルームに入る
            PhotonNetwork.JoinRandomRoom();
        }
        //マスターサーバーに接続できなかったら
        else
        {
            //マスターサーバーへの接続し、その結果を取得する
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// マスターサーバーへの接続が成功した際に呼び出される
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //マスターサーバーに接続しているなら
        if (isConnecting)
        {
            //ルームの設定を作成する
            RoomOptions roomOptions = new();

            //ルームの最大人数を設定する
            roomOptions.MaxPlayers= ConstData.MAX_PLAYERS;

            //「room」に参加するか作成する
            PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);

            //マスターサーバーに接続していない状態に切り替える
            isConnecting = false;
        }
    }

    /// <summary>
    /// ゲームサーバーへの接続が成功した際に呼び出される
    /// </summary>
    public override void OnJoinedRoom()
    {
        //TODO:ゲームサーバーへの接続が成功した際の処理
    }
}
