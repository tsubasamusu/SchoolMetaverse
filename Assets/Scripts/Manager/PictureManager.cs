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
    public class PictureManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        [SerializeField]
        private MessageManager messageManager;//MessageManager

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

            //���̃X�v���C�g��ݒ肷��
            uiManagerMain.SetImgBlackBordSprite(sprite, texture.width, texture.height);
        }
    }
}

