using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace OmniLibrary.EnemyEngine
{

    public class Skeleton : GameObject
    {
        #region fields and properties
        int counter;
        int frameState;
        int frame;
        int health;
        int currentHealth;
        int rows;
        int selection;
        int selfacing;
        int squareId;
        public int HitDamage;
        public int PassDamage;
        public double ResFire;
        public double ResIce;
        public double ResLighning;
        public int Gold;
        int[,] Stance = new int[1, 2];




        int[,] walk = new int[1, 2];
        int[,] attack = new int[1, 2];
        int[,] cast = new int[1, 2];
        int[,] block = new int[1, 2];
        int[,] die = new int[1, 2];
        int[,] aim = new int[1, 2];
        public enum ActionState
        {
            stance, walk, attack, cast, block, hit, die, aim,
        }





        aStar aSt;
        MovementGrid Grid;

        public int SquareId
        {
            get { return squareId; }
            set { squareId = value; }
        }

        public int Selfacing
        {
            get { return selfacing; }
            set { selfacing = value; }
        }

        double time;

        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }
        Texture2D image;
        Rectangle destRect;
        List<Rectangle> SourceList;

        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        int collums;

        public int Columns
        {
            get { return collums; }
            set { collums = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        bool isTarget;

        public bool IsTarget
        {
            get { return isTarget; }
            set { isTarget = value; }
        }
        bool moving;
        bool expire;

        public bool Moving
        {
            get { return moving; }
            set { moving = value; }
        }
        HeathBar HpBar;
        double animationTime;
        public int FrameState
        {
            get { return frameState; }
            set { frameState = value; }
        }

        Vector2 nextPoint;
        public Queue<int> walkPath;
        public Vector2 NextPoint
        {
            get { return nextPoint; }
            set { nextPoint = value; }
        }

        #endregion

        public Skeleton(Texture2D SkellySprite, int row, int columns, Vector2 SpawnPoint, int endPoint, int id, HeathBar hpBar, MovementGrid grid, string type)
        {
            
            Type = "Skelly";
            IsEnemy = true;
            EnemyId = id;
            Moving = true;
            expire = false;
            obj_Expire += new EventHandler(Skeleton_obj_Expire);
            obj_Hit += new EventHandler(Skeleton_obj_Hit);
            frame = 0;
            Speed = .5f;
            IsTarget = false;
            HpBar = hpBar;
            walkPath = new Queue<int>();
           
            Position = SpawnPoint;
            Scale = .6f;
            Rows = row;
            Columns = columns;
            setframes();
            Image = SkellySprite;
            Velocity = Vector2.Zero;
            Name = type;
            SetHp();
            SetDamage();
            CurrentHealth = Health;
            animationTime = 0;
            IsSpawn = true;
            destRect = new Rectangle((int)Position.X, (int)Position.Y, (int)((Image.Width / Columns) * Scale), (int)((Image.Height / Rows) * Scale));
            HpBar.BaseRect = new Rectangle(destRect.X + destRect.Width / 2, destRect.Y + destRect.Height / 10, destRect.Width / 3, destRect.Height / 10);
            HpBar.MaxHealth = (int)Health;
            HpBar.Health = (int)CurrentHealth;
            Origin = new Vector2(destRect.X + destRect.Width / 2, destRect.Y + destRect.Height / 2);
            Grid = grid;
            aSt = new aStar(Grid);
            squareId = getId();
            SourceList = new List<Rectangle>();
            for (int i = 0; i < Rows; i++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    SourceList.Add(new Rectangle(Image.Width / Columns * x, Image.Height / Rows * i, Image.Width / Columns, Image.Height / Rows));
                }
            }
            
            counter = 0;
            Visable = true;

            walkPath = aSt.FindPath(squareId, Grid.lstEnd[0], false);
            walkPath.Dequeue();
            NextPoint = Grid.SquareList[walkPath.Peek()].Origin;
            if (Grid.SourceList[squareId].Contains(new Point((int)NextPoint.X, (int)nextPoint.Y)))
            {
                walkPath.Dequeue();
                NextPoint = Grid.SquareList[walkPath.Peek()].Origin;
                checkDirection();

            }
            checkDirection();
            frameState = (int)ActionState.walk;
            selection = walk[0, 0];
            selfacing = facing();
            frame = selection + selfacing * collums;

        }

        void Skeleton_obj_Hit(object sender, EventArgs e)
        {
            frameState = (int)ActionState.block;
            selection = block[0, 0];
        }

        void Skeleton_obj_Expire(object sender, EventArgs e)
        {
            Moving = false;
            frameState = (int)ActionState.die;
            Selection = die[0, 0];
            expire = true;
        }
        public override void Update(GameTime gameTime)
        {

            Origin = new Vector2(destRect.X + destRect.Height / 2, destRect.Y + destRect.Width / 2);
            Location = destRect;
            Visable = true;
            if (Visable)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                animationTime += gameTime.ElapsedGameTime.Milliseconds;


                if (Moving)
                {
                    move();

                    HpBar.DestRect = new Rectangle(destRect.X + destRect.Width / 3, destRect.Y + destRect.Height / 5, HpBar.DestRect.Width, HpBar.DestRect.Height);
                    HpBar.BaseRect = new Rectangle(destRect.X + destRect.Width / 3, destRect.Y + destRect.Height / 5, HpBar.BaseRect.Width, HpBar.BaseRect.Height);
                }
                Time = 0;

                if (animationTime > 250)
                {
                    HpBar.Health = CurrentHealth;
                    Animate(frameState);
                    animationTime = 0;
                    squareId = getId();
                    
                }
                if (Grid.SourceList[squareId].Contains(new Point((int)NextPoint.X, (int)nextPoint.Y)))
                {
                    if (!expire)
                    {
                        if (walkPath.Count > 1)
                        {
                            walkPath.Dequeue();
                            NextPoint = Grid.SquareList[walkPath.Peek()].Origin;
                            checkDirection();
                        }
                        else Skeleton_obj_Expire(this, null);
                    }


                }



            }


        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (Visable)
            {
                spritebatch.Draw(Image, destRect, SourceList[frame], Color.White);
                HpBar.Draw(spritebatch);
            }
        }
        /// <summary>
        ///0 = Stance
        ///1 = walk
        ///2 = attack
        ///3 = cast
        ///4 = block
        ///5 = hit
        ///6 = die
        ///7 = aim 
        /// </summary>
        /// <param name="frameState"></param>
        public void Animate(int frameState)
        {
            if (expire)
            {
                selection++;
                if (selection >= die[0, 1])
                {
                    this.IsDead = true;
                }





            }

            switch (frameState)
            {
                case 0: if (frameState == (int)ActionState.stance && Selection < Stance[0, 1])
                    {
                        Selection++;
                    }
                    else
                        Selection = (int)Stance[0, 0];
                    break;
                case 1: if (frameState == (int)ActionState.walk && Selection < walk[0, 1])
                    {
                        Selection++;
                    }
                    else
                        Selection = (int)walk[0, 0];
                    break;
                case 2: if (frameState == (int)ActionState.attack && Selection < attack[0, 1])
                    {
                        Selection++;
                    }
                    else
                        Selection = (int)attack[0, 0];
                    break;
                case 3: if (frameState == (int)ActionState.cast && Selection < cast[0, 1])
                    {
                        Selection++;
                    }
                    else
                        Selection = (int)cast[0, 0];
                    break;
                case 4: if (frameState == (int)ActionState.block && Selection < block[0, 1])
                    {
                        Selection++;
                    }
                    else
                    {
                        frameState = (int)ActionState.walk;
                        Selection = (int)walk[0, 0];
                    }
                    break;
                case 6: if (frameState == (int)ActionState.die && Selection < die[0, 1])
                    {
                        Selection++;
                    }
                    else
                        Selection = (int)die[0, 0];
                    break;
                case 7: if (frameState == (int)ActionState.aim && Selection < aim[0, 1])
                    {
                        Selection++;
                    }
                    else
                        Selection = (int)aim[0, 0];
                    break;
            }
            //if (frameState == (int)ActionState.hit)
            //{
            //    Moving = false;
            //}
            //else
            //{
            //    Moving = true;
            //}
            frame = Selection + collums * facing();
            //if (Position == WayPoints.Peek()||Position.X>WayPoints.Peek().X)
            //{

            //}

            destRect = new Rectangle((int)Position.X, (int)Position.Y, destRect.Width, destRect.Height);

        }
        public void checkDirection()
        {
            Rotation = (float)Math.Atan2(NextPoint.Y - Origin.Y, NextPoint.X - Origin.X);
        }
        public void move()
        {
            Velocity = new Vector2((float)Math.Cos(Rotation) * Speed, (float)Math.Sin(Rotation) * Speed);
            Position += Velocity;
            destRect = new Rectangle((int)Position.X, (int)Position.Y, destRect.Width, destRect.Height);

        }
        public int facing()
        {
            //Radians go from 0 to 2
            //1 radion = 180/pie degrees
            //thus 0= right 
            //1 = left
            //0.5 = up
            //1.5 = down
            //2 = right
            if (frame < SourceList.Count)
            {
                if (1.4 < Rotation && Rotation < 1.6) // 180 down threshold
                {
                    return 6;
                }
                else if (0.1 > Rotation && Rotation < 0 || Rotation > 1.9 && Rotation < 2)//270 right threshold
                {
                    return 4;
                }
                else if (0.4 < Rotation && Rotation < 0.6)//360 up threshold
                {
                    return 2;
                }
                else if (0.9 < Rotation && Rotation < 1.1)//90 left threshold
                {

                    return 0;
                }
                else
                {
                    //upRight
                    if (0.1 > Rotation && Rotation < 0.4)
                    {
                        return 3;
                    }
                    //downRight
                    else if (1.6 > Rotation && Rotation < 1.8)
                    {
                        return 5;
                    }
                    //downLeft
                    else if (1.1 > Rotation && Rotation < 1.4)
                    {
                        return 7;
                    }

                    //UpLeft
                    else if (0.6 > Rotation && Rotation < 0.8)
                    {
                        return 1;

                    }


                }
            }
            if (Rotation == 0)
            {
                return 4;
            }
            return 0;


        }
        public int getId()
        {
            int Id = 0;
            foreach (Square s in Grid.SquareList)
            {
                if (s.Location.Contains(new Point((int)Origin.X, (int)Origin.Y)))
                {
                    if (s.Id < 1024)
                        Id = s.Id;
                    else
                    {
                    }
                }
            }
            return Id;
        }
        public void setframes()
        {
            if (collums == 32)
            {
                Stance[0, 0] = 0;
                Stance[0, 1] = 3;

                walk[0, 0] = 4;
                walk[0, 1] = 11;
                attack[0, 0] = 12;
                attack[0, 1] = 15;
                cast[0, 0] = 16;
                cast[0, 1] = 19;
                block[0, 0] = 29;
                block[0, 1] = 21;

                die[0, 0] = 22;
                die[0, 1] = 26;
                aim[0, 0] = 27;
                aim[0, 1] = 31;
            }
            else if (collums == 28)
            {
                Stance[0, 0] = 0;
                Stance[0, 1] = 3;

                walk[0, 0] = 4;
                walk[0, 1] = 11;
                attack[0, 0] = 12;
                attack[0, 1] = 15;
                block[0, 0] = 16;
                block[0, 1] = 21;
               
                die[0, 0] = 22;
                die[0, 1] = 27;
            }
            else if (collums == 24)
            {
                Stance[0, 0] = 0;
                Stance[0, 1] = 3;

                walk[0, 0] = 4;
                walk[0, 1] = 11;
                attack[0, 0] = 12;
                attack[0, 1] = 17;
                block[0, 0] = 17;
                block[0, 1] = 19;

                die[0, 0] = 20;
                die[0, 1] = 23;
            }
        }
        public void TakeDamage(int Damage)
        {
            CurrentHealth -= Damage;
            if (CurrentHealth <= 0)
            {
                Skeleton_obj_Expire(this, null);
            }
            else
            {
                Skeleton_obj_Hit(this, null);
            }
        }
        public void SetHp()
        {
            switch (Name)
            {
                //set hp here and res here

                case "skeleton":
                    Gold = 50;
                    Health = 275;
                    ResFire = 10;
                    ResIce = 10;
                    ResLighning = 10;
                    Speed = 0.4f;
                    break;
                case "ant":
                    Gold = 20;
                    Health = 225;
                    ResFire = 25;
                    ResIce = 5;
                    ResLighning = 15;
                    Speed = 0.6f;
                    break;
                case "fireAnt":
                    Gold = 30;
                    Health = 275;
                    ResFire = 50;
                    ResIce = 0;
                    ResLighning = 25;
                    Speed = 0.55f;
                    break;
                case "iceAnt":
                    Gold = 30;
                    Health = 350;
                    ResFire = 0;
                    ResIce = 50;
                    ResLighning = 35;
                    Speed = 0.4f;
                    break;
                case "spider":
                    Gold = 20;
                    Health = 200;
                    ResFire = 0;
                    ResIce = 50;
                    ResLighning = 40;
                    Speed = 0.625f;
                    break;
                case "spiderRegular":
                    Gold = 40;
                    Health = 250;
                    ResFire = 0;
                    ResIce = 50;
                    ResLighning = 40;
                    Speed = 0.6f;
                    break;
                case "spiderLarge":
                    Gold = 40;
                    Health = 400;
                    ResFire = 0;
                    ResIce = 50;
                    ResLighning = 40;
                    Speed = 0.5125f;
                    break;
                case "werebear":
                    Gold = 100;
                    Health = 850;
                    ResFire = 40;
                    ResIce = 35;
                    ResLighning = 25;
                    Speed = 0.375f;
                    break;
                case "werebearWhite":
                    Gold = 150;
                    Health = 1000;
                    ResFire = 30;
                    ResIce = 60;
                    ResLighning = 35;
                    Speed = 0.32f;
                    break;
                case "minoraur":
                    Gold = 250;
                    Health = 1750;
                    ResFire = 60;
                    ResIce = 20;
                    ResLighning = 40;
                    Speed = 0.25f;
                    break;
                case "bandit":
                    Gold = 30;
                    Health = 200;
                    ResFire = 5;
                    ResIce = 25;
                    ResLighning = 35;
                    Speed = 0.7f;
                    break;
                case "banditHeavy":
                    Gold = 60;
                    Health = 350;
                    ResFire = 15;
                    ResIce = 35;
                    ResLighning = 20;
                    Speed = 0.45f;
                    break;
                case "banditElite":
                    Gold = 90;
                    Health = 500;
                    ResFire = 20;
                    ResIce = 15;
                    ResLighning = 20;
                    Speed = 0.6f;
                    break;
                case "goblinRed":
                    Gold = 300;
                    Health = 300;
                    ResFire = 75;
                    ResIce = 0;
                    ResLighning = 0;
                    break;
                case "goblinBlue":
                    Gold = 300;
                    Health = 300;
                    ResFire = 0;
                    ResIce = 75;
                    ResLighning = 0;
                    break;
                case "goblinBlack":
                    Gold = 300;
                    Health = 300;
                    ResFire = 50;
                    ResIce = 50;
                    ResLighning = 50;
                    break;
                case "goblinGreen":
                    Gold = 300;
                    Health = 300;
                    ResFire = 10;
                    ResIce = 10;
                    ResLighning = 60;
                    break;
                case "goblinOrange":
                    Gold = 300;
                    Health = 300;
                    ResFire = 60;
                    ResIce = 10;
                    ResLighning = 25;
                    break;
                case "goblinPink":
                    Gold = 300;
                    Health = 300;
                    ResFire = 40;
                    ResIce = 50;
                    ResLighning = 40;
                    break;
                case "goblinPurple":
                    Gold = 300;
                    Health = 300;
                    ResFire = 15;
                    ResIce = 60;
                    ResLighning = 20;
                    break;
                case "goblinWhite":
                    Gold = 300;
                    Health = 300;
                    ResFire = 0;
                    ResIce = 90;
                    ResLighning = 0;
                    break;
                case "goblinYellow":
                    Gold = 300;
                    Health = 300;
                    ResFire = 0;
                    ResIce = 0;
                    ResLighning = 75;
                    break;

            }

        }
        public void SetDamage()
        {
            switch (Name)
            {
                //set damage here
                case "skeleton":
                    HitDamage = 5;
                    PassDamage = 1;
                    break;
                case "ant":
                    HitDamage = 2;
                    PassDamage = 1;
                    break;
                case "fireAnt":
                    HitDamage = 3;
                    PassDamage = 1;
                    break;
                case "iceAnt":
                    HitDamage = 4;
                    PassDamage = 1;
                    break;
                case "spider":
                    HitDamage = 2;
                    PassDamage = 1;
                    break;
                case"spiderRegular":
                    HitDamage = 4;
                    PassDamage = 2;
                    break;
                case "spiderLarge":
                    HitDamage = 4;
                    PassDamage = 2;
                    break;
                case "werebear":
                    HitDamage = 15;
                    PassDamage = 3;
                    break;
                case "werebearWhite":
                    HitDamage = 20;
                    PassDamage = 3;
                    break;
                case "minoraur":
                    HitDamage = 35;
                    PassDamage = 5;
                    break;
                case "bandit":
                    HitDamage = 2;
                    PassDamage = 1;
                    break;
                case "banditHeavy":
                    HitDamage = 10;
                    PassDamage = 2;
                    break;
                case "banditElite":
                    HitDamage = 5;
                    PassDamage = 1;
                    break;
                case "goblinRed":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;
                case "goblinBlue":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;
                case "goblinBlack":
                    HitDamage = 20;
                    PassDamage = 2;
                    break;
                case "goblinGreen":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;
                case "goblinOrange":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;
                case "goblinPink":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;
                case "goblinPurple":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;
                case "goblinWhite":
                    HitDamage = 20;
                    PassDamage = 2;
                    break;
                case "goblinYellow":
                    HitDamage = 10;
                    PassDamage = 1;
                    break;

            }
        }

    }
}
