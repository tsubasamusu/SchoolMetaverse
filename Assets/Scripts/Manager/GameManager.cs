using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace SchoolMetaverse
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private List<SerializableInterface<ISetUp>> setUpList = new();//�����ݒ�p�C���^�[�t�F�C�X�̃��X�g

        /// <summary>
        /// �Q�[���J�n����ɌĂяo�����
        /// </summary>
        private void Start()
        {
            //�e�N���X�̏����ݒ���s��
            for (int i = 0; i < setUpList.Count; i++) { setUpList[i].Value.SetUp(); }
        }
    }
}
