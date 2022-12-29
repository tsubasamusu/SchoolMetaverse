using Photon.Pun;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpBeforeJoinRoomList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g�i���[���Q���O�j

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpAfterJoinRoomList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g�i���[���Q����j

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�e�N���X�̏����ݒ���s���i���[���Q���O�j
            for (int i = 0; i < setUpBeforeJoinRoomList.Count; i++) { setUpBeforeJoinRoomList[i].Value.SetUp(); }
        }

        /// <summary>
        /// �Q�[���T�[�o�[�ւ̐ڑ������������ۂɌĂяo�����
        /// </summary>
        public override void OnJoinedRoom()
        {
            //�v���C���[�̃Q�[���I�u�W�F�N�g�𐶐�����
            GameObject objPlayer =
                PhotonNetwork.Instantiate(GameData.instance.ObjPlayerPrefab.name, GameData.instance.SpawnTran.position, Quaternion.identity);

            //���������I�u�W�F�N�g�̏����ݒ���s��
            objPlayer.GetComponent<PlayerController>().SetUp();

            //�J�����̐e��ݒ肷��
            Camera.main.transform.SetParent(objPlayer.transform);

            //�J�����̈ʒu��ݒ肷��
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //�e�N���X�̏����ݒ���s���i���[���Q����j
            for (int i = 0; i < setUpAfterJoinRoomList.Count; i++) { setUpAfterJoinRoomList[i].Value.SetUp(); }
        }
    }
}
