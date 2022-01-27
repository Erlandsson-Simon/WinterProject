using System;
using System.Numerics;
using Raylib_cs;
using R = Raylib_cs.Raylib;

class PlayerMovemnt
{

    public static Vector2 HorizontalMovement(float playerSpeed)
    {
        Vector2 movement = new Vector2();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = playerSpeed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -playerSpeed;
        }

        return movement;

    }

    public static (Vector2, int) UpwardMovement(bool onFloor, int jumpInt, float playerSpeed)
    {
        Vector2 movement = new Vector2();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            jumpInt = 0;
        }

        if (jumpInt < 100)
        {
            jumpInt += 1;
            movement.Y = -playerSpeed;
        }

        return (movement, jumpInt);
    }


    public static (Vector2, float) Gravity(float accel)
    {
        Vector2 movement = new Vector2();

        movement.Y = accel;
        accel += 0.15f;

        return (movement, accel);
    }
}
