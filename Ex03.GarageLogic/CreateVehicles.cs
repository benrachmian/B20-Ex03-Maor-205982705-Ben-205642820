using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eTypeOfVehicle
    {
        RegularMotorcycle = 1,
        ElectricMotorcycle,
        RegularCar,
        ElectricCar,
        Truck
    }
    public class CreateVehicles
    {
        public static Vehicle CreateVehicle(eTypeOfVehicle i_TypeOfVehicle, string i_LicenseNumber)
        {
            switch (i_TypeOfVehicle)
            {
                case eTypeOfVehicle.ElectricCar:
                    {
                        Tire[] electricCarTires = new Tire[4];
                        foreach (Tire tire in electricCarTires)
                        {
                            new Tire(Car.k_MaxPsiInElectricCar);
                        }
                        return new Car(i_LicenseNumber, new BatterySystem(Car.k_MaxBatteryHoursInElectricCar), electricCarTires);
                    }
                case eTypeOfVehicle.ElectricMotorcycle:
                    {
                        Tire[] electricMotorocycleTires = new Tire[2];
                        foreach (Tire tire in electricMotorocycleTires)
                        {
                            new Tire(Motorcycle.k_MaxPsiInElectricMotorcycle);
                        }
                        return new Motorcycle(i_LicenseNumber, new BatterySystem(Motorcycle.k_MaxBatteryHoursInElectricMotorcycle), electricMotorocycleTires);
                    }
                case eTypeOfVehicle.RegularCar:
                    {
                        Tire[] regularCarTires = new Tire[4];
                        for(int i=0;i<4;i++)
                        {
                            regularCarTires[i] = new Tire(Car.k_MaxPsiInFuelCar);
                        }
                        //foreach(Tire tire in regularCarTires)
                        //{
                        //    new Tire(Car.k_MaxPsiInFuelCar);
                        //}
                        return new Car(i_LicenseNumber, new FuelSystem(Car.k_MaxLitersInFuelCar, eFuelType.Octan96), regularCarTires);
                    }
                case eTypeOfVehicle.RegularMotorcycle:
                    {
                        Tire[] RegularMotorcycleTires = new Tire[2];
                        foreach (Tire tire in RegularMotorcycleTires)
                        {
                            new Tire(Motorcycle.k_MaxPsiInRegularMotorcycle);
                        }
                        return new Motorcycle(i_LicenseNumber, new FuelSystem(Motorcycle.k_MaxLitersInFuelMotorcycle, eFuelType.Octan95), RegularMotorcycleTires);
                    }
                default: //eTypeOfVehicle.Truck:
                    {
                        Tire[] TruckTires = new Tire[16];
                        foreach (Tire tire in TruckTires)
                        {
                            new Tire(Truck.k_MaxPsiInTruck);
                        }
                        return new Truck(i_LicenseNumber, new FuelSystem(Truck.k_MaxLitersInFuelTruck, eFuelType.Soler), TruckTires);
                    }
            }
        }
    }
}
