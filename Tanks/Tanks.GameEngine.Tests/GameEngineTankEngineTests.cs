using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tanks.GameEngine.Tests
{
    [TestClass]
    public class GameEngineTankEngineTests
    {
        public int index = 0;
        [TestMethod]
        public void TestInit()
        {
            int tankInitX = 5, tankInitY = 10;
            var tankInitCorrect = false;
            MoveDirection moveDirection = 0;
            var tank = new List<TankFragment>();
            var tankEngine = new TankEngine();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, new int[100, 100]);
            Assert.IsTrue(tank.Count > 0);
        }

        [TestMethod]
        public void TestShootEnable()
        {
            var tank = new TankEngine();
            Assert.IsTrue(tank.ShootEnable);
        }

        [TestMethod]
        public void TestShootCounter()
        {
            var tank = new TankEngine();
            tank.ShootCounter = 5;
            Assert.IsTrue(tank.ShootCounter == 0);
        }

        [TestMethod]
        public void TestCreateUp()
        {
            var initX = 5;
            var initY = 5;
            var testTank = new List<TankFragment>();
            var tank = new TankEngine();
            testTank = tank.TankCreate(initX, initY, MoveDirection.Up);
            Assert.IsTrue(testTank[0].X == initX && testTank[0].Y == initY &&
                          testTank[1].X == initX + 1 && testTank[1].Y == initY &&
                          testTank[2].X == initX + 2 && testTank[2].Y == initY &&
                          testTank[3].X == initX && testTank[3].Y == initY + 1 &&
                          testTank[4].X == initX + 1 && testTank[4].Y == initY + 1 &&
                          testTank[5].X == initX + 2 && testTank[5].Y == initY + 1 &&
                          testTank[6].X == initX && testTank[6].Y == initY + 2 &&
                          testTank[7].X == initX + 1 && testTank[7].Y == initY + 2 &&
                          testTank[8].X == initX + 2 && testTank[8].Y == initY + 2);
        }

        [TestMethod]
        public void TestCreateDown()
        {
            var initX = 5;
            var initY = 5;
            var testTank = new List<TankFragment>();
            var tank = new TankEngine();
            testTank = tank.TankCreate(initX, initY, MoveDirection.Down);
            Assert.IsTrue(testTank[0].X == initX && testTank[0].Y == initY &&
                          testTank[1].X == initX - 1 && testTank[1].Y == initY &&
                          testTank[2].X == initX - 2 && testTank[2].Y == initY &&
                          testTank[3].X == initX && testTank[3].Y == initY - 1 &&
                          testTank[4].X == initX - 1 && testTank[4].Y == initY - 1 &&
                          testTank[5].X == initX - 2 && testTank[5].Y == initY - 1 &&
                          testTank[6].X == initX && testTank[6].Y == initY - 2 &&
                          testTank[7].X == initX - 1 && testTank[7].Y == initY - 2 &&
                          testTank[8].X == initX - 2 && testTank[8].Y == initY - 2);
        }

        [TestMethod]
        public void TestCreateRight()
        {
            var initX = 5;
            var initY = 5;
            var testTank = new List<TankFragment>();
            var tank = new TankEngine();
            testTank = tank.TankCreate(initX, initY, MoveDirection.Right);
            Assert.IsTrue(testTank[0].X == initX && testTank[0].Y == initY &&
                          testTank[1].X == initX && testTank[1].Y == initY + 1 &&
                          testTank[2].X == initX && testTank[2].Y == initY + 2 &&
                          testTank[3].X == initX - 1 && testTank[3].Y == initY &&
                          testTank[4].X == initX - 1 && testTank[4].Y == initY + 1 &&
                          testTank[5].X == initX - 1 && testTank[5].Y == initY + 2 &&
                          testTank[6].X == initX - 2 && testTank[6].Y == initY &&
                          testTank[7].X == initX - 2 && testTank[7].Y == initY + 1 &&
                          testTank[8].X == initX - 2 && testTank[8].Y == initY + 2);
        }

        [TestMethod]
        public void TestCreateLeft()
        {
            var initX = 5;
            var initY = 5;
            var testTank = new List<TankFragment>();
            var tank = new TankEngine();
            testTank = tank.TankCreate(initX, initY, MoveDirection.Left);
            Assert.IsTrue(testTank[0].X == initX && testTank[0].Y == initY &&
                          testTank[1].X == initX && testTank[1].Y == initY - 1 &&
                          testTank[2].X == initX && testTank[2].Y == initY - 2 &&
                          testTank[3].X == initX + 1 && testTank[3].Y == initY &&
                          testTank[4].X == initX + 1 && testTank[4].Y == initY - 1 &&
                          testTank[5].X == initX + 1 && testTank[5].Y == initY - 2 &&
                          testTank[6].X == initX + 2 && testTank[6].Y == initY &&
                          testTank[7].X == initX + 2 && testTank[7].Y == initY - 1 &&
                          testTank[8].X == initX + 2 && testTank[8].Y == initY - 2);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100,100];
            map.Borders = new List<int>{0, 0, 100, 100};
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            var gameMap = new int[100,100];
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, gameMap);
            var testList = new List<TankFragment>(tank.Count);
            foreach (TankFragment t in tank)
            {
                var item = new TankFragment(t.X, t.Y);
                testList.Add(item);
            }
            tankEngine.TankMove(tank, MoveDirection.Right, map);
            Assert.IsTrue(testList[0].X + 1 == tank[0].X &&
                testList[1].X + 1 == tank[1].X &&
                testList[2].X + 1 == tank[2].X &&
                testList[3].X + 1 == tank[3].X &&
                testList[4].X + 1 == tank[4].X &&
                testList[5].X + 1 == tank[5].X &&
                testList[6].X + 1 == tank[6].X &&
                testList[7].X + 1 == tank[7].X &&
                testList[8].X + 1 == tank[8].X);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            var gameMap = new int[100, 100];
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, gameMap);
            var testList = new List<TankFragment>(tank.Count);
            foreach (TankFragment t in tank)
            {
                var item = new TankFragment(t.X, t.Y);
                testList.Add(item);
            }
            tankEngine.TankMove(tank, MoveDirection.Left, map);
            Assert.IsTrue(testList[0].X - 1 == tank[0].X &&
                testList[1].X - 1 == tank[1].X &&
                testList[2].X - 1 == tank[2].X &&
                testList[3].X - 1 == tank[3].X &&
                testList[4].X - 1 == tank[4].X &&
                testList[5].X - 1 == tank[5].X &&
                testList[6].X - 1 == tank[6].X &&
                testList[7].X - 1 == tank[7].X &&
                testList[8].X - 1 == tank[8].X);
        }

        [TestMethod]
        public void TestMoveUp()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            var gameMap = new int[100, 100];
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, gameMap);
            var testList = new List<TankFragment>(tank.Count);
            foreach (TankFragment t in tank)
            {
                var item = new TankFragment(t.X, t.Y);
                testList.Add(item);
            }
            tankEngine.TankMove(tank, MoveDirection.Up, map);
            Assert.IsTrue(testList[0].Y - 1 == tank[0].Y &&
                testList[1].Y - 1 == tank[1].Y &&
                testList[2].Y - 1 == tank[2].Y &&
                testList[3].Y - 1 == tank[3].Y &&
                testList[4].Y - 1 == tank[4].Y &&
                testList[5].Y - 1 == tank[5].Y &&
                testList[6].Y - 1 == tank[6].Y &&
                testList[7].Y - 1 == tank[7].Y &&
                testList[8].Y - 1 == tank[8].Y);
        }

        [TestMethod]
        public void TestMoveDown()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase {GameMap = new int[100, 100], Borders = new List<int> {0, 0, 100, 100}};
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            var gameMap = new int[100, 100];
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, gameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankMove(tank, MoveDirection.Down, map);
            Assert.IsTrue(testList[0].Y + 1 == tank[0].Y &&
                testList[1].Y + 1 == tank[1].Y &&
                testList[2].Y + 1 == tank[2].Y &&
                testList[3].Y + 1 == tank[3].Y &&
                testList[4].Y + 1 == tank[4].Y &&
                testList[5].Y + 1 == tank[5].Y &&
                testList[6].Y + 1 == tank[6].Y &&
                testList[7].Y + 1 == tank[7].Y &&
                testList[8].Y + 1 == tank[8].Y);
        }

        [TestMethod]
        public void TestCreateOnMap()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, map.GameMap);
            tankEngine.TankCreateOnMap(tank, map);
            Assert.IsTrue(map.GameMap[tank[0].Y, tank[0].X] == 2 &&
                map.GameMap[tank[1].Y, tank[1].X] == 2 &&
                map.GameMap[tank[2].Y, tank[2].X] == 2 &&
                map.GameMap[tank[3].Y, tank[3].X] == 2 &&
                map.GameMap[tank[4].Y, tank[4].X] == 2 &&
                map.GameMap[tank[5].Y, tank[5].X] == 2 &&
                map.GameMap[tank[6].Y, tank[6].X] == 2 &&
                map.GameMap[tank[7].Y, tank[7].X] == 2 &&
                map.GameMap[tank[8].Y, tank[8].X] == 2);
        }

        [TestMethod]
        public void TestDeleteFromMap()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, map.GameMap);
            tankEngine.TankDeleteFromMap(tank, map);
            Assert.IsTrue(map.GameMap[tank[0].Y, tank[0].X] == 0 &&
                map.GameMap[tank[1].Y, tank[1].X] == 0 &&
                map.GameMap[tank[2].Y, tank[2].X] == 0 &&
                map.GameMap[tank[3].Y, tank[3].X] == 0 &&
                map.GameMap[tank[4].Y, tank[4].X] == 0 &&
                map.GameMap[tank[5].Y, tank[5].X] == 0 &&
                map.GameMap[tank[6].Y, tank[6].X] == 0 &&
                map.GameMap[tank[7].Y, tank[7].X] == 0 &&
                map.GameMap[tank[8].Y, tank[8].X] == 0);
        }

        [TestMethod]
        public void TestDirectionUpIsBlocked()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Up, map.GameMap);
            map.GameMap[tank[0].Y - 1, tank[0].X] = 1;
            map.GameMap[tank[1].Y - 1, tank[1].X] = 1;
            map.GameMap[tank[2].Y - 1, tank[2].X] = 1;
            Assert.IsTrue(tankEngine.DirectionIsBlocked(tank, MoveDirection.Up, map));
        }

        [TestMethod]
        public void TestDirectionDownIsBlocked()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Down, map.GameMap);
            map.GameMap[tank[0].Y + 1, tank[0].X] = 1;
            map.GameMap[tank[1].Y + 1, tank[1].X] = 1;
            map.GameMap[tank[2].Y + 1, tank[2].X] = 1;
            Assert.IsTrue(tankEngine.DirectionIsBlocked(tank, MoveDirection.Down, map));
        }

        [TestMethod]
        public void TestDirectionRightIsBlocked()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, map.GameMap);
            map.GameMap[tank[0].Y, tank[0].X + 1] = 1;
            map.GameMap[tank[1].Y, tank[1].X + 1] = 1;
            map.GameMap[tank[2].Y, tank[2].X + 1] = 1;
            Assert.IsTrue(tankEngine.DirectionIsBlocked(tank, MoveDirection.Right, map));
        }

        [TestMethod]
        public void TestDirectionLeftIsBlocked()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 5, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            map.GameMap[tank[0].Y, tank[0].X - 1] = 1;
            map.GameMap[tank[1].Y, tank[1].X - 1] = 1;
            map.GameMap[tank[2].Y, tank[2].X - 1] = 1;
            Assert.IsTrue(tankEngine.DirectionIsBlocked(tank, MoveDirection.Left, map));
        }

        [TestMethod]
        public void TestNearUpBorder()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 1;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Up, map.GameMap);
            Assert.IsTrue(tankEngine.TankNearBorder(tank, MoveDirection.Up, map.Borders));
        }

        [TestMethod]
        public void TestNearDownBorder()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 10, tankInitY = 98;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Down, map.GameMap);
            Assert.IsTrue(tankEngine.TankNearBorder(tank, MoveDirection.Down, map.Borders));
        }

        [TestMethod]
        public void TestNearRightBorder()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 98, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Right, map.GameMap);
            Assert.IsTrue(tankEngine.TankNearBorder(tank, MoveDirection.Right, map.Borders));
        }

        [TestMethod]
        public void TestNearLeftBorder()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            Assert.IsTrue(tankEngine.TankNearBorder(tank, MoveDirection.Left, map.Borders));
        }

        [TestMethod]
        public void TestShoot()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            tankEngine.ShootEnable = false;
            int count = tankEngine.Bullets.Count;
            tankEngine.Shoot(tank[1].X, tank[1].Y, MoveDirection.Left);
            Assert.IsTrue(count == tankEngine.Bullets.Count);
        }

        [TestMethod]
        public void TestShoot1()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            tankEngine.ShootEnable = true;
            int count = tankEngine.Bullets.Count;
            tankEngine.Shoot(tank[1].X, tank[1].Y, MoveDirection.Left);
            Assert.IsTrue(count + 1 == tankEngine.Bullets.Count);
        }

        [TestMethod]
        public void TestShoot2()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            tankEngine.ShootEnable = true;
            int count = tankEngine.Bullets.Count;
            tankEngine.Shoot(tank[1].X, tank[1].Y, MoveDirection.Left);
            Assert.IsTrue(tankEngine.ShootEnable == false);
        }

        [TestMethod]
        public void TestMakeShoot()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            tankEngine.ShootEnable = false;
            int count = tankEngine.Bullets.Count;
            tankEngine.MakeShoot(tank[1].X - 1, tank[1].Y, MoveDirection.Left);
            Assert.IsTrue(tankEngine.Bullets.Exists(bull => bull.X == tank[1].X - 1 &&
                bull.Y == tank[1].Y && bull.Direction == MoveDirection.Left));
        }

        [TestMethod]
        public void TestMakeShoot1()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            tankEngine.ShootEnable = false;
            int count = tankEngine.Bullets.Count;
            tankEngine.MakeShoot(tank[1].X - 1, tank[1].Y, MoveDirection.Left);
            Assert.IsTrue(tankEngine.ShootEnable == false);
        }

        [TestMethod]
        public void TestTurnUpLeft()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank,MoveDirection.Left, MoveDirection.Up);
            Assert.IsTrue(tank[0].X == testList[6].X &&
                          tank[0].Y == testList[6].Y &&
                          tank[1].X == testList[6].X &&
                          tank[1].Y == testList[6].Y - 1 &&
                          tank[2].X == testList[6].X &&
                          tank[2].Y == testList[6].Y - 2 &&
                          tank[3].X == testList[6].X + 1 &&
                          tank[3].Y == testList[6].Y &&
                          tank[4].X == testList[6].X + 1 &&
                          tank[4].Y == testList[6].Y - 1 &&
                          tank[5].X == testList[6].X + 1 &&
                          tank[5].Y == testList[6].Y - 2 &&
                          tank[6].X == testList[6].X + 2 &&
                          tank[6].Y == testList[6].Y &&
                          tank[7].X == testList[6].X + 2 &&
                          tank[7].Y == testList[6].Y - 1 &&
                          tank[8].X == testList[6].X + 2 &&
                          tank[8].Y == testList[6].Y - 2);
        }

        [TestMethod]
        public void TestTurnRightLeft()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Left, MoveDirection.Right);
            Assert.IsTrue(tank[0].X == testList[8].X &&
                          tank[0].Y == testList[8].Y &&
                          tank[1].X == testList[8].X &&
                          tank[1].Y == testList[8].Y - 1 &&
                          tank[2].X == testList[8].X &&
                          tank[2].Y == testList[8].Y - 2 &&
                          tank[3].X == testList[8].X + 1 &&
                          tank[3].Y == testList[8].Y &&
                          tank[4].X == testList[8].X + 1 &&
                          tank[4].Y == testList[8].Y - 1 &&
                          tank[5].X == testList[8].X + 1 &&
                          tank[5].Y == testList[8].Y - 2 &&
                          tank[6].X == testList[8].X + 2 &&
                          tank[6].Y == testList[8].Y &&
                          tank[7].X == testList[8].X + 2 &&
                          tank[7].Y == testList[8].Y - 1 &&
                          tank[8].X == testList[8].X + 2 &&
                          tank[8].Y == testList[8].Y - 2);
        }

        [TestMethod]
        public void TestTurnDownLeft()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Left, MoveDirection.Down);
            Assert.IsTrue(tank[0].X == testList[2].X &&
                          tank[0].Y == testList[2].Y &&
                          tank[1].X == testList[2].X &&
                          tank[1].Y == testList[2].Y - 1 &&
                          tank[2].X == testList[2].X &&
                          tank[2].Y == testList[2].Y - 2 &&
                          tank[3].X == testList[2].X + 1 &&
                          tank[3].Y == testList[2].Y &&
                          tank[4].X == testList[2].X + 1 &&
                          tank[4].Y == testList[2].Y - 1 &&
                          tank[5].X == testList[2].X + 1 &&
                          tank[5].Y == testList[2].Y - 2 &&
                          tank[6].X == testList[2].X + 2 &&
                          tank[6].Y == testList[2].Y &&
                          tank[7].X == testList[2].X + 2 &&
                          tank[7].Y == testList[2].Y - 1 &&
                          tank[8].X == testList[2].X + 2 &&
                          tank[8].Y == testList[2].Y - 2);
        }

        [TestMethod]
        public void TestTurnLeftRight()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Right, MoveDirection.Left);
            Assert.IsTrue(tank[0].X == testList[8].X &&
                            tank[0].Y == testList[8].Y &&
                            tank[1].X == testList[8].X &&
                            tank[1].Y == testList[8].Y + 1 &&
                            tank[2].X == testList[8].X &&
                            tank[2].Y == testList[8].Y + 2 &&
                            tank[3].X == testList[8].X - 1 &&
                            tank[3].Y == testList[8].Y &&
                            tank[4].X == testList[8].X - 1 &&
                            tank[4].Y == testList[8].Y + 1 &&
                            tank[5].X == testList[8].X - 1 &&
                            tank[5].Y == testList[8].Y + 2 &&
                            tank[6].X == testList[8].X - 2 &&
                            tank[6].Y == testList[8].Y &&
                            tank[7].X == testList[8].X - 2 &&
                            tank[7].Y == testList[8].Y + 1 &&
                            tank[8].X == testList[8].X - 2 &&
                            tank[8].Y == testList[8].Y + 2);
        }

        [TestMethod]
        public void TestTurnUpRight()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Right, MoveDirection.Up);
            Assert.IsTrue(tank[0].X == testList[2].X &&
                          tank[0].Y == testList[2].Y &&
                          tank[1].X == testList[2].X &&
                          tank[1].Y == testList[2].Y + 1 &&
                          tank[2].X == testList[2].X &&
                          tank[2].Y == testList[2].Y + 2 &&
                          tank[3].X == testList[2].X - 1 &&
                          tank[3].Y == testList[2].Y &&
                          tank[4].X == testList[2].X - 1 &&
                          tank[4].Y == testList[2].Y + 1 &&
                          tank[5].X == testList[2].X - 1 &&
                          tank[5].Y == testList[2].Y + 2 &&
                          tank[6].X == testList[2].X - 2 &&
                          tank[6].Y == testList[2].Y &&
                          tank[7].X == testList[2].X - 2 &&
                          tank[7].Y == testList[2].Y + 1 &&
                          tank[8].X == testList[2].X - 2 &&
                          tank[8].Y == testList[2].Y + 2);
        }

        [TestMethod]
        public void TestTurnDownRight()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Right, MoveDirection.Down);
            Assert.IsTrue(tank[0].X == testList[6].X &&
                          tank[0].Y == testList[6].Y &&
                          tank[1].X == testList[6].X &&
                          tank[1].Y == testList[6].Y + 1 &&
                          tank[2].X == testList[6].X &&
                          tank[2].Y == testList[6].Y + 2 &&
                          tank[3].X == testList[6].X - 1 &&
                          tank[3].Y == testList[6].Y &&
                          tank[4].X == testList[6].X - 1 &&
                          tank[4].Y == testList[6].Y + 1 &&
                          tank[5].X == testList[6].X - 1 &&
                          tank[5].Y == testList[6].Y + 2 &&
                          tank[6].X == testList[6].X - 2 &&
                          tank[6].Y == testList[6].Y &&
                          tank[7].X == testList[6].X - 2 &&
                          tank[7].Y == testList[6].Y + 1 &&
                          tank[8].X == testList[6].X - 2 &&
                          tank[8].Y == testList[6].Y + 2);
        }

        [TestMethod]
        public void TestTurnLeftUp()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Up, MoveDirection.Left);
            Assert.IsTrue(tank[0].X == testList[2].X &&
                          tank[0].Y == testList[2].Y &&
                          tank[1].X == testList[2].X + 1 &&
                          tank[1].Y == testList[2].Y &&
                          tank[2].X == testList[2].X + 2 &&
                          tank[2].Y == testList[2].Y &&
                          tank[3].X == testList[2].X &&
                          tank[3].Y == testList[2].Y + 1 &&
                          tank[4].X == testList[2].X + 1 &&
                          tank[4].Y == testList[2].Y + 1 &&
                          tank[5].X == testList[2].X + 2 &&
                          tank[5].Y == testList[2].Y + 1 &&
                          tank[6].X == testList[2].X &&
                          tank[6].Y == testList[2].Y + 2 &&
                          tank[7].X == testList[2].X + 1 &&
                          tank[7].Y == testList[2].Y + 2 &&
                          tank[8].X == testList[2].X + 2 &&
                          tank[8].Y == testList[2].Y + 2);
        }

        [TestMethod]
        public void TestTurnRightUp()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Up, MoveDirection.Right);
            Assert.IsTrue(tank[0].X == testList[6].X &&
                          tank[0].Y == testList[6].Y &&
                          tank[1].X == testList[6].X + 1 &&
                          tank[1].Y == testList[6].Y &&
                          tank[2].X == testList[6].X + 2 &&
                          tank[2].Y == testList[6].Y &&
                          tank[3].X == testList[6].X &&
                          tank[3].Y == testList[6].Y + 1 &&
                          tank[4].X == testList[6].X + 1 &&
                          tank[4].Y == testList[6].Y + 1 &&
                          tank[5].X == testList[6].X + 2 &&
                          tank[5].Y == testList[6].Y + 1 &&
                          tank[6].X == testList[6].X &&
                          tank[6].Y == testList[6].Y + 2 &&
                          tank[7].X == testList[6].X + 1 &&
                          tank[7].Y == testList[6].Y + 2 &&
                          tank[8].X == testList[6].X + 2 &&
                          tank[8].Y == testList[6].Y + 2);
        }

        [TestMethod]
        public void TestTurnDowntUp()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Up, MoveDirection.Down);
            Assert.IsTrue(tank[0].X == testList[8].X &&
                          tank[0].Y == testList[8].Y &&
                          tank[1].X == testList[8].X + 1 &&
                          tank[1].Y == testList[8].Y &&
                          tank[2].X == testList[8].X + 2 &&
                          tank[2].Y == testList[8].Y &&
                          tank[3].X == testList[8].X &&
                          tank[3].Y == testList[8].Y + 1 &&
                          tank[4].X == testList[8].X + 1 &&
                          tank[4].Y == testList[8].Y + 1 &&
                          tank[5].X == testList[8].X + 2 &&
                          tank[5].Y == testList[8].Y + 1 &&
                          tank[6].X == testList[8].X &&
                          tank[6].Y == testList[8].Y + 2 &&
                          tank[7].X == testList[8].X + 1 &&
                          tank[7].Y == testList[8].Y + 2 &&
                          tank[8].X == testList[8].X + 2 &&
                          tank[8].Y == testList[8].Y + 2);
        }

        [TestMethod]
        public void TestTurnLeftDown()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Down, MoveDirection.Left);
            Assert.IsTrue(tank[0].X == testList[6].X &&
                          tank[0].Y == testList[6].Y &&
                          tank[1].X == testList[6].X - 1 &&
                          tank[1].Y == testList[6].Y &&
                          tank[2].X == testList[6].X - 2 &&
                          tank[2].Y == testList[6].Y &&
                          tank[3].X == testList[6].X &&
                          tank[3].Y == testList[6].Y - 1 &&
                          tank[4].X == testList[6].X - 1 &&
                          tank[4].Y == testList[6].Y - 1 &&
                          tank[5].X == testList[6].X - 2 &&
                          tank[5].Y == testList[6].Y - 1 &&
                          tank[6].X == testList[6].X &&
                          tank[6].Y == testList[6].Y - 2 &&
                          tank[7].X == testList[6].X - 1 &&
                          tank[7].Y == testList[6].Y - 2 &&
                          tank[8].X == testList[6].X - 2 &&
                          tank[8].Y == testList[6].Y - 2);
        }
        
        [TestMethod]
        public void TestTurnRightDown()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Down, MoveDirection.Right);
            Assert.IsTrue(tank[0].X == testList[2].X &&
                          tank[0].Y == testList[2].Y &&
                          tank[1].X == testList[2].X - 1 &&
                          tank[1].Y == testList[2].Y &&
                          tank[2].X == testList[2].X - 2 &&
                          tank[2].Y == testList[2].Y &&
                          tank[3].X == testList[2].X &&
                          tank[3].Y == testList[2].Y - 1 &&
                          tank[4].X == testList[2].X - 1 &&
                          tank[4].Y == testList[2].Y - 1 &&
                          tank[5].X == testList[2].X - 2 &&
                          tank[5].Y == testList[2].Y - 1 &&
                          tank[6].X == testList[2].X &&
                          tank[6].Y == testList[2].Y - 2 &&
                          tank[7].X == testList[2].X - 1 &&
                          tank[7].Y == testList[2].Y - 2 &&
                          tank[8].X == testList[2].X - 2 &&
                          tank[8].Y == testList[2].Y - 2);
        }

        [TestMethod]
        public void TestTurnUpDown()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            var testList = new List<TankFragment>(tank.Count);
            testList.AddRange(tank.Select(t => new TankFragment(t.X, t.Y)));
            tankEngine.TankTurn(tank, MoveDirection.Down, MoveDirection.Up);
            Assert.IsTrue(tank[0].X == testList[8].X &&
                          tank[0].Y == testList[8].Y &&
                          tank[1].X == testList[8].X - 1 &&
                          tank[1].Y == testList[8].Y &&
                          tank[2].X == testList[8].X - 2 &&
                          tank[2].Y == testList[8].Y &&
                          tank[3].X == testList[8].X &&
                          tank[3].Y == testList[8].Y - 1 &&
                          tank[4].X == testList[8].X - 1 &&
                          tank[4].Y == testList[8].Y - 1 &&
                          tank[5].X == testList[8].X - 2 &&
                          tank[5].Y == testList[8].Y - 1 &&
                          tank[6].X == testList[8].X &&
                          tank[6].Y == testList[8].Y - 2 &&
                          tank[7].X == testList[8].X - 1 &&
                          tank[7].Y == testList[8].Y - 2 &&
                          tank[8].X == testList[8].X - 2 &&
                          tank[8].Y == testList[8].Y - 2);
        }

        [TestMethod]
        public void TestOnDraw()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            int i = index;
            tankEngine.TankDraw += TestMethod;
            tankEngine.OnTankDraw(tank);
            Assert.IsTrue(i + 1 == index);
        }

        [TestMethod]
        public void TestOnErase()
        {
            var tankEngine = new TankEngine();
            var map = new MapBase();
            map.GameMap = new int[100, 100];
            map.Borders = new List<int> { 0, 0, 100, 100 };
            int tankInitX = 1, tankInitY = 10;
            var tank = new List<TankFragment>();
            tankEngine.TankInit(out tank, tankInitX, tankInitY, MoveDirection.Left, map.GameMap);
            int i = index;
            tankEngine.TankErase += TestMethod;
            tankEngine.OnTankErase(tank);
            Assert.IsTrue(i + 1 == index);
        }


        public void TestMethod(List<TankFragment> list)
        {
            this.index ++;
        }

        [TestMethod]
        public void TestOnBulletErase()
        {
            var bullet = new BulletEngine(1, 1, MoveDirection.Up);
            bullet.BulletErase += TestBulletMethod;
            int i = index;
            bullet.OnBulletErase(bullet);
            Assert.IsTrue(i + 1 == index);
        }

        [TestMethod]
        public void TestOnBulletDraw()
        {
            var bullet = new BulletEngine(1, 1, MoveDirection.Up);
            bullet.BulletDraw += TestBulletMethod;
            int i = index;
            bullet.OnBulletDraw(bullet);
            Assert.IsTrue(i + 1 == index);
        }

        [TestMethod]
        public void TestBulletNearUpWall()
        {
            int[,] map = new int[10,10];
            var bullet = new BulletEngine(1, 1, MoveDirection.Up);
            map[bullet.Y - 1, bullet.X] = 1;
            Assert.IsTrue(bullet.BulletNearWall(bullet, map));
        }

        [TestMethod]
        public void TestBulletNearDownWall()
        {
            int[,] map = new int[10, 10];
            var bullet = new BulletEngine(1, 1, MoveDirection.Down);
            map[bullet.Y + 1, bullet.X] = 1;
            Assert.IsTrue(bullet.BulletNearWall(bullet, map));
        }

        [TestMethod]
        public void TestBulletNearLeftWall()
        {
            int[,] map = new int[10, 10];
            var bullet = new BulletEngine(1, 1, MoveDirection.Left);
            map[bullet.Y, bullet.X - 1] = 1;
            Assert.IsTrue(bullet.BulletNearWall(bullet, map));
        }

        [TestMethod]
        public void TestBulletNearRightWall()
        {
            int[,] map = new int[10, 10];
            var bullet = new BulletEngine(1, 1, MoveDirection.Right);
            map[bullet.Y, bullet.X + 1] = 1;
            Assert.IsTrue(bullet.BulletNearWall(bullet, map));
        }

        public void TestBulletMethod(BulletEngine bullet)
        {
            index++;
        }
    }
}