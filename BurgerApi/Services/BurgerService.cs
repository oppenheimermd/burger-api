using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Data;
using BurgerApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BurgerApi.Services
{
    public class BurgerService : IBurgerService
    {
        private readonly BurgerContext _burgerContext;
        private readonly string _folder;
        /// <summary>
        /// Base directory location for media
        /// </summary>
        private readonly string _fileLocationPrefix;

        public BurgerService(BurgerContext burgerContext, IHostingEnvironment env)
        {
            _burgerContext = burgerContext;
            _folder = Path.Combine(env.WebRootPath, "burgerImages");
            _fileLocationPrefix = "/burgerImages/";
        }

        public async Task<IEnumerable<Burger>> GetBurgersAsync(int count, int skip = 0)
        {
            var burgers = await _burgerContext.Burgers
                .AsNoTracking()
                .Where(x => x.Verified == true)
                .Skip(skip)
                .Take(count).ToListAsync();

            return burgers;
        }

        public async Task<IEnumerable<Burger>> GetBurgersByBaseTypeAsync(string baseCode, int count, int skip = 0)
        {
            var burgers = await _burgerContext.Burgers
                .Where(x => x.BurgerBase.BaseNameCode == baseCode)
                .AsNoTracking()
                .Where(x => x.Verified == true)
                .Skip(skip)
                .Take(count).ToListAsync();

            return burgers;
        }

        public async Task<IEnumerable<Burger>> GetBurgersByCuisineAsync(string cuisineCode, int count, int skip = 0)
        {
            var burgers = await _burgerContext.Burgers
                .Where(x => x.Cuisine.CuisineCode == cuisineCode)
                .AsNoTracking()
                .Where(x => x.Verified == true)
                .Skip(skip)
                .Take(count).ToListAsync();

            return burgers;
        }

        public async Task<Burger> GetRandomBurgerAsync()
        {
            var burger = await _burgerContext.Burgers
                .OrderBy(x => Guid.NewGuid()).Take(1).FirstAsync();

            return burger;
        }

        public async Task<BurgerImage> GetRandomBurgerPictureAsync()
        {
            var burger = await _burgerContext.Images
                .OrderBy(x => Guid.NewGuid()).Take(1).FirstAsync();

            return burger;
        }

        public async Task SaveBurgerAsync(Burger burger)
        {
            _burgerContext.Burgers.Add(burger);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task SaveBurgerBaseAsync(BurgerBase burgerBase)
        {
            _burgerContext.BurgerBases.Add(burgerBase);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task SaveCuisineAsync(Cuisine cuisine)
        {
            _burgerContext.Cuisines.Add(cuisine);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task SaveBurgerImageAsync(BurgerImage burgerImage)
        {
            _burgerContext.Images.Add(burgerImage);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task<string> SaveBurgerPhotoAsync(string originalFilename, IFormFile burgerPhoto)
        {
            var newFileName = "";
            using (var ms = new MemoryStream())
            {
                burgerPhoto.CopyTo(ms);
                var fileBytes = ms.ToArray();
                newFileName = await SaveBurgerFileAsync(fileBytes, originalFilename).ConfigureAwait(false);
            }

            return newFileName;
        }

        //  Helpers

        private async Task<string> SaveBurgerFileAsync(byte[] bytes, string fileName)
        {
            var newFileName = DateTime.UtcNow.Ticks.ToString();
            var ext = Path.GetExtension(fileName);

            var relative = $"{newFileName}{ext}";
            var absolute = Path.Combine(_folder, relative);
            var dir = Path.GetDirectoryName(absolute);

            Directory.CreateDirectory(dir);
            using (var writer = new FileStream(absolute, FileMode.CreateNew))
            {
                await writer.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }

            return newFileName + ext;
        }
    }
}
