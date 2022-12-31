using Photon.Pun;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �X�|�[���n�_�Ɋւ��鏈�����s��
    /// </summary>
    public class SpawnTranDetail : MonoBehaviour
    {
        /// <summary>
        /// �X�|�[���\���ǂ������ׂ�
        /// </summary>
        /// <returns>�X�|�[���\�Ȃ�true</returns>
        public bool CheckCanSpawn()
        {
            //���[���ɎQ�����̑��̃v���C���[�̐������J��Ԃ�
            for (int i=0;i<PhotonNetwork.PlayerListOthers.Length;i++) 
            {
                //�J��Ԃ������Ŏ擾�����v���C���[��PlayerController���Z�b�g����Ă���Ȃ�
                if (PhotonNetwork.PlayerListOthers[i].CustomProperties["PlayerController"] is PlayerController player)
                {
                    //�J��Ԃ������Ŏ擾�����v���C���[���X�|�[���n�_�ɂ���Ȃ�Afalse��Ԃ�
                    if (player.transform.position.z < ConstData.MINI_POS_Z) return false;
                }
                //�J��Ԃ������Ŏ擾�����v���C���[��PlayerController���Z�b�g����Ă��Ȃ��Ȃ�
                else
                {
                    //�G���[��\������
                    Debug.LogError("Player�N���X�̃J�X�^���v���p�e�B��PlayerController���Z�b�g���Ă�������");
                }
            }

            //true��Ԃ�
            return true;
        }
    }
}
