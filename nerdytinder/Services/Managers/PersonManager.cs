using System;
using System.Threading.Tasks;
using System.Linq;


namespace nerdytinder
{
	public class PersonManager : BaseManager<Person>
	{
		public override string Identifier => "Person";

		public Task<Person> GetPersonByEmail(string email)
		{
			return new Task<Person>(() =>
			{
				//We hit the backend table instead of local store since we might not have data
				var list = AzureService.Instance.Client.GetTable<Person>().Where(a => a.Email == email).ToListAsync().Result;
				var person = list.FirstOrDefault();
				return person;
			});
		}

		async public override Task<bool> UpdateAsync(Person item)
		{
			var result = await base.UpdateAsync(item);

			if (item.Id == App.Instance.CurrentNerd?.Id)
			{
				Task.Run(async () =>
				{
					App.Instance.CurrentNerd = await AzureService.Instance.PersonManager.Table.LookupAsync(item.Id);
				}).Wait();
			}

			return result;
		}
	}
}

