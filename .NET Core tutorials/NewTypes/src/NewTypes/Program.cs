using System;
using System.Collections.Generic;
using NewTypes.Pets;

namespace NewTypes {
    class Program {
        static void Main (string[] args) {
            var pets = new List<IPet> {
                new Dog (),
                new Cat (),
                new Bird()
            };
            foreach (var pet in pets)
            {
                Console.WriteLine (pet.TalkOwner());
            }
            
        }
    }
}