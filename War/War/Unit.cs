using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    public struct WarriorWeapon
    {
        int AddAtack, AddProtect, AddLife;
        public WarriorWeapon(int atack, int protect, int life)
        {
            AddAtack = atack;
            AddProtect = protect;
            AddLife = life;
        }
    }

    public class Unit : GameObject
    {
        public int speed;
        public int attack, FightRadius;
        protected Rase rase;
        public List<point> WayToGo;
        Building followAfterB;
        Unit followAfterU;//тот, за кем будем бегат
        public Caste cast;
        protected int waitTimes = 3;
        protected int StandartwaitTimes = 3;
        public int ID;
        public Unit() { }
        //1. новое указание (новая цель) - поиск пути
        //2. передвижение поклеточно
        public override void NewOrder(int Xcoord, int Ycoord)
        {
            if (Map.all[Xcoord, Ycoord] == Cell.Free)
            {
                Go(Xcoord, Ycoord);
                act = ToDo.GoToPoint;
                timecount = StandartTimecount;
            }
        }
        public override void Act()
        {
            if (act != ToDo.None)
            {
                if (timecount > 0)
                    timecount--;
                else
                {
                    if (act == ToDo.GoToPoint)
                    {
                        if (WayToGo.Count > 0)
                            Move();
                        else
                            act = ToDo.None;
                    }
                    else if (act == ToDo.Fight)
                    {
                        //-||-
                    }
                    //if (act == ToDo...)
                    //{
                    //    //-||-
                    //}

                    timecount = StandartTimecount;
                }
            }
        }
        public virtual void Go(int x, int y)
        {//перенаправление юнита(нажатие кнопки мыши)
            followAfterB = null;
            followAfterU = null;
            waitTimes = StandartwaitTimes;
            if (Map.all[x, y] == Cell.Woods
                || Map.all[x, y] == Cell.RockMount
                || Map.all[x, y] == Cell.CrystalMount
                || Map.all[x, y] == Cell.GoldMount)
            { }// WayToGo = FindPath(x, y);//тип послали в неизвесность
            else if (Map.all[x, y] == Cell.Free)
                WayToGo = FindPath(x, y);
            else if (Map.all[x, y] == Cell.Unit)
            {//следуем за юнитом
                int n = Map.AllUnits.Count; 
                for (int i = 0; i < n; i++)
                    if (Map.AllUnits[i].X <= x && Map.AllUnits[i].X + Map.AllUnits[i].Size > x
                        && Map.AllUnits[i].Y <= y && Map.AllUnits[i].Y + Map.AllUnits[i].Size > y)//учет габбаритов
                    {
                        followAfterU = Map.AllUnits[i];
                        break;
                    }
                WayToGo = FindPath(x, y);
            }
            else if (Map.all[x, y] == Cell.Building)
            {
                int n = Map.AllBuildings.Count;
                for (int i = 0; i < n; i++)
                    if (Map.AllBuildings[i].X <= x && Map.AllBuildings[i].X + Map.AllBuildings[i].Size > x
                        && Map.AllBuildings[i].Y <= y && Map.AllBuildings[i].Y + Map.AllBuildings[i].Size > y)
                    {
                        followAfterB = Map.AllBuildings[i];
                        break;
                    }
                WayToGo = FindPath(x, y);
            }
        }
        public virtual void Move()
        {
            if (followAfterU != null)
            {//если мы приследуем врага, то по возможности атакуем его
                FindPath(followAfterU.X, followAfterU.Y);
                if (followAfterU.Color != Color)
                    Attack(followAfterU);
                if (followAfterU.Life <= 0)
                    followAfterU = null;
            }
            if (followAfterB != null)
            {//если мы приследуем здание
                if (followAfterB.Color != Color)
                    Attack(followAfterB);
                if (followAfterB.Life <= 0)
                    followAfterB = null;
            }
            if (WayToGo.Count > 0)
            {//
                int i = WayToGo.Count - 1;
                if ((WayToGo[i].x==X&& WayToGo[i].y==Y) ||
                    Map.all[WayToGo[i].x, WayToGo[i].y] == Cell.Free)
                {//двигаемся, если свободно
                    Map.all[X, Y] = Cell.Free;
                    X = WayToGo[i].x;
                    Y = WayToGo[i].y;
                    Map.all[X, Y] = Cell.Unit;
                    WayToGo.RemoveAt(i);
                }
                else
                {//иначе - ждем три раза
                    if (waitTimes < 0)
                    {
                        WayToGo.Clear();
                        act = ToDo.None;
                    }
                    else
                        waitTimes--;
                }
            }
            else
            {
                WayToGo.Clear();
                act = ToDo.None;
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
        public virtual void Attack(int xA,int yA)
        {
            if(Map.all[xA,yA]==Cell.Building)
            {
                foreach(Building b in Map.AllBuildings)
                {
                    if(b.X==xA&&b.Y==yA)
                    {
                        followAfterB = b;
                        break;
                    }
                }
                if (followAfterB != null)
                    Attack(followAfterB);
            }
            if (Map.all[xA, yA] == Cell.Unit)
            {
                foreach (Unit u in Map.AllUnits)
                {
                    if (u.X == xA && u.Y == yA)
                    {
                        followAfterU = u;
                        break;
                    }
                }
                if (followAfterU != null)
                    Attack(followAfterU);
            }
        }
        public virtual void Build(int x, int y, Building home, int index)
        {//лучше всего перегружать эту ф-ю, т.к. у всех юнитов список зданий разный

        }
        
        //
        protected List<point> FindPath(int x,int y)
        {
            point from = new point(X, Y);
            point to = new point(x, y);
            int wdth = Map.w;
            int hght = Map.h;
            List<point> answer = new List<point>();
            answer.Add(to);
            //маркируем карту:
            //double[,] mp = new double[Map.w, Map.h];
            Map.wayBuf = new double[wdth, hght];
            for (int ii = 0; ii < wdth; ii++)
                for (int jj = 0; jj < hght; jj++)
                    Map.wayBuf[ii, jj] = Map.all[ii, jj] == Cell.Free ? 0 : -1;//-1 = препятствие
            int i = from.x, j = from.y;
            Map.wayBuf[i, j] = 200;
            setDist(i, j, 1,to);
            //ищем мин. путь
            if (true)//хз зачем оно надо было, но, походу, надо было...
            {
                i = to.x;
                j = to.y;
                bool brk = false;
                double min;
                int deadline = 0;
                while (i >= 0 && i < wdth && j >= 0 && j < hght && !brk)
                {
                    if (i == from.x && j == from.y)
                    {
                        brk = true;
                        break;
                    }
                    min = Map.wayBuf[i, j];
                    if (i + 1 >= 0 && i + 1 < wdth && j >= 0 && j < hght && Map.wayBuf[i + 1, j] < min && Map.wayBuf[i + 1, j] != -1)
                    {
                        i++;
                        answer.Add(new point(i, j));
                    }
                    else if (i >= 0 && i < wdth && j + 1 >= 0 && j + 1 < hght && Map.wayBuf[i, j + 1] < min && Map.wayBuf[i, j + 1] != -1)
                    {
                        j++;
                        answer.Add(new point(i, j));
                    }
                    else if (i - 1 >= 0 && i - 1 < wdth && j >= 0 && j < hght && Map.wayBuf[i - 1, j] < min && Map.wayBuf[i - 1, j] != -1)
                    {
                        i--;
                        answer.Add(new point(i, j));
                    }
                    else if (i >= 0 && i < wdth && j - 1 >= 0 && j - 1 < hght && Map.wayBuf[i, j - 1] < min && Map.wayBuf[i, j - 1] != -1)
                    {
                        j--;
                        answer.Add(new point(i, j));
                    }

                    else if (i + 1 >= 0 && i + 1 < wdth && j + 1 >= 0 && j + 1 < hght && Map.wayBuf[i + 1, j + 1] < min && Map.wayBuf[i + 1, j + 1] != -1)
                    {
                        i++;
                        j++;
                        answer.Add(new point(i, j));
                    }
                    else if (i + 1 >= 0 && i + 1 < wdth && j - 1 >= 0 && j - 1 < hght && Map.wayBuf[i + 1, j - 1] < min && Map.wayBuf[i + 1, j - 1] != -1)
                    {
                        i++;
                        j--;
                        answer.Add(new point(i, j));
                    }
                    else if (i - 1 >= 0 && i - 1 < wdth && j + 1 >= 0 && j + 1 < hght && Map.wayBuf[i - 1, j + 1] < min && Map.wayBuf[i - 1, j + 1] != -1)
                    {
                        i--;
                        j++;
                        answer.Add(new point(i, j));
                    }
                    else if (i - 1 >= 0 && i - 1 < wdth && j - 1 >= 0 && j - 1 < hght && Map.wayBuf[i - 1, j - 1] < min && Map.wayBuf[i - 1, j - 1] != -1)
                    {
                        i--;
                        j--;
                        answer.Add(new point(i, j));
                    }
                    else
                    {
                        /*answer.RemoveAt(answer.Count - 1);
                        map[answer[answer.Count - 1].X, answer[answer.Count - 1].Y] = 120;
                        i = answer[answer.Count - 1].X;
                        j = answer[answer.Count - 1].Y;*/
                        deadline++;
                    }
                    if (deadline > 100)
                        break;
                }
            }
            //
            return answer;
        }
        int mod(int a, int b)
        {
            while (a < 0)
                a += b;
            while (a >= b)
                a -= b;
            return a;
        }
        void setDist(int i, int j, double val,point to)
        {
            int wdth = Map.w,hght=Map.h;
            if (i == to.x && j == to.y)
            {
                Map.wayBuf[i, j] = val;
            }
            else if (Map.wayBuf[i, j] != -1 && (Map.wayBuf[i, j] == 0 || Map.wayBuf[i, j] > val))
            {
                Map.wayBuf[i, j] = val;
                //if (i + 1 >= 0 && i + 1 < wdth && j >= 0 && j < hght)
                    setDist(mod(i + 1,wdth), j, val + 1,to);
                //if (i >= 0 && i < wdth && j + 1 >= 0 && j + 1 < hght)
                    setDist(i, mod(j + 1,hght), val + 1, to);
                //if (i - 1 >= 0 && i - 1 < wdth && j >= 0 && j < hght)
                    setDist(mod(i - 1,wdth), j, val + 1, to);
                //if (i >= 0 && i < wdth && j - 1 >= 0 && j - 1 < hght)
                    setDist(i, mod(j - 1,hght), val + 1, to);

                //if (i + 1 >= 0 && i + 1 < wdth && j + 1 >= 0 && j + 1 < hght)
                    setDist(mod(i + 1,wdth), mod(j + 1,hght), val + Math.Sqrt(2), to);
                //if (i + 1 >= 0 && i + 1 < wdth && j - 1 >= 0 && j - 1 < hght)
                    setDist(mod(i + 1,wdth), mod(j - 1,hght), val + Math.Sqrt(2), to);
                //if (i - 1 >= 0 && i - 1 < wdth && j + 1 >= 0 && j + 1 < hght)
                    setDist(mod(i - 1,wdth), mod(j + 1,hght), val + Math.Sqrt(2), to);
                //if (i - 1 >= 0 && i - 1 < wdth && j - 1 >= 0 && j - 1 < hght)
                    setDist(mod(i - 1,wdth), mod(j - 1,hght), val + Math.Sqrt(2), to);
            }
        }
        //
    }
    public class Worker : Unit
    {
        //int miningForse;//=attack
        point homePoint;//туда, куда надо носить добычу
        point minePoint;//там, где надо брать добычу
        //public Caste cast = Caste.Worker;
        public Worker() { ID = Map.AllUnits.Count; }
        public Worker(int x, int y,int Lfe, int size, Col col,Rase r,int RV,int spd,int Atack, int xGo, int yGo,int attcR,ToDo thng)
        {//если что-то делает
            cast = Caste.Worker;
            X = x;
            Y = y;
            Life = Lfe;
            Size = size;
            Color = col;
            Rase = r;
            ViewRadius = RV;
            speed = spd;
            attack = Atack;
            act = thng;
            ID = Map.AllUnits.Count;
            if (act != ToDo.None||xGo==-1||yGo==-1)
            {
                if (act == ToDo.GoToPoint)
                {
                    if (Map.all[xGo, yGo] == Cell.Woods
                  || Map.all[xGo, yGo] == Cell.RockMount
                  || Map.all[xGo, yGo] == Cell.CrystalMount
                  || Map.all[xGo, yGo] == Cell.GoldMount)
                    {
                        //Mine(xGo,yGo);
                        Go(xGo,yGo);//пока так
                    }
                    else
                    {
                        Go(xGo,yGo);
                    }
                }
                else if(act==ToDo.Fight)
                {
                    Attack(xGo,yGo);
                }
            }
            else
            {
                WayToGo = new List<point>();
            }
            FightRadius = attcR;
        }
        public override void Build(int x, int y, Building home, int thing)
        {//построение зданий
            WorkerBuildings wb = (WorkerBuildings)thing;
            //проверка на то, хватает ли рес-ов
            if (Map.wood >= 2 && Map.rock >= 1 && Map.gold >= 1)
            {
                //проверка на то, получится ли его вместиить конкретно здесь
                bool b = true;
                for (int i = x; i < x + home.Size; i++)
                    for (int j = y; j < y + home.Size; j++)
                        if (Map.all[i % Map.w, j % Map.h] != Cell.Free)
                        {
                            b = false;
                            break;
                        }
                if (b)
                    Map.Add(home, x, y);
            }
        }
        //public override void Attack(Unit enemy)
        //{//атакуем, если расстояние позволяет

        //}
        public override void Show(Graphics g)
        {

        }
        /*
        public void Mine(int x, int y)
        {//добыча рес-ов

        }
        public override void Go(int x, int y)
        {
            if (map.all[x, y] == Cell.Woods
                || map.all[x, y] == Cell.RockMount
                || map.all[x, y] == Cell.CrystalMount
                || map.all[x, y] == Cell.GoldMount)
                Mine(x, y);
            else if (map.all[x, y] == Cell.Free)
                WayToGo = FindPath(x, y);
        }*/
    }
    public class Warrior : Unit
    {
        public WarriorWeapon w1, w2;
        //public Caste cast = Caste.Warior;
        public Warrior() { ID = Map.AllUnits.Count; }
        public Warrior(int x, int y, int Lfe, int size, Col col, Rase r, int RV, int spd, int Atack, int xGo, int yGo, int attcR, ToDo thng)
        {
            cast = Caste.Warior;
            X = x;
            Y = y;
            Life = Lfe;
            Size = size;
            Color = col;
            Rase = r;
            ViewRadius = RV;
            speed = spd;
            attack = Atack;
            act = thng;
            ID = Map.AllUnits.Count;
            if (act != ToDo.None)
            {
                if (act == ToDo.GoToPoint)
                {
                    Go(xGo, yGo);
                }
                else if (act == ToDo.Fight)
                {
                    Attack(xGo, yGo);
                }
            }
            else
            {
                WayToGo = new List<point>();
            }
            FightRadius = attcR;
        }
        public void Building(int x, int y, WarriorBuildings thing)
        {
            
        }
    }
    public class Archer : Unit
    {
        int numOfShots;
        //public Caste cast = Caste.Archer;
        //Spells[] spells;
        public Archer() { ID = Map.AllUnits.Count; }
        public Archer(int x, int y, int Lfe, int size, Col col, Rase r, int RV, int spd, int Atack, int xGo, int yGo, int attcR, ToDo thng)
        {
            cast = Caste.Archer;
            X = x;
            Y = y;
            Life = Lfe;
            Size = size;
            Color = col;
            Rase = r;
            ViewRadius = RV;
            speed = spd;
            attack = Atack;
            act = thng;
            ID = Map.AllUnits.Count;
            if (act != ToDo.None)
            {
                if (act == ToDo.GoToPoint)
                {
                    Go(xGo, yGo);
                }
                else if (act == ToDo.Fight)
                {
                    Attack(xGo, yGo);
                }
            }
            else
            {
                WayToGo = new List<point>();
            }
            FightRadius = attcR;
        }
        public void Shoot(Unit enemy)
        {
            
        }
        public override void Build(int x, int y, Building home, int index)
        {
            
        }
    }
    public class Wizzard : Unit
    {
        int mana;
        Spells[] spells;
        //public Caste cast = Caste.Wizzard;
        public Wizzard() { ID = Map.AllUnits.Count; }
        public Wizzard(int x, int y, int Lfe, int size, Col col, Rase r, int RV, int spd, int Atack, int xGo, int yGo, int attcR, ToDo thng)
        {
            cast = Caste.Wizzard;
            X = x;
            Y = y;
            Life = Lfe;
            Size = size;
            Color = col;
            Rase = r;
            ViewRadius = RV;
            speed = spd;
            attack = Atack;
            act = thng;
            ID = Map.AllUnits.Count;
            if (act != ToDo.None)
            {
                if (act == ToDo.GoToPoint)
                {
                    Go(xGo, yGo);
                }
                else if (act == ToDo.Fight)
                {
                    Attack(xGo, yGo);
                }
            }
            else
            {
                WayToGo = new List<point>();
            }
            FightRadius = attcR;
        }
        public void Spell(Unit enemy)
        {
            
        }
        public override void Build(int x, int y,Building home, int index)
        {
            
        }
    }
}
