using System;
using Raylib_cs;
using R = Raylib_cs.Raylib;


class PlayerImage
{
    public static Texture2D LoadPlayerImage(int playerWidth, int playerHeight)
    {
        Image playerImage = R.LoadImage("playerImage.png");
        R.ImageResize(ref playerImage, playerWidth, playerHeight);
        Texture2D playerTexture = R.LoadTextureFromImage(playerImage);

        return playerTexture;
    }
}