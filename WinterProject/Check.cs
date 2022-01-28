using System;
using Raylib_cs;
using R = Raylib_cs.Raylib;

class Check
{
    public static (Rectangle, bool) IfOnFloor(Rectangle playerRect, int playerHeight, bool onFloor, bool onPlatform)
    {
        if (playerRect.y > 600 - playerHeight)
        {
            playerRect.y = 600 - playerHeight;
            onFloor = true;
        }
        else if (onPlatform)
        {
            onFloor = true;
        }
        else
        {
            onFloor = false;
        }

        (Rectangle, bool) a = (playerRect, onFloor);
        return a;
    }

}