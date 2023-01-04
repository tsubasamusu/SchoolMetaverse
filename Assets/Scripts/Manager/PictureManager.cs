using Photon.Pun;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class PictureManager : MonoBehaviourPunCallbacks,ISetUp
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        /// <summary>
        /// PictureManager�̏����ݒ���s��
        /// </summary>
        public void SetUp()
        {
            //���ɑ��̃v���C���[���摜��\�����Ă���Ȃ�
            if (PhotonNetwork.CurrentRoom.CustomProperties["PictureBites"] is byte[] bytes)
            {
                //�o�C�i���f�[�^����摜�����ɐݒu����
                SetPictureFromBytes(bytes);
            }
        }

        /// <summary>
        /// �摜�𑗐M���鏀�����s��
        /// </summary>
        /// <param name="picturePath"></param>
        public void PrepareSendPicture(string picturePath) { photonView.RPC(nameof(SendPicture), RpcTarget.All,picturePath); }

        /// <summary>
        /// �摜�𑗐M����
        /// </summary>
        [PunRPC]
        private void SendPicture(string picturePath)
        {
            //�C���[�W�i�ێ��p�j
            Image imgPicture;

            //�C���[�W���擾����
            try { imgPicture = Image.FromFile(picturePath); }

            //�t�@�C����������Ȃ�������
            catch (FileNotFoundException)
            {
                //���b�Z�[�W�𑗐M����
                messageManager.PrepareSendMessage("Bot", "�������摜�̃p�X����͂��Ă��������B");

                //�ȍ~�̏������s��Ȃ�
                return;
            }

            //ImageConverter���쐬����
            ImageConverter imageConverter = new();

            //�C���[�W���o�C�i���f�[�^�ɕϊ�����
            byte[] bytes = (byte[])imageConverter.ConvertTo(imgPicture, typeof(byte[]));

            //�o�C�i���f�[�^����摜�����ɐݒu����
            SetPictureFromBytes(bytes);
        }

        /// <summary>
        /// �o�C�i���f�[�^����摜�����ɐݒu����
        /// </summary>
        /// <param name="bytes">�o�C�i���f�[�^</param>
        private void SetPictureFromBytes(byte[] bytes)
        {
            //Texture2D���쐬����
            Texture2D texture = new(1, 1);

            //�e�N�X�`�����쐬�ł��Ȃ�������
            if (!texture.LoadImage(bytes))
            {
                //���b�Z�[�W�𑗐M����
                messageManager.PrepareSendMessage("Bot", "�摜�̃e�N�X�`�����쐬�ł��܂���ł����B");

                //�ȍ~�̏������s��Ȃ�
                return;
            }

            //�X�v���C�g���쐬����
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

            //Hashtable���쐬����
            var hashtable = new ExitGames.Client.Photon.Hashtable
            {
                //�Q�[���T�[�o�[�ɉ摜�̃o�C�i���f�[�^����������
                ["PictureBites"] = bytes
            };

            //�쐬�����J�X�^���v���p�e�B����������
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

            //���̃X�v���C�g��ݒ肷��
            uiManagerMain.SetImgBlackBordSprite(sprite, texture.width, texture.height);
        }
    }
}

