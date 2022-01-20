using System;
using Raylib_cs;
using R = Raylib_cs.Raylib;

string gameName = "MugArm";

float playerSpeed = 5f;
int playerWidth = 50;
int playerHeight = 50;

Color playerColor = Color.WHITE;

R.InitWindow(800, 600, gameName);
R.SetTargetFPS(60);

Texture2D playerTexture = PlayerImage.LoadPlayerImage(playerWidth, playerHeight);

while (!R.WindowShouldClose())
{
    R.BeginDrawing();

    R.ClearBackground(Color.WHITE);

    Rectangle playerRect = new Rectangle(50, 50, playerWidth, playerHeight);

    PlayerMovemnt.HorizontalMovement(playerRect, playerSpeed);

    R.DrawTexture(playerTexture, (int)playerRect.x, (int)playerRect.y, playerColor);

    R.EndDrawing();
}

R.CloseWindow();