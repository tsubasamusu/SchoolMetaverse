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
        private List<SerializableInterface<ISetUp>> setUpList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�v���C���[�̃Q�[���I�u�W�F�N�g�𐶐�����
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTranDetail.transform.position, Quaternion.identity);

            //PlayerController���擾����
            PlayerController playerController=objPlayer.GetComponent<PlayerController>();

            //���L����v������
            GameData.instance.RequestOwnership();

            //���������I�u�W�F�N�g�̏����ݒ���s��
            playerController.SetUp();

            //�J�����̐e��ݒ肷��
            Camera.main.transform.SetParent(objPlayer.transform);

            //�J�����̈ʒu��ݒ肷��
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //�e�N���X�̏����ݒ���s���i���[���Q����j
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
