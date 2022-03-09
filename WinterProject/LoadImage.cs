using System;
using Raylib_cs;
using R = Raylib_cs.Raylib;


class PlayerImage
{
    public static (Texture2D, Texture2D, Texture2D) LoadPlayerImage(int playerWidth, int playerHeight)
    {
        Image playerImage = R.LoadImage("Images/playerImages/normal_stans.png");
        R.ImageResize(ref playerImage, playerWidth, playerHeight);
        Texture2D playerTextureNormal = R.LoadTextureFromImage(playerImage);

        Image playerImageLeft = R.LoadImage("Images/playerImages/playerImageLookLeft.png");
        R.ImageResize(ref playerImageLeft, playerWidth, playerHeight);
        Texture2D playerTextureLeft = R.LoadTextureFromImage(playerImageLeft);

        Image playerImageRight = R.LoadImage("Images/playerImages/playerImageLookRight.png");
        R.ImageResize(ref playerImageRight, playerWidth, playerHeight);
        Texture2D playerTextureRight = R.LoadTextureFromImage(playerImageRight);

        (Texture2D, Texture2D, Texture2D) a = new (playerTextureNormal, playerTextureLeft, playerTextureRight);

        return a;
    }
}