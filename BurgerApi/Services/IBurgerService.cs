using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerApi.Models;
using Microsoft.AspNetCore.Http;

namespace BurgerApi.Services
{
    public interface IBurgerService
    {
        //  Queries

        /// <summary>
        /// Get all <see cref="Burger"/> by page count
        /// </summary>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Burger>> GetBurgersAsync(int count, int skip = 0);

        /// <summary>
        /// Get all <see cref="Burger"/> by base type, i.e. Beef, Vegetarian, Pork etc...
        /// </summary>
        /// <param name="baseCode"></param>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Burger>> GetBurgersByBaseTypeAsync(string baseCode, int count, int skip = 0);

        /// <summary>
        /// Get all <see cref="Burger"/> by cuisine, i.e. American, Asian, etc...
        /// </summary>
        /// <param name="cuisineCode"></param>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Burger>> GetBurgersByCuisineAsync(string cuisineCode, int count, int skip = 0);

        /// <summary>
        /// Get random burger full detail(if any) <see cref="GetRandomBurgerPictureAsync"/> for just
        /// picture
        /// </summary>
        /// <returns></returns>
        Task<Burger> GetRandomBurgerAsync();

        /// <summary>
        /// Get random burger picture. For random burger with details <see cref="GetRandomBurgerAsync"/>
        /// </summary>
        /// <returns></returns>
        Task<BurgerImage> GetRandomBurgerPictureAsync();

        //  Create update

        /// <summary>
        /// Save new burger instance
        /// </summary>
        /// <param name="burger"></param>
        /// <returns></returns>
        Task SaveBurgerAsync(Burger burger);

        /// <summary>
        /// Save new burger base instance
        /// </summary>
        /// <param name="burgerBase"></param>
        /// <returns></returns>
        Task SaveBurgerBaseAsync(BurgerBase burgerBase);

        /// <summary>
        /// Save new cuisine instance
        /// </summary>
        /// <param name="cuisine"></param>
        /// <returns></returns>
        Task SaveCuisineAsync(Cuisine cuisine);

        /// <summary>
        /// Save new burger image instance
        /// </summary>
        /// <param name="burgerImage"></param>
        /// <returns></returns>
        Task SaveBurgerImageAsync(BurgerImage burgerImage);

        /// <summary>
        /// Physical burger image file
        /// </summary>
        /// <param name="originalFilename"></param>
        /// <param name="burgerPhoto"></param>
        /// <returns></returns>
        Task<string> SaveBurgerPhotoAsync(string originalFilename, IFormFile burgerPhoto);
    }
}
