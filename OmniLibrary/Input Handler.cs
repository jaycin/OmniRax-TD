using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OmniLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Input_Handler : Microsoft.Xna.Framework.GameComponent
    {
        #region Field Region
        //this is the states for keyboard and gamepad
        public static KeyboardState keyboardState { get; set; }
        public static KeyboardState lastKeyboardState { get; set; }
        public static GamePadState[] GamePadStates { get; set; }
        public static GamePadState[] LastGamePadState { get; set; }
        public static Point MousePosition;

        #endregion

        #region Constructor Region
        public Input_Handler(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();

            GamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                GamePadStates[(int)index] = GamePad.GetState(index);
            }
            
        }
        
        #endregion

        #region XNA methods
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            GamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                GamePadStates[(int)index] = GamePad.GetState(index);
            }

            base.Update(gameTime);
            
        }
        #endregion

        #region General Method Region
        public static void Flush()
        {
            lastKeyboardState = keyboardState;
        }
        #endregion

        #region Keyboard Region
        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
                lastKeyboardState.IsKeyDown(key);
        }
        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
                lastKeyboardState.IsKeyUp(key);
        }
        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }
        #endregion

        #region Game Pad Region
        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonUp(button) &&
            LastGamePadState[(int)index].IsButtonDown(button);
        }
        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonDown(button) &&
            LastGamePadState[(int)index].IsButtonUp(button);
        }
        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonDown(button);
        }
        #endregion
        #region Mouse Region

        #endregion
    }
}
