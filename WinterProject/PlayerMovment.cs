using System;
using System.Numerics;
using Raylib_cs;
using R = Raylib_cs.Raylib;

class PlayerMovemnt
{

    public static (Vector2, Texture2D) HorizontalMovement(float playerSpeed, Texture2D playerTextureRight, Texture2D playerTextureLeft, Texture2D playerTexture)
    {
        Vector2 movement = new Vector2();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = playerSpeed;
            playerTexture = playerTextureRight;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -playerSpeed;
            playerTexture = playerTextureLeft;
        }

        (Vector2, Texture2D) a = new(movement, playerTexture);

        return a;

    }

    public static (Vector2, bool) UpwardMovement(bool onFloor, bool jumpBool, float playerSpeed, bool onPlatform)
    {
        Vector2 movement = new Vector2();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && onPlatform == true)
        {
            jumpBool = true;
        }

        if (jumpBool)
        {
            movement.Y = -playerSpeed;
        }

        (Vector2, bool) a = (movement, jumpBool);

        return a;
    }


    public static (Vector2, float) Gravity(float accel)
    {
        Vector2 movement = new Vector2();

        movement.Y = accel;
        accel += 0.35f;

        (Vector2, float) a = (movement, accel);

        return a;
    }
}
