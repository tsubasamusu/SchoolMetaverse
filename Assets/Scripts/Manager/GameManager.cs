using Photon.Pun;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Transform spawnTran;//�X�|�[���n�_

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g

        /// <summary>
        /// �V�[���J�ڒ���ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�v���C���[�̃Q�[���I�u�W�F�N�g�𐶐�����
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTran.position, Quaternion.identity);

            //���������I�u�W�F�N�g�̏����ݒ���s��
            objPlayer.GetComponent<PlayerController>().SetUp();

            //�J�����̐e��ݒ肷��
            Camera.main.transform.SetParent(objPlayer.transform);

            //�J�����̈ʒu��ݒ肷��
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //BGM���Đ�����
            SoundManager.instance.PlaySound(SoundDataSO.SoundName.BGM,true, GameData.instance.bgmVolume);

            //�e�N���X�̏����ݒ���s��
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
