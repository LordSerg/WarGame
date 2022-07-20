using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_MapMaker
{
    //Цвета (игроки)
    public enum Col : int
    {
        Red,
        Green,
        Blue,
        Yellow,
        Pink,
        Violet,
        Black,
        White,
    }
    //Расы
    public enum Rase
    {
        Human,
        Snake//пока так)
    }
    //список ресурсов:
    public enum Resources
    {
        Tree,
        Rock,
        Crystal,
        Gold
    }
    //списки строений:
    public enum WorkerBuildings
    {
        //ферма - расширяет максимальное количество !рабочих!
        Farm,
        //склад - расширяет максимальное количество  ресурсов
        Warehouse,
        //дом - выработка рабочих
        Cottage,
        //замок - обучает рабочих в воинов
        //расширяет максимальное количество !воинов!
        Castle,
        //стена - защитная оболочка для города
        Wall,
        //ворота - для прохода в город союзникам и изолирования от врагов
        Gate
    }
    public enum WarriorBuildings
    {
        //кузница, где воин может делать мечи и щиты
        Forge,
        //стрелковая башня - обучение стрелков
        ArchTower
    }
    public enum ArcherBuildings
    {
        //место для улучшения стрел и лечение(хилка)
        //(вообщем всякое про яды, химию и медицину)
        Lab,
        //библиотека - обучение волшебников
        Library
    }
    public enum WizzardBuildings
    {
        //магическая башня - стреляет
        MagicTower,
        //портал - ворота, для быстрого перемещения куда-либо,
        //строятся парно
        Portal
    }
    //пасхалки:
    public enum WWWWBuildings
    {//при выдилении одновременно и воина и мага и рабочего и стрелка - можно построить:
        //LookOutTower//смотровая башня с большим радиусом обзора
        MegaCastle//Замок, можно будет придумать какие-то плюшки
    }
    public enum WWWizzBuildings
    {//при выделении трех магов можно сделать голема (как существо)
        Golem//TODO
    }
    public enum AAABuildings
    {//при выделении трех стрелков можно сделать 
        //потом придумаю(
    }
    public enum Spells
    {
        //боевые:
        Lightning,//сильный удар по отряду
        Resurrection,//воскрешение кого-либо умершего в этом бою 
                     //из своего отряда
        Illness,//отравить чужой отряд (-2 жизни 3 хода подряд)
        //мирные:
        Motivation,//юнит может выполнить в 2 раза больше действий
                   //наложить можно только на одного юнита при чем один раз за день
                   //
        EarthWatch//просмотр какой-либо территории
    }
}
