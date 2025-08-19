using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class RandomService :IRandomService
	{
		
        private readonly IWriter _writer;
        private readonly IReader _reader;
		public RandomService(IWriter writer, IReader reader)
        {
            _writer = writer;
            _reader = reader;
        }
        public async Task<int> GetRandom()
		{
            
            var random = new Random(GetSeedHash());
            var randomNumbr = random.Next(100);
            var checkNumber = await _reader.CheckSameNumbr(randomNumbr);
            if (!checkNumber)
            {
                await _writer.SaveNumber(randomNumbr);
            }
            
            return randomNumbr;
		}
        public int GetSeedHash()
        {
            return Guid.NewGuid().GetHashCode();
        }

	}
}
