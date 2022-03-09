using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Cars;

namespace CarRest.Managers
{
    public class CarsManager
    {
        private static int NextId = 1;
        private static List<Car> Data = new List<Car>()
        {
            new Car(NextId++, "Mustang", 100, "AB94"),
            new Car(NextId++, "Fiesta", 1000, "AB34"),
            new Car(NextId++, "Prious", 500, "BE22"),
            new Car(NextId++, "Charger", 10000, "PE234")
        };

        public IEnumerable<Car> GetAllCars(string model, int? MaxPrice, String LicensePlate)
        {
            List<Car> cars = Data;

            if (!String.IsNullOrWhiteSpace(model))
            {
                cars = cars.FindAll((c) => c.Model.Contains(model, StringComparison.OrdinalIgnoreCase));
            }

            if (MaxPrice.HasValue)
            {
                cars = cars.FindAll((c) => c.Price <= MaxPrice);
            }

            if (!String.IsNullOrWhiteSpace(LicensePlate))
            {
                cars = cars.FindAll((c) => c.LicensePlate.Contains(LicensePlate, StringComparison.OrdinalIgnoreCase));
            }

            return cars;
        }

        public Car GetCar(int id)
        {
            return Data.Find((c) => c.Id == id);
        }

        public Car PostCar(Car car)
        {
            car.Id = NextId++;
            Data.Add(car);
            return car;
        }

        public Car DeleteCar(int id)
        {
            Car car = GetCar(id);
            if (car == null) return null;
            Data.Remove(car);
            return car;
        }
    }
}
