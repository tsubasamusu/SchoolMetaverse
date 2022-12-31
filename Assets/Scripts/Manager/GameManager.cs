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
        private Transform playersTran;//�v���C���[�̐e�̈ʒu���

        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g

        /// <summary>
        /// �V�[���J�ڒ���ɌĂяo�����
        /// </summary>
        /// <returns>�҂�����</returns>
        private IEnumerator Start()
        {
            //�v���C���[�̃Q�[���I�u�W�F�N�g�𐶐�����
            GameObject objPlayer =
                PhotonNetwork.Instantiate("Player", spawnTranDetail.transform.position, Quaternion.identity);

            //���������I�u�W�F�N�g�̏����ݒ���s��
            objPlayer.GetComponent<PlayerController>().SetUp();

            //���������I�u�W�F�N�g�̐e��ݒ肷��
            objPlayer.transform.SetParent(playersTran);

            //���������I�u�W�F�N�g��񊈐�������
            objPlayer.SetActive(false);

            //�X�|�[���\�ɂȂ�܂ő҂�
            yield return new WaitUntil(() => spawnTranDetail.CheckCanSpawn());

            //���������I�u�W�F�N�g������������
            objPlayer.SetActive(true);

            //�J�����̐e��ݒ肷��
            Camera.main.transform.SetParent(objPlayer.transform);

            //�J�����̈ʒu��ݒ肷��
            Camera.main.transform.localPosition = new(0f, ConstData.CAMERA_HEIGHT, 0f);

            //�e�N���X�̏����ݒ���s��
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
