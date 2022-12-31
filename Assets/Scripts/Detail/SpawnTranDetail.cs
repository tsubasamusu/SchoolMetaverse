using Photon.Pun;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// スポーン地点に関する処理を行う
    /// </summary>
    public class SpawnTranDetail : MonoBehaviour
    {
        /// <summary>
        /// スポーン可能かどうか調べる
        /// </summary>
        /// <returns>スポーン可能ならtrue</returns>
        public bool CheckCanSpawn()
        {
            //ルームに参加中の他のプレイヤーの数だけ繰り返す
            for (int i=0;i<PhotonNetwork.PlayerListOthers.Length;i++) 
            {
                //繰り返し処理で取得したプレイヤーにPlayerControllerがセットされているなら
                if (PhotonNetwork.PlayerListOthers[i].CustomProperties["PlayerController"] is PlayerController player)
                {
                    //繰り返し処理で取得したプレイヤーがスポーン地点にいるなら、falseを返す
                    if (player.transform.position.z < ConstData.MINI_POS_Z) return false;
                }
                //繰り返し処理で取得したプレイヤーにPlayerControllerがセットされていないなら
                else
                {
                    //エラーを表示する
                    Debug.LogError("PlayerクラスのカスタムプロパティにPlayerControllerをセットしてください");
                }
            }

            //trueを返す
            return true;
        }
    }
}
