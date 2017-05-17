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
    public class GameStateManager : Microsoft.Xna.Framework.GameComponent
    {
        #region Event Region
        public event EventHandler OnStateChange;
        #endregion

        #region Fields and Properties Region
        Stack<GameState> gameStates = new Stack<GameState>();
        const int startDrawOrder = 2000;
        const int drawOrderInc = 100;
        int drawOrder;
        
        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }
        
        #endregion

        #region Constructor Region
        public GameStateManager(Game game)
            : base(game)
        {
            drawOrder = startDrawOrder;
        }
        #endregion

        #region XNA Method Region
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            foreach(GameState g in gameStates)
            {
                if(Enabled)
                base.Update(gameTime);
            }
            
        }
        #endregion
        
        public void loadState(GameState newState)
        {
            newState.isPop = false;
            newState.Visible = true;
            newState.Enabled = true;
            if(Game.Components.Contains(newState))
            {
                //Game.Components.Remove(newState);
               // Game.Components.Add(newState);
            }
            else
            Game.Components.Add(newState);
            drawOrder += drawOrderInc;
            if (OnStateChange != null)
                OnStateChange(this, null);
        }
        #region Methods Region
        
        /// <summary>
        /// remove top of stack and go back to previous
        /// </summary>
        public void PopState()
        {
            if (gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= drawOrderInc;
                if (OnStateChange != null)
                    OnStateChange(this, null);
            }
            
        }

        private void RemoveState()
        {
            GameState State = gameStates.Peek();            
            OnStateChange -= State.StateChange;
            Game.Components.Remove(State);
            gameStates.Pop();
            gameStates.Peek().isPop = true;
            if (!gameStates.Peek().Visible)
            {
                gameStates.Peek().Visible = true;
                
            }
            if (!gameStates.Peek().Enabled)
            {
                gameStates.Peek().Enabled = true;

            }
        }

        /// <summary>
        /// Move to another state
        /// </summary>
        /// <param name="newState"></param>

        public void PushState(GameState newState)
        {
            drawOrder += drawOrderInc;
            newState.DrawOrder = drawOrder;
            newState.isPop = false;
            gameStates.Push(newState);
            if (Game.Components.Contains(newState))
            {
                if (!newState.Visible)
                {
                    newState.Visible = true;
                    if (!newState.Enabled)
                    {
                        newState.Enabled = true;
                    }
                }
                return;
            }
            else
                Game.Components.Add(newState);
            OnStateChange += newState.StateChange;
            if (OnStateChange != null)
                OnStateChange(this, null);
        }
        public void AddLoaded(GameState loadedState)
        {
            for (int i = 0; i < Game.Components.Count; i++)
            {
                try
                {
                    if (Game.Components[i] == loadedState)
                    {
                        
                        loadedState = (GameState)Game.Components[i];
                        drawOrder += drawOrderInc;
                        loadedState.DrawOrder = drawOrder;
                        gameStates.Push(loadedState);
                        OnStateChange += loadedState.StateChange;
                        
                        if (OnStateChange != null)
                        {
                            OnStateChange(this, null);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.ToString());
                }
            }
        }
          
        /// <summary>
        /// Remove all states from stacks
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(GameState newState)
        {
            while (gameStates.Count > 0)
            {
                RemoveState();
            }
            
            newState.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;
            gameStates.Push(newState);
            Game.Components.Add(newState);
            if (newState.Visible == false)
            {
                newState.Visible = true;
            }
            if (OnStateChange != null)
            {
                OnStateChange(this, null);
            }
        }
        #endregion
    }
    }
  
        
    