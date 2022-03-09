using System;
using Raylib_cs;
using R = Raylib_cs.Raylib;

class Check
{
    public static (Rectangle, bool) IfOnFloor(Rectangle playerRect, int playerHeight, bool onFloor, bool onPlatform)
    {
        if (playerRect.y > R.GetScreenHeight() - playerHeight)
        {
            playerRect.y = R.GetScreenHeight() - playerHeight;
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

    // public static void platformCollition()
    // {
    //     (Vector2, bool) upWardMovementTuple = PlayerMovemnt.UpwardMovement(onFloor, jumpBool, playerSpeed, onPlatform);

    //     Vector2 yMovement = upWardMovementTuple.Item1;
    //     jumpBool = upWardMovementTuple.Item2;

    //     playerRect.y += yMovement.Y;

    //     foreach (var item in platforms)
    //     {
    //         if (R.CheckCollisionRecs(playerRect, item))
    //         {
    //             playerRect.y -= yMovement.Y;
    //             jumpBool = false;
    //         }
    //     }
    // }

}