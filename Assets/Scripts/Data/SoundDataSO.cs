using System.Collections.Generic; 
using System; 
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "Create SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
    /// <summary>   
    /// ���̖��O   
    /// </summary>   
    public enum SoundName
    {
        �{�^�������������̉�,
        �����ȃ{�^�������������̉�,
        �������̑���,
        BGM
    }

    /// <summary>   
    /// ���̃f�[�^���Ǘ����� 
    /// </summary>   
    [Serializable]
    public class SoundData
    {
        public SoundName name;//���O  
        public AudioClip clip;//�N���b�v 
    }

    public List<SoundData> soundDataList = new();//���̃f�[�^�̃��X�g  
}