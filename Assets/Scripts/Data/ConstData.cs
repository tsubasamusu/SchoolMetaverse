using UnityEngine;

namespace SchoolMetaverse
{
    public static class ConstData
    {
        public const int MAX_PLAYERS = 10;//最大参加人数

        public const float LOOK_SENSITIVITY = 5f;//視点感度

        public const float LOOK_SMOOTH = 0.1f;//視点の滑らかさ 

        public const float MOVE_SPEED = 0.5f;//移動速度

        public const float MAX_CAMERA_ANGLE_X = 40f;//カメラの最大角度x

        public const float CAMERA_HEIGHT = 1.4f;//カメラの高さ

        public const KeyCode WALK_F_KEY = KeyCode.UpArrow;//前へ進むキー

        public const KeyCode WALK_R_KEY = KeyCode.RightArrow;//右へ進むキー

        public const KeyCode WALK_B_KEY = KeyCode.DownArrow;//後ろへ進むキー

        public const KeyCode WALK_L_KEY = KeyCode.LeftArrow;//左へ進むキー
    }
}
