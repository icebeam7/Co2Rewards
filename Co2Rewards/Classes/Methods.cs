using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using IsolatedStorageSettings;

namespace Co2Rewards
{
	public class Methods<T> 
	{
		private MobileServiceCollection<T, T> collection;
		private MobileServiceClient MobileService;
		private IMobileServiceTable<T> table;

		private string applicationURL = @"https://co2rewards.azure-mobile.net/";
		private string applicationKey = @"";// tester: ask me for key: luis.beltran@itcelaya.edu.mx

		public Methods ()
		{
			MobileService = new MobileServiceClient(applicationURL, applicationKey);
			table = MobileService.GetTable<T> ();
		}

		public static string GetID()
		{
			if (IsolatedStorageSettings.Settings.ID.Value == "")
				IsolatedStorageSettings.Settings.ID.Value = Guid.NewGuid ().ToString ();

			return IsolatedStorageSettings.Settings.ID.Value;
		}

		public async Task<bool> Save(T item, bool isNew)
		{
			try
			{
				if (isNew)
					await table.InsertAsync(item);
				else
					await table.UpdateAsync(item);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Advertisement> GetAdvertisementById(string id)
		{
			return (await (((IMobileServiceTable<Advertisement>)table).Where (item => item.Id == id).ToListAsync ())).FirstOrDefault ();
		}

		public async Task<List<Advertisement>> GetAdvertisementsByCompany(string idCompany)
		{
			return (await (((IMobileServiceTable<Advertisement>)table).Where (item => item.IDCompany == idCompany).ToListAsync ())).ToList ();
		}

		public async Task<List<Advertisement>> GetAdvertisementsByDate(int month, int year)
		{
			DateTime minDate = new DateTime(year, month, 1, 0, 0, 0);
			DateTime maxDate = minDate.AddMonths (1);

			return (await (((IMobileServiceTable<Advertisement>)table).Where (item => item.MinDate >= minDate && item.MaxDate < maxDate).ToListAsync ())).ToList();
		}

		public async Task<Company> GetCompanyById(string id)
		{
			return (await (((IMobileServiceTable<Company>)table).Where (item => item.Id == id).ToListAsync ())).FirstOrDefault ();
		}

		public async Task<List<Company>> GetCompaniesByName(string name)
		{
			return (await (((IMobileServiceTable<Company>)table).Where (item => item.Name.Contains(name)).ToListAsync ())).ToList();
		}

		public async Task<Reward> GetRewardById(string id)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.Id == id).ToListAsync ())).FirstOrDefault ();
		}

		public async Task<List<Reward>> GetRewardsByTitle(string name)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.Title.Contains(name)).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsByDescription(string name)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.Description.Contains(name)).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsByCO2Level(decimal minCO2, decimal maxCO2)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.MinCO2Level <= maxCO2 && item.MinCO2Level >= minCO2).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsByPrize(string name)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.Prize.Contains(name)).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsByPrizeDescription(string name)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.PrizeDescription.Contains(name)).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsByCompany(string idCompany)
		{
			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.IDCompany == idCompany).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsByDate(int month, int year)
		{
			DateTime minDate = new DateTime(year, month, 1, 0, 0, 0);
			DateTime maxDate = minDate.AddMonths (1);

			return (await (((IMobileServiceTable<Reward>)table).Where (item => item.MinDate >= minDate && item.MaxDate < maxDate).ToListAsync ())).ToList();
		}

		public async Task<List<Reward>> GetRewardsAvailable(int month, int year)
		{
			return (await GetRewardsByDate (month, year)).Where (item => item.Limit > 0).ToList ();
		}

		public async Task<List<Reward>> GetRewardsICanClaim(int month, int year, string idUser)
		{
			UserCO2 user = await GetUserCO2ByDate (idUser, month, year);

			return (user != null)
				? (await GetRewardsAvailable (month, year)).Where (item => item.MinCO2Level <= user.CO2Level).ToList ()
				: new List<Reward> ();
		}

		public async Task<User> GetUserById(string id)
		{
			return (await (((IMobileServiceTable<User>)table).Where (item => item.Id == id).ToListAsync ())).FirstOrDefault ();
		}

		public async Task<UserCO2> GetUserCO2ById(string id)
		{
			return (await (((IMobileServiceTable<UserCO2>)table).Where (item => item.Id == id).ToListAsync ())).FirstOrDefault ();
		}

		public async Task<List<UserCO2>> GetUserCO2ByIdUser(string idUser)
		{
			return (await (((IMobileServiceTable<UserCO2>)table).Where (item => item.IDUser == idUser).ToListAsync ())).ToList ();
		}

		public async Task<UserCO2> GetUserCO2ByDate(string idUser, int month, int year)
		{
			return (await (((IMobileServiceTable<UserCO2>)table).Where (item => item.IDUser == idUser && item.Month == month && item.Year == year).ToListAsync ())).FirstOrDefault();
		}

		public async Task<UserReward> GetUserRewardById(string id)
		{
			return (await (((IMobileServiceTable<UserReward>)table).Where (item => item.Id == id).ToListAsync ())).FirstOrDefault ();
		}

		public async Task<List<UserReward>> GetUserRewardsByIdUser(string idUser)
		{
			return (await (((IMobileServiceTable<UserReward>)table).Where (item => item.IDUser == idUser).ToListAsync ())).ToList ();
		}

		public async Task<List<T>> GetAll()
		{
			return (await (table.ToCollectionAsync ())).ToList ();
		}

		public async Task<MobileServiceCollection<T, T>> Refresh()
		{
			collection = await table.ToCollectionAsync();
			return collection;
		}

		public async void Delete(T item)
		{
			await table.DeleteAsync(item);
		}
	}
}