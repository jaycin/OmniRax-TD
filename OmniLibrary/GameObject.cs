using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;


namespace OmniLibrary
{
    public abstract class GameObject
    {
        #region Fields and Encaptulation
        string name;
        string type;
        string description;
        bool isTower;
        bool isProjectile;
        bool isMap;

        public bool IsMap
        {
            get { return isMap; }
            set { isMap = value; }
        }
        public bool IsTower
        {
            get { return isTower; }
            set { isTower = value; }
        }
        bool isEnemy;

        public bool IsEnemy
        {
            get { return isEnemy; }
            set { isEnemy = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        int enemyId;

        public int EnemyId
        {
            get { return enemyId; }
            set { enemyId = value; }
        }



        Vector2 size;
        Vector2 position;
        Vector2 velocity;
        Vector2 origin;
        Vector2 rotOrigin;

        public Vector2 RotOrigin
        {
            get { return rotOrigin; }
            set { rotOrigin = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Rectangle location;

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }


        bool hasFocus;


        bool isEnabled;
        bool visable;
        bool mouseOver;
        bool isSpawn;
        bool isIdle;
        bool isDead;
        bool isLocked;

        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }
        public bool IsIdle
        {
            get { return isIdle; }
            set { isIdle = value; }
        }
        public bool HasFocus
        {
            get { return hasFocus; }
            set { hasFocus = value; }
        }
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        public bool Visable
        {
            get { return visable; }
            set { visable = value; }
        }
        public bool MouseOver
        {
            get { return mouseOver; }
            set { mouseOver = value; }
        }
        public bool IsSpawn
        {
            get { return isSpawn; }
            set { isSpawn = value; }
        }

        float rotation;
        float speed;
        float scale;
        float time;
        float animationTime;

        public float AnimationTime
        {
            get { return animationTime; }
            set { animationTime = value; }
        }


        public float Time
        {
            get { return time; }
            set { time = value; }
        }
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        SpriteFont spriteFont;
        Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }


        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }

        #endregion

        #region Events
        //Ui
        public event EventHandler obj_Selected;
        public event EventHandler obj_MouseOver;
        public event EventHandler obj_Leave;
        public event EventHandler obj_RightClick;
        //Towers
        public event EventHandler obj_fire;
        public event EventHandler obj_Upgrade;
        public event EventHandler obj_teleport;
        //enemies
        public event EventHandler obj_Expire;
        public event EventHandler obj_Damaged;
        public event EventHandler obj_Hit;
        public event EventHandler obj_Die;
        public event EventHandler obj_Win;
        #endregion
        #region Constructors
        public GameObject()
        {
            IsTower = false;
            IsEnemy = false;
            color = Color.White;
            isEnabled = true;
            MouseOver = false;
            IsDead = false;
            Visable = true;
            spriteFont = ObjectManager.SpriteFont;
            
        }
        #endregion
        #region Abstract Methods
        public abstract void Update(GameTime gametime);
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
        #region Virtual Methods
        public virtual void OnUpgrade(EventArgs e)
        {
            if (obj_Upgrade != null)
            {
                obj_Upgrade(this, e);
            }
        }
        public virtual void OnHit(EventArgs e)
        {
            if (obj_Hit != null)
            {
                obj_Hit(this, e);
            }
        }
        public virtual void OnDamaged(EventArgs e)
        {
            if (obj_Damaged != null)
            {
                obj_Damaged(this, e);
            }
        }
        public virtual void OnExpire(EventArgs e)
        {
            if (obj_Expire != null)
            {
                obj_Expire(this, e);
            }
        }
        public virtual void OnSelected(EventArgs e)
        {
            
            if (obj_Selected != null)
            {
                obj_Selected(this, e);
            }
        }
        public virtual void OnMouseOver(EventArgs e)
        {
            if (obj_MouseOver != null)
            {
                obj_MouseOver(this, e);
                
            }
        }
        public virtual void OnMouseLeave(EventArgs e)
        {
            if (obj_Leave != null)
            {
                obj_Leave(this, e);
                
            }
        }
        public virtual void OnFire(EventArgs e)
        {
            if (obj_fire != null)
            {
                obj_fire(this, e);
            }
        }
        public virtual void OnWin(EventArgs e)
        {
            if (obj_Win != null)
            {
                obj_Win(this, e);
            }
            
        }
        public virtual void OnRightClick(EventArgs e)
        {
            if (obj_RightClick != null)
            {
                obj_RightClick(this, e);
            }
        }
        
        #endregion


    }
}
