﻿using System;
using System.Reflection;

namespace intro
{
    // This enumeration defines the possible targets of an attribute.

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Value of VehicleDescriptionAttribute *****\n");
            ReflectAttributesUsingLateBinding();
            Console.ReadLine();
        }

        private static void ReflectAttributesUsingLateBinding()
        {
            try
            {
                // Load the local copy of AttributedCarLibrary.
                Assembly asm = Assembly.Load("intro");
                // Get type info of VehicleDescriptionAttribute.
                Type vehicleDesc = asm.GetType("intro.VehicleDescriptionAttribute");
                // Get type info of the Description property.
                PropertyInfo propDesc = vehicleDesc.GetProperty("Description");
                // Get all types in the assembly.
                Type[] types = asm.GetTypes();
                // Iterate over each type and obtain any VehicleDescriptionAttributes.
                foreach (Type t in types)
                {
                    object[] objs = t.GetCustomAttributes(vehicleDesc, false);
                    // Iterate over each VehicleDescriptionAttribute and print
                    // the description using late binding.
                    foreach (object o in objs)
                    {
                        Console.WriteLine("-> {0}: {1}\n", t.Name, propDesc.GetValue(o, null));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
