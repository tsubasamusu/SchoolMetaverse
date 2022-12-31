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
        private List<SerializableInterface<ISetUp>> setUpList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g

        private bool joinedRoom;//���[���ɎQ���������ǂ���

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        /// <returns>�҂�����</returns>
        private IEnumerator Start()
        {
            //�v���C���[�̃X�|�[������������܂ő҂�
            yield return StartCoroutine(SpawnPlayer());

            //�e�N���X�̏����ݒ���s���i���[���Q����j
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }

        /// <summary>
        /// �v���C���[���X�|�[��������
        /// </summary>
        /// <returns>�҂�����</r
        public IEnumerator SpawnPlayer()
        {
            //���[���ɎQ������܂ő҂�
            yield return new WaitUntil(() => joinedRoom);

            //�v���C���[�̃Q�[���I�u�W�F�N�g�𐶐�����
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTranDetail.transform.position, Quaternion.identity);

            //���������I�u�W�F�N�g�̏����ݒ���s��
            objPlayer.GetComponent<PlayerController>().SetUp();

            //�X�|�[���\�ɂȂ�܂ő҂�
            yield return new WaitUntil(() => spawnTranDetail.CheckCanSpawn());

            //�J�����̐e��ݒ肷��
            Camera.main.transform.SetParent(objPlayer.transform);

            //�J�����̈ʒu��ݒ肷��
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);
        }
    }
}
