using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using R = Raylib_cs.Raylib;

string gameName = "MugArm";

float playerSpeed = 10f;
int playerWidth = 50;
int playerHeight = 50;
int[] playerStartingPos = new int[] { 960, 540 };

float accel = 0;
bool jumpBool = false;

bool onFloor = false;
bool onPlatform = false;

Color playerColor = Color.WHITE;

R.InitWindow(1920, 1080, gameName);
R.SetTargetFPS(60);

float screenWidth = R.GetScreenWidth();
float screenHeight = R.GetScreenHeight();

(Texture2D, Texture2D, Texture2D) playerTextureTuple = PlayerImage.LoadPlayerImage(playerWidth, playerHeight);

Texture2D playerTextureNormal = playerTextureTuple.Item1;
Texture2D playerTextureLeft = playerTextureTuple.Item2;
Texture2D playerTextureRight = playerTextureTuple.Item3;

Texture2D playerTexture = playerTextureNormal;

List<Rectangle> platforms = new List<Rectangle>();

Rectangle playerRect = new Rectangle(playerStartingPos[0], playerStartingPos[1], playerWidth, playerHeight);

Rectangle earthRect = new Rectangle(0, 880, screenWidth, 300);
platforms.Add(earthRect);
Rectangle skyRect = new Rectangle(0, 0, screenWidth, 880);

Camera2D camera = new Camera2D();
camera.target = new Vector2(screenWidth / 2, screenHeight / 2);
camera.zoom = 1f;
camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);

while (!R.WindowShouldClose())
{
    R.BeginDrawing();
    // hälsobar etc

    R.BeginMode2D(camera);

    R.ClearBackground(Color.WHITE);

    R.DrawRectangleRec(earthRect, Color.GREEN);
    R.DrawRectangleRec(skyRect, Color.SKYBLUE);

    R.DrawTexture(playerTexture, (int)playerRect.x, (int)playerRect.y, playerColor);

    R.EndMode2D();
    R.EndDrawing();

    playerTexture = playerTextureNormal;

    (Vector2, Texture2D) playerMovementHorizontalTuple = PlayerMovemnt.HorizontalMovement(playerSpeed, playerTextureRight, playerTextureLeft, playerTexture);

    Vector2 xMovement = playerMovementHorizontalTuple.Item1;
    playerTexture = playerMovementHorizontalTuple.Item2;

    playerRect.x += xMovement.X;
    camera.target.X += xMovement.X;

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.x -= xMovement.X;
            camera.target.X -= xMovement.X;
        }
    }

    (Vector2, bool) upWardMovementTuple = PlayerMovemnt.UpwardMovement(onFloor, jumpBool, playerSpeed);

    Vector2 yMovement = upWardMovementTuple.Item1;
    jumpBool = upWardMovementTuple.Item2;

    playerRect.y += yMovement.Y;

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.y -= yMovement.Y;
        }
    }

    (Rectangle, bool) ifOnFloorTuple = Check.IfOnFloor(playerRect, playerHeight, onFloor, onPlatform);

    playerRect = ifOnFloorTuple.Item1;
    onFloor = ifOnFloorTuple.Item2;

    if (onFloor)
    {
        accel = 0;
        jumpBool = false;
    }
    else
    {
        (Vector2, float) gravityTuple = PlayerMovemnt.Gravity(accel);
        yMovement = gravityTuple.Item1;
        accel = gravityTuple.Item2;

        playerRect.y += yMovement.Y;
    }

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.y = item.y - playerHeight;

            onPlatform = true;
            jumpBool = false;
        }
        else
        {
            onPlatform = false;
        }
    }
}

R.CloseWindow();