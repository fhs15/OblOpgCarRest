using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRest.Managers;
using System;
using Cars;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRest.Managers.Tests
{
    [TestClass()]
    public class CarsManagerTests
    {
        private CarsManager manager;
        [TestInitialize]
        public void init()
        {
            manager = new CarsManager();
        }
        [TestMethod()]
        public void GetAllCarsTest()
        {
            IEnumerable<Car> cars = manager.GetAllCars(null, null, null);
            Assert.IsNotNull(cars);

            cars = manager.GetAllCars("Prious", 500, "BE22");
            foreach (var car in cars)
            {
                Assert.IsTrue(car.Model.Contains("Prious", StringComparison.OrdinalIgnoreCase) && car.LicensePlate.Contains("BE22", StringComparison.OrdinalIgnoreCase) && car.Price <= 500);
            }
        }

        [TestMethod()]
        public void GetCarTest()
        {
            Car car = manager.GetCar(2);
            Assert.AreEqual("Fiesta", car.Model);
        }

        [TestMethod()]
        public void PostCarTest()
        {
            Car newCar = new Car(33, "Fiat", 200, "AB94");
            Car createCar = manager.PostCar(newCar);

            Assert.AreNotEqual(33, createCar.Id);
            Assert.AreEqual("Fiat", manager.GetCar(createCar.Id).Model);
        }

        [TestMethod()]
        public void DeleteCarTest()
        {
            Car car = manager.DeleteCar(1);

            Assert.IsNull(manager.GetCar(1));

            Assert.IsNull(manager.DeleteCar(20));
        }
    }
}