﻿using BSerializer.BaseTypes;
using BSerializer.Core.Collection;
using BSerializer.Core.Custom;
using BSerializer.Core.Parser.SerializationNodes;
using ConsoleUI.Model;
using Library.Extractors;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomSerializer customSerializer = new CustomSerializer(typeof(Person));

            GenericSerializer<Person> serializer = new GenericSerializer<Person>();
            GenericSerializer<IPerson> serializerInterface = new GenericSerializer<IPerson>();

            var person = new Person() { Id = 123, FirstName = "Bloodthirst",LastName ="Ketsueki" , Address = "Some place", Parent = null };

            string interfaceSerialized = serializerInterface.Serialize(person);
            IPerson interfaceDiserialized = serializerInterface.Deserialize(interfaceSerialized);

            string serialized = serializer.Serialize(person);
            Person deserialized = serializer.Deserialize(serialized);

        }
    }
}
