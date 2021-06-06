using System;
using System.Collections.Generic;
using MediatR;
using Application.Location;

namespace ClientApp
{
    public class Store
    {
        private readonly IMediator mediator;

        public Store(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async void Open()
        {
            string command = "";
            string name;
            Console.WriteLine("Hello from the Store!");
            Console.Write("Let's try getting your name: ");
            name = Console.ReadLine();

            Console.WriteLine($"Hello {name}! Welcome to the store!");

            int a = await mediator.Send(new GetItemCount.Query(){itemName = "apples"});
            Console.WriteLine(a);
        }
    }
}
