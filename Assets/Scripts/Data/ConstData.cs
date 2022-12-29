using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolMetaverse
{
    public static class ConstData
    {
        public const int MAX_PLAYERS = 10;//最大参加人数

        public const float LOOK_SENSITIVITY = 5f;//視点感度

        public const float LOOK_SMOOTH = 0.1f;//視点の滑らかさ 

        public const float ANIM_SPEED = 1.5f;//アニメーションの再生速度

        public const float USE_CURVES_HEIGHT = 0.5f;//カーブ補正の有効高さ

        public const float MOVE_SPEED = 2f;//移動速度

        public const bool USE_CURVES = true;//Mecanimでカーブ調整を使うか
    }
}
