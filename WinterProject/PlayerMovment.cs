using System;
using Raylib_cs;
using R = Raylib_cs.Raylib;

class PlayerMovemnt {

    public static void HorizontalMovement(Rectangle playerRect, float playerSpeed) {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        playerRect.x += playerSpeed;
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        playerRect.x -= playerSpeed;
    }

    }
}
