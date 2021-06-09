using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Location
{
    public class GetItemCount
    {
        public class Query : IRequest<int> 
        {
            public string itemName {get; set;}
        }

        public class Handler : IRequestHandler<Query, int>
        {
            public async Task<int> Handle(Query request, CancellationToken ct)
            {
                Console.WriteLine(request.itemName);
                await Task.Delay(10);
                return 10;
            }
        }
    }
}