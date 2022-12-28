using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject objPlayerPrefab;//�v���C���[�̃I�u�W�F�N�g�̃v���t�@�u

    private bool isConnecting;//�}�X�^�[�T�[�o�[�ɐڑ����Ă��邩�ǂ���

    /// <summary>
    /// Start���\�b�h���O�ɌĂяo�����
    /// </summary>
    private void Awake()
    {
        //�}�X�^�[�N���C�A���g���V�[�������[�h������A����ȊO�̃N���C�A���g�������V�[�������[�h����
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�}�X�^�[�T�[�o�[�ɐڑ������Ȃ�
        if (PhotonNetwork.IsConnected)
        {
            //�����_���ȃ��[���ɓ���
            PhotonNetwork.JoinRandomRoom();
        }
        //�}�X�^�[�T�[�o�[�ɐڑ��ł��Ȃ�������
        else
        {
            //�}�X�^�[�T�[�o�[�ւ̐ڑ����A���̌��ʂ��擾����
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// �}�X�^�[�T�[�o�[�ւ̐ڑ������������ۂɌĂяo�����
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //�}�X�^�[�T�[�o�[�ɐڑ����Ă���Ȃ�
        if (isConnecting)
        {
            //���[���̐ݒ���쐬����
            RoomOptions roomOptions = new();

            //���[���̍ő�l����ݒ肷��
            roomOptions.MaxPlayers= ConstData.MAX_PLAYERS;

            //�uroom�v�ɎQ�����邩�쐬����
            PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);

            //�}�X�^�[�T�[�o�[�ɐڑ����Ă��Ȃ���Ԃɐ؂�ւ���
            isConnecting = false;
        }
    }

    /// <summary>
    /// �Q�[���T�[�o�[�ւ̐ڑ������������ۂɌĂяo�����
    /// </summary>
    public override void OnJoinedRoom()
    {
        //TODO:�Q�[���T�[�o�[�ւ̐ڑ������������ۂ̏���
    }
}
