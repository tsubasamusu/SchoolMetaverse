using System.Drawing;
using UnityEngine;

namespace SchoolMetaverse
{
    /// <summary>
    /// �摜�Ɋւ��鏈�����s��
    /// </summary>
    public class PictureManager : MonoBehaviour
    {
        [SerializeField]
        private UIManagerMain uiManagerMain;//UIManagerMain

        [SerializeField]
        private MessageManager messageManager;//MessageManager

        [SerializeField]
        private Material blackBoardMaterial;//���̃}�e���A��

        /// <summary>
        /// �摜�𑗐M����
        /// </summary>
        public void SendPicture()
        {
            //���͂��ꂽ�摜�̃p�X���擾����
            string picturePath = uiManagerMain.IfPicturePath.text;

            //�C���[�W���擾����
            Image imgPicture = Image.FromFile(picturePath);

            //�C���[�W���擾�ł��Ȃ�������
            if (imgPicture == null)
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
            if(! texture.LoadImage(bytes))
            {
                //���b�Z�[�W�𑗐M����
                messageManager.PrepareSendMessage("Bot", "�摜�̃e�N�X�`�����쐬�ł��܂���ł����B");

                //�ȍ~�̏������s��Ȃ�
                return;
            }

            //���̃e�N�X�`����ݒ肷��
            blackBoardMaterial.mainTexture= texture;
        }
    }
}
