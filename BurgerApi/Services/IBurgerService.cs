using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Models;
using BurgerApi.Models.Helpers;
using Microsoft.AspNetCore.Http;

namespace BurgerApi.Services
{
    public interface IBurgerService
    {
        //  Queries

        /// <summary>
        /// Get all <see cref="Cuisine"/> types for <see cref="Burger"/>
        /// </summary>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Cuisine>> GetCuisineAsync(int count, int skip = 0);

        /// <summary>
        /// Get a instance of a <see cref="Cuisine"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Cuisine> GetCuisineAsync(int? id);

        /// <summary>
        /// Get all <see cref="BurgerBase"/> by page count
        /// </summary>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<BurgerBase>> GetBurgersBaseAsync(int count, int skip = 0);

        /// <summary>
        /// Get an instance of a <see cref="BurgerBase"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BurgerBase> GetBurgerBaseAsync(int? id);

        /// <summary>
        /// Get all <see cref="Burger"/>'s. This query is pageable
        /// </summary>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Burger>> GetBurgersAsync(int count, int skip = 0);

        /// <summary>
        /// Get all <see cref="Burger"/>'s. This query is pageable
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PagedResult<Burger> GetBurgersPageable(int? page);


        /// <summary>
        /// Get an instance of a <see cref="Burger"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Burger> GetBurgerAsync(int? id);

        //  Persistence


        /// <summary>
        /// Save new cuisine instance
        /// </summary>
        /// <param name="cuisine"></param>
        /// <returns></returns>
        Task SaveCuisineAsync(Cuisine cuisine);

        /// <summary>
        /// Edit <see cref="Cuisine"/> instance
        /// </summary>
        /// <param name="cuisine"></param>
        /// <returns></returns>
        Task<bool> UpdateCuisineAsync(Cuisine cuisine);

        /// <summary>
        /// Save an instance of a <see cref="BurgerBase"/>
        /// </summary>
        /// <param name="burgerBase"></param>
        /// <returns></returns>
        Task SaveBurgerBaseAsync(BurgerBase burgerBase);

        /// <summary>
        /// Edit <see cref="BurgerBase"/> instance
        /// </summary>
        /// <param name="burgerBase"></param>
        /// <returns></returns>
        Task<bool> UpdateBurgerBaseAsync(BurgerBase burgerBase);

        /// <summary>
        /// Create a new instance of a <see cref="Burger"/>
        /// </summary>
        /// <param name="burger"></param>
        /// <returns></returns>
        Task SaveBurgerAsync(Burger burger);

        /// <summary>
        /// Create a instance of a <see cref="BurgerImage"/>.  This is just the
        /// database entity.
        /// </summary>
        /// <param name="burgerImage"></param>
        /// <returns></returns>
        Task SaveBurgerImage(BurgerImage burgerImage);

        /// <summary>
        /// Generates drop down list of <see cref="Cuisine"/> type
        /// </summary>
        /// <returns></returns>
        Task<List<Cuisine>> GetCuisineDropListAsync();

        /// <summary>
        /// Generates dropdown list of <see cref="BurgerBase"/> type
        /// </summary>
        /// <returns></returns>
        Task<List<BurgerBase>> GetBurgerBaseDropListAsync();

        /// <summary>
        /// Check if <see cref="Burger"/> entity has already been added
        /// </summary>
        /// <param name="instagramUrl"></param>
        /// <returns></returns>
        Task<bool> BurgerExistAsync(string instagramUrl);

        /// <summary>
        /// Update a <see cref="Burger"/> instance
        /// </summary>
        /// <param name="burger"></param>
        /// <returns></returns>
        Task<bool> UpdateBurgerAsync(Burger burger);


        /// <summary>
        /// Get all <see cref="Burger"/> by base type, i.e. Beef, Vegetarian, Pork etc...
        /// </summary>
        /// <param name="baseCode"></param>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        //Task<IEnumerable<Burger>> GetBurgersByBaseTypeAsync(string baseCode, int count, int skip = 0);

        /// <summary>
        /// Get all <see cref="Burger"/> by cuisine, i.e. American, Asian, etc...
        /// </summary>
        /// <param name="cuisineCode"></param>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        //Task<IEnumerable<Burger>> GetBurgersByCuisineAsync(string cuisineCode, int count, int skip = 0);

        /// <summary>
        /// Get random burger full detail(if any) <see cref="GetRandomBurgerPictureAsync"/> for just
        /// picture
        /// </summary>
        /// <returns></returns>
        //Task<Burger> GetRandomBurgerAsync();

        /// <summary>
        /// Get random burger picture. For random burger with details <see cref="GetRandomBurgerAsync"/>
        /// </summary>
        /// <returns></returns>
        //Task<BurgerImage> GetRandomBurgerPictureAsync();

        //  Create update

        /// <summary>
        /// Save new burger instance
        /// </summary>
        /// <param name="burger"></param>
        /// <returns></returns>
        //Task SaveBurgerAsync(Burger burger);

        /// <summary>
        /// Save new burger base instance
        /// </summary>
        /// <param name="burgerBase"></param>
        /// <returns></returns>
        //Task SaveBurgerBaseAsync(BurgerBase burgerBase);

        /// <summary>
        /// Save new burger image instance
        /// </summary>
        /// <param name="burgerImage"></param>
        /// <returns></returns>
        //Task SaveBurgerImageAsync(BurgerImage burgerImage);

        /// <summary>
        /// Physical burger image file
        /// </summary>
        /// <param name="burgerPhoto"></param>
        /// <returns></returns>
        Task<BurgerImage> SaveBurgerPhotoAsync(IFormFile burgerPhoto);
    }
}
