# Overview
[Unity3D Roll-a-Ball Game](https://learn.unity.com/project/roll-a-ball?uv=6)
---
I closely followed the Roll-a-Ball tutorial except for a few changes I felt compelled to make:

1. The AI enemy moves to the last valid location the player was at if the player moves to a position it can't get to.
2. The camera rotates to follow the movement of the player, but this is not the best implementation.
3. The player movement directions are calculated relative to the camera's view.
4. The enemy is made of 4 boxes, and its collision object is separate.
