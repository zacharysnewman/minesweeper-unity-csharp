using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODO : MonoBehaviour
{
    /* 
     * Bomb reveals
     * √ Revealing a bomb shouldn't reveal nearby
     * 
     * Restarting
     * √ Win or lose has a smiley face to restart
     * 
     * Controls
     * √ Left click should equate to flagMode=false
     * √ Right click should equate to flagMode=true
     * 
     * Better Camera
     * √ Don't lock movement on both axes when one is blocked by a boundary (this should make movement smoother)
     * 
     * Map Generation
     * √ Should occur on first click (excluding activated tile from bomb generation)
     * √ Should not be able to hit a bomb on first click ^
     * 
     * Map Struct
     * √ Should not allow activating nearby (by tapping revealed tile) unless the number of nearby flags is >= nearby bombs
     * 
     * Camera
     * √ Move with the arrow keys
     * 
     * Renderer -> UI
     * √ On lose, show specific bomb that was detonated (using primaryActivation flag for this might cause problems)
     * √ Should also reveal all bombs (renderer trick only?)
     * √ Show current bomb count (- flagged tiles) with digital font
     * √ Should show some display of winning or losing
     * √ Disable clicking after game is over
     * √ Clicking should be reenabled on starting a new game
     * √ Center camera origin on board center on state change
     * √ Detect both winning and losing states
     * √ Include font and smiley faces in Tileset
     * X Show integer timer with digital font
     * X Show game win/lose/not-started state in smiley face icon
     * 
     * State
     * X Include game time in state and update
     * 
     * UI Settings
     * √ Update slider title text to show actual value
     * √ Update bomb slider min/max and slider value based on updates from width and height
     * 
     * UI Input
     * X Move start button to a smiley face on top of the screen
     * X Settings in menu for width, height, bombcount should only update map when smiley is pressed
     * 
     * Fix issues
     * (√) Destroy previous tiles after each state change
     * (√) Revealing empty tiles should reveal nearby empty tiles on activation (out parameter?)
     * (√) Activating tiles with nearby bombs should activate all surrounding tiles that aren't flagged
     * (√) Bombs generate in a straight line and do not shuffle at all, should be spread throughout the level
     * 
     * Build Rendering to display grid of sprite tiles based on State
     * (√) Use tileset for graphics
     * (√) Create prefab for tile
     * (√) Perhaps have prefab use a tileset assigned by the RendererBehaviour?
     * 
     * Finish Map struct
     * (√) Setup Equality checks for Map
     * (√) Only fire StateChangedEvent when Map actually changes in State
     * (√) Replace Width, Height, and Bombs with MapInfo
     * (√) Setup UI flagmode
     * (√) Pass flagmode through to ActivateTileEvent
     * 
     * Finish State struct
     * (√) Add equality check to WithMap
     * (√) Connect ActivateTile to Map ActivateTile method
     * 
     * Setup UI for hooking into EntryBehaviour
     * (√) Button area for clicking on tiles
     * (√) Map Width Slider
     * (√) Map Height Slider
     * (√) Bomb Count Slider
     * (√) Play Button to setup empty grid size
     * (√) Attach all buttons to associated methods in EntryBehaviour
     * 
     * Compare MapState and Map for redundancies 
     * (√) collapse into one struct
     * 
     */
}