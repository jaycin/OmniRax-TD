using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using OmniLibrary.Towers;
using OmniLibrary.EnemyEngine;
using OmniLibrary.Map;

namespace OmniLibrary
{
    public class ObjectManager:List<GameObject>
    {
        #region Fields and Encapsulation
        private static SpriteFont spriteFont;
        MovementGrid Grid;
        int moneyGain;
        List<Texture2D> EffectList;
        
        public int MoneyGain
        {
            get { return moneyGain; }
            set { moneyGain = value; }
        }
        int damageGiven;

        public int DamageGiven
        {
            get { return damageGiven; }
            set { damageGiven = value; }
        }
        int TotalGold;
        bool btnDown;
        bool btnRightDown;
        public bool isWin;
        public bool Enabled;
        List<GameObject> addLst;
        List<GameObject> removeLst;
        public List<Skeleton> skellyList;
        List<MageTower>MageLst;
        List<ArcherTower>ArcherLst;
        List<Projectile> plist;
        List<Texture2D> projectiles;
        //Garbage Collection
        public List<GameObject> RemoveLst
        {
            get { return removeLst; }
            set { removeLst = value; }
        }
        //This list acts as a holder for new game objects because you cannot add while looping the object manager

        public List<GameObject> AddLst
        {
            get { return addLst; }
            set { addLst = value; }
        }
        public static SpriteFont SpriteFont
        {
            get { return ObjectManager.spriteFont; }
            set { ObjectManager.spriteFont = value; }
        }
        public EventHandler _MoneyGain;
        public EventHandler _DamageGive;
        #endregion
        #region Constuctors
        public ObjectManager(SpriteFont spriteFont,List<Texture2D> Projectile,MovementGrid grid)
        {
            Grid = grid;
            SpriteFont = spriteFont;
            btnDown = false;
            btnRightDown = false;
            AddLst = new List<GameObject>();
            plist = new List<Projectile>();
            RemoveLst = new List<GameObject>();
            projectiles = Projectile;
            MageLst = new List<MageTower>();
            ArcherLst = new List<ArcherTower>();
            skellyList = new List<Skeleton>();
            
        }
        public ObjectManager(SpriteFont spriteFont, List<Texture2D> Projectile, MovementGrid grid,List<Texture2D> effectList)
        {
            EffectList = effectList;
            Grid = grid;
            SpriteFont = spriteFont;
            btnDown = false;
            btnRightDown = false;
            AddLst = new List<GameObject>();
            plist = new List<Projectile>();
            RemoveLst = new List<GameObject>();
            projectiles = Projectile;
            MageLst = new List<MageTower>();
            ArcherLst = new List<ArcherTower>();
            skellyList = new List<Skeleton>();
            Enabled = true;
            isWin = false;

        }
        public ObjectManager(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
            btnDown = false;
            AddLst = new List<GameObject>();
            RemoveLst = new List<GameObject>();
            MageLst = new List<MageTower>();
            Enabled = true;
            skellyList = new List<Skeleton>();
            isWin = false;
        }
        #endregion
        #region Methods
        public void Update(GameTime gameTime)
        {
                //fires events for all objects 
            if (Enabled)
            {
                foreach (GameObject obj in this)
                {

                    if (isWin)
                    {
                        if (skellyList.Count == 0)
                        {
                          
                                obj.OnWin(null);
                           
                           
                        }
                    }
                    if (obj.Location.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && obj.IsEnabled)
                    {
                        obj.OnMouseOver(null);
                    }
                    if (!obj.Location.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && obj.MouseOver == true && obj.IsEnabled)
                    {
                        obj.OnMouseLeave(null);
                    }

                    if (!obj.IsLocked && obj.IsEnabled)
                    {
                        if (obj.MouseOver == true && !btnDown && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            obj.OnSelected(null);
                            btnDown = true;
                        }
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Released && btnDown == true)
                    {
                        btnDown = false;
                    }
                    if (!obj.IsLocked && obj.IsEnabled)
                    {
                        if (obj.MouseOver == true && !btnRightDown && Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            obj.OnRightClick(null);
                            btnRightDown = true;
                        }
                    }
                    if (Mouse.GetState().RightButton == ButtonState.Released && btnRightDown== true)
                    {
                        btnRightDown = false;
                    }
                    if (obj.IsEnabled)
                    {
                        obj.Update(gameTime);
                    }
                    if (obj.IsDead)
                    {
                        RemoveLst.Add(obj);
                    }
                    if (obj.Type == "MessageBox" && !obj.MouseOver && Mouse.GetState().LeftButton == ButtonState.Pressed && btnDown == false)
                    {
                        btnDown = true;
                        obj.IsDead = true;
                        unlockPlots();
                        removeConainers();
                    }
                    if (obj.Type == "Projectile")
                    {
                        Projectile p = (Projectile)obj;
                        if (p.Explosion)
                        {
                            foreach (Square sq in Grid.SquareList)
                            {
                                if (skellyList.Count != 0)
                                {
                                    foreach (Skeleton s in skellyList)
                                    {
                                        if (p.Location.Contains(new Point((int)s.Origin.X, (int)s.Origin.Y)))
                                        {
                                            s.TakeDamage(p.Damage);
                                        }
                                    }
                                    p.Damage = 0;
                                }

                            }
                        }
                    }
                    if (obj.Type == "Skelly")
                    {
                        Skeleton s = (Skeleton)obj;
                        if (skellyList.Count != 0)
                        {

                            foreach (Skeleton sL in skellyList)
                            {
                                if (s.EnemyId == sL.EnemyId)
                                {
                                    s.CurrentHealth = sL.CurrentHealth;
                                }
                            }
                        }
                    }
                    if (obj.Type == "Wave")
                    {
                        Wave w = (Wave)obj;
                        if (w.checkEnd())
                        {
                            w.Wave_completed(w, null);
                        }

                    }


                }
                //if (Grid != null)
                //{
                //    foreach (Square sq in Grid.SquareList)
                //    {
                //        sq.Damage = 0;
                //    } 
                //}
                if (MageLst.Count != 0)
                {
                    foreach (MageTower m in MageLst)
                    {
                        foreach (Skeleton s in skellyList)
                        {
                            if (m.CircleRectangle.Contains((int)s.Origin.X, (int)s.Origin.Y) && m.CoolDown >= m.FireTime)
                            {
                                //Bullet b = new Bullet(projectiles[0], 8, m.FirePoint, s.Origin, s.EnemyId, this,Grid);

                                if (!m.isIce)
                                {
                                    plist.Add(new Projectile(projectiles[0], 8, projectiles[1], 4, 3, m.FirePoint, s.EnemyId, this, Grid, 4f, m.Damage, 1));
                                }
                                else
                                    plist.Add(new Projectile(projectiles[3], 8, projectiles[4], 3, 6, m.FirePoint, s.EnemyId, this, Grid, 5f, m.Damage, 0));
                                m.shots--;

                                m.CoolDown = 0;


                            }
                        }
                    }
                }
                //adds new items after updating
                foreach (GameObject obj in AddLst)
                {
                    this.Add(obj);
                    if (obj.IsTower)
                    {
                        MageLst.Add((MageTower)obj);
                    }
                    if (obj.IsEnemy)
                    {
                        skellyList.Add((Skeleton)obj);
                    }

                }
                if (plist != null)
                {
                    foreach (Projectile p in plist)
                    {
                        this.Add(p);
                    }
                    plist.Clear();
                }

                AddLst.Clear();
                //removes items after updating
                foreach (GameObject obj in RemoveLst)
                {

                    if (obj.IsTower)
                    {
                        MageLst.Remove((MageTower)obj);
                    }
                    if (obj.IsEnemy)
                    {
                        Skeleton s = (Skeleton)obj;
                        DropEffect goldDrop;
                        if (s.Gold < 50)
                        {
                            goldDrop = new DropEffect(EffectList[0], new Vector2(s.Position.X, s.Position.Y - 10), 1, 6, 0, 4, 4);
                        }
                        else if (s.Gold >= 50 && s.Gold < 150)
                        {
                            goldDrop = new DropEffect(EffectList[0], new Vector2(s.Position.X, s.Position.Y - 10), 1, 6, 0, 4, 4);
                        }
                        else
                        {
                            goldDrop = new DropEffect(EffectList[0], new Vector2(s.Position.X, s.Position.Y - 10), 1, 6, 0, 4, 4);
                        }

                        this.addLst.Add(goldDrop);
                        if (s.CurrentHealth > 0)
                        {
                            skellyList.Remove((Skeleton)obj);
                            DamageGiven += s.PassDamage;

                        }
                        else
                        {
                            skellyList.Remove((Skeleton)obj);
                            MoneyGain += s.Gold;
                        }

                    }
                    if (obj.Type == "GraveStone")
                    {
                        isWin = true;
                    }
                    this.Remove(obj);
                }
                RemoveLst.Clear();




                
            }
              

        }
        public int FindTarget(int Id)
        {
            
            foreach (GameObject obj in this)
            {
                if (obj.IsEnemy)
                {
                    if (obj.EnemyId == Id)
                    {
                        Skeleton s = (Skeleton)obj;
                        Queue<int> Clone = new Queue<int>(s.walkPath.ToArray());
                        if (Clone.Count > 1) 
                        Clone.Dequeue();

                        return Clone.Peek();
                    }
                }

            }
            return 0;
        }
        public Rectangle GameRectangle()
        {
            foreach (GameObject obj in this)
            {
                if (obj.IsMap)
                {
                    MapLandscape Map = (MapLandscape)obj;

                    return Map.DestRect;
                    
                }

            }
            return Rectangle.Empty;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in this)
            {
                obj.Draw(spriteBatch);
            }
        }
        
        #endregion
        #region Abstact methods
        public void lockPlots()
        {
            foreach (GameObject obj in this)
            {
                if (obj.Type == "Plot")
                {
                    obj.IsEnabled = false;
                }
            }
        }
        public void LockTowers()
        {
            foreach (GameObject obj in this)
            {
                if (obj.IsTower)
                {
                    obj.IsEnabled = false;
                }
            }
        }
        public void unLockTowers()
        {
            foreach (GameObject obj in this)
            {
                if (obj.IsTower)
                {
                    obj.IsEnabled = true;
                }
            }
        }
        public void unlockPlots()
        {
            foreach (GameObject obj in this)
            {
                
                if (obj.Type == "Plot")
                {
                    Plot p = (Plot)obj;
                    if (p.HasTower)
                    {
                    }
                    else
                    obj.IsEnabled = true;
                }
            }
        }
        public void removeConainers()
        {
            foreach (GameObject obj in this)
            {
                if (obj.Name == "Container"&&obj.Type != "Wave")
                {
                    obj.IsDead = true;
                }
                if (obj.Type == "MessageBox")
                {
                    obj.IsDead = true;
                }
            }
            this.unlockPlots();
            this.unLockTowers();
        }
        public int TakeMoney()
        {
            int x = MoneyGain;
            MoneyGain = 0;

            return x;

        }
        public int TakeDamage()
        {
            int x = DamageGiven;
            DamageGiven = 0;
            return x;
        }
        public void RemoveGrave(int GraveId)
        {
             
            foreach (GameObject obj in this)
            {
                if (obj.Type == "GraveStone")
                {
                    GraveStone g = (GraveStone)obj;
                    if (g.GraveStoneId == GraveId)
                    {
                        obj.IsDead = true;
                    }
                }
            }
        }
        public void removeWaves()
        {
            foreach (GameObject obj in this)
            {
                if (obj.Type == "Wave")
                {
                   Wave g = (Wave)obj;
                    
                        obj.IsDead = true;
                    
                }
                if (obj.IsEnemy)
                {
                    Skeleton s = (Skeleton)obj;
                    obj.IsDead = true;
                }
                if (obj.IsTower)
                {
                    MageTower m = (MageTower)obj;
                    obj.IsDead = true;
                }
            }
            lockPlots();
        }
        #endregion
    }
}
