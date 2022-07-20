using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_MapMaker
{
    //особые вещи:
    public struct WarriorWeapon
    {
        int AddAtack, AddProtect, AddLife;
        public WarriorWeapon(int atack,int protect,int life)
        {
            AddAtack = atack;
            AddProtect = protect;
            AddLife = life;
        }
    }

    public class Unit:GameObject
    {
        public int speed;
        protected int attack, FightRadius;
        protected Rase rase;
        public List<point> WayToGo;
        Building followAfterB;
        Unit followAfterU;//тот, за кем будем бегат
        public Unit() { }
        public Unit(int Attack,int life,Rase r,int x,int y,Map M)
        {
            attack = Attack;
            Life = life;
            rase = r;
            X = x;
            Y = y;
            FightRadius = 5;
            ViewRadius = 7;
            map = M;
        }
        //1. новое указание (новая цель) - поиск пути
        //2. передвижение поклеточно
        public virtual void Go(int x,int y)
        {//перенаправление юнита(нажатие кнопки мыши)
            followAfterB = null;
            followAfterU = null;
            if (map.all[x, y] == Cell.Woods
                || map.all[x, y] == Cell.RockMount
                || map.all[x, y] == Cell.CrystalMount
                || map.all[x, y] == Cell.GoldMount)
            { }// WayToGo = FindPath(x, y);//тип послали в неизвесность
            else if (map.all[x, y] == Cell.Free)
                WayToGo = FindPath(x, y);
            else if(map.all[x,y]==Cell.Unit)
            {//следуем за юнитом
                int n = AllUnits.Count;
                for(int i=0;i<n;i++)
                    if(AllUnits[i].X<=x&&AllUnits[i].X+ AllUnits[i].Size> x 
                        &&AllUnits[i].Y<=y&&AllUnits[i].Y+ AllUnits[i].Size> y)//учет габбаритов
                    {
                        followAfterU = AllUnits[i];
                        break;
                    }
                WayToGo = FindPath(x, y);
            }
            else if(map.all[x,y]==Cell.Building)
            {
                int n = AllBuildings.Count;
                for (int i = 0; i < n; i++)
                    if (AllBuildings[i].X <= x && AllBuildings[i].X + AllBuildings[i].Size > x
                        && AllBuildings[i].Y <= y && AllBuildings[i].Y + AllBuildings[i].Size > y)
                    {
                        followAfterB = AllBuildings[i];
                        break;
                    }
                WayToGo = FindPath(x, y);
            }
        }
        public virtual void Move()
        {
            if(followAfterU!=null)
            {//если мы приследуем врага, то по возможности атакуем его
                FindPath(followAfterU.X,followAfterU.Y);
                if (followAfterU.Color != Color)
                    Attack(followAfterU);
                if (followAfterU.Life <= 0)
                    followAfterU = null;
            }
            if (followAfterB != null)
            {//если мы приследуем здание
                if(followAfterB.Color!=Color)
                    Attack(followAfterB);
                if (followAfterB.Life <= 0)
                    followAfterB = null;
            }
            if (WayToGo.Count>0)
            {//двигаемся
                int i = WayToGo.Count - 1;
                X += WayToGo[i].x;
                Y += WayToGo[i].y;
                WayToGo.RemoveAt(i);
            }
        }
        public virtual void Attack(Unit enemy)
        {
            if ((X - enemy.X) * (X - enemy.X) + (Y - enemy.Y) * (Y - enemy.Y) <= FightRadius * FightRadius)
                enemy.Life -= attack;
        }
        public virtual void Attack(Building enemyhome)
        {
            if ((X - enemyhome.X) * (X - enemyhome.X) + (Y - enemyhome.Y) * (Y - enemyhome.Y) <= FightRadius * FightRadius)
                enemyhome.Life -= attack;
        }
        public virtual void Build(int x, int y, Building home,int index)
        {//лучше всего перегружать эту ф-ю, т.к. у всех юнитов список зданий разный
            
        }
        protected List<point> FindPath(int x,int y)
        {
            List<point>path=new List<point>();
            return path;
        }
    }
    public class Worker : Unit 
    {
        //int miningForse;//=attack
        point homePoint;//туда, куда надо носить добычу
        point minePoint;//там, где надо брать добычу
        public Worker() { }
        public Worker(int Atack, int Life, Rase r, int x, int y, Map M,Col col) 
            :base(Atack, Life, r, x, y, M)
        {
            Color = col;
        }
        public void Mine(int x, int y)
        {//добыча рес-ов
            
        }
        public override void Build(int x, int y, Building home, int thing)
        {//построение зданий
            WorkerBuildings wb = (WorkerBuildings)thing;
            //проверка на то, хватает ли рес-ов
            if (wood >= 2 && rock >= 1 && gold >= 1)
            {
                //проверка на то, получится ли его вместиить конкретно здесь
                bool b = true;
                for (int i = x; i < x + home.Size; i++)
                    for (int j = y; j < y + home.Size; j++)
                        if (map.all[i % map.w, j % map.h] != Cell.Free)
                        {
                            b = false;
                            break;
                        }
                if (b)
                    map.Add(home,x,y);
            }
        }
        public override void Attack(Unit enemy)
        {//атакуем, если расстояние позволяет
            
        }
        public override void Show(Graphics g)
        {
            
        }
        public override void Go(int x, int y)
        {
            if (map.all[x, y] == Cell.Woods
                || map.all[x, y] == Cell.RockMount
                || map.all[x, y] == Cell.CrystalMount
                || map.all[x, y] == Cell.GoldMount)
                Mine(x, y);
            else if(map.all[x, y] == Cell.Free)
                WayToGo = FindPath(x, y);
        }
    }
    public class Warrior : Unit
    {
        public WarriorWeapon w1, w2;
        public void Building(int x, int y, WarriorBuildings thing)
        {
            //TODO
        }
    }
    public class Wizzard : Unit
    {
        int numOfShots, mana;
        Spells[] spells;

        public void Shoot(Unit enemy)
        {
            //TODO
        }
        public void Building(int x, int y, WizzardBuildings thing)
        {
            //TODO
        }
    }
}
