using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Data;
using BurgerApi.Models;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BurgerApi.Services
{
    public class BurgerService : IBurgerService
    {
        private readonly BurgerContext _burgerContext;
        private readonly string _folder;
        private readonly int _imageSmallSize;
        private readonly int _imageMediumSize;
        private readonly int _imageLargeSize;

        /// <summary>
        /// Base directory location for media
        /// </summary>
        //private readonly string _fileLocationPrefix;

        public BurgerService(BurgerContext burgerContext, IHostingEnvironment env)
        {
            _burgerContext = burgerContext;
            _folder = Path.Combine(env.WebRootPath, "burgerImages");
            //_fileLocationPrefix = "/burgerImages/";
            this._imageSmallSize = 60;
            this._imageMediumSize = 180;
            this._imageLargeSize = 1000;
        }


        public async Task<IEnumerable<Cuisine>> GetCuisineAsync(int count, int skip = 0)
        {
            var cuisine = await _burgerContext.Cuisines
                .OrderBy(x => x.CuisineCode)
                .Skip(skip)
                .AsNoTracking()
                .Take(count).ToListAsync();

            return cuisine;
        }

        public async Task<Cuisine> GetCuisineAsync(int? id)
        {
            var cuisine = await _burgerContext.Cuisines
                .FirstOrDefaultAsync(x => x.Id == id);

            return cuisine;
        }

        public async Task<IEnumerable<BurgerBase>> GetBurgersBaseAsync(int count, int skip = 0)
        {
            var burgersBase = await _burgerContext.BurgerBases
                .AsNoTracking()
                .Skip(skip)
                .Take(count).ToListAsync();

            return burgersBase;
        }

        public async Task<BurgerBase> GetBurgerBaseAsync(int? id)
        {
            var burgerBase = await _burgerContext.BurgerBases
                .FirstOrDefaultAsync(x => x.Id == id);

            return burgerBase;
        }

        public async Task<IEnumerable<Burger>> GetBurgersAsync(int count, int skip = 0)
        {
            var burgers = await _burgerContext.Burgers
                .Include(b => b.BurgerBase).Include(b => b.Cuisine).Include(b => b.Image)
                .AsNoTracking()
                .Skip(skip)
                .Take(count).ToListAsync();

            return burgers;
        }

        public async Task<Burger> GetBurgerAsync(int? id)
        {

            var burger = await _burgerContext.Burgers
                .Include(b => b.BurgerBase)
                .Include(b => b.Cuisine)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(x => x.Id == id);

            return burger;
        }

        public async Task<List<Cuisine>> GetCuisineDropListAsync()
        {
            var cusineList = await _burgerContext.Cuisines.AsNoTracking().ToListAsync();
            return cusineList;
        }

        public async Task<List<BurgerBase>> GetBurgerBaseDropListAsync()
        {
            var burgerbaseList = await _burgerContext.BurgerBases.AsNoTracking().ToListAsync();
            return burgerbaseList;
        }


        //  Persistence

        public async Task SaveCuisineAsync(Cuisine cuisine)
        {
            _burgerContext.Cuisines.Add(cuisine);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateCuisineAsync(Cuisine cuisine)
        {
            try
            {
                _burgerContext.Update(cuisine);
                await _burgerContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuisineExists(cuisine.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                };
            }

        }

        public async Task SaveBurgerBaseAsync(BurgerBase burgerBase)
        {
            _burgerContext.BurgerBases.Add(burgerBase);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateBurgerBaseAsync(BurgerBase burgerBase)
        {
            try
            {
                _burgerContext.Update(burgerBase);
                await _burgerContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerBaseExists(burgerBase.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                };
            }
        }

        public async Task SaveBurgerAsync(Burger burger)
        {
            _burgerContext.Burgers.Add(burger);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task SaveBurgerImage(BurgerImage burgerImage)
        {
            _burgerContext.Images.Add(burgerImage);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateBurgerAsync(Burger burger)
        {
            try
            {
                _burgerContext.Update(burger);
                await _burgerContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerExists(burger.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                };
            }

        }

        /*


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

        */

        public async Task SaveBurgerImageAsync(BurgerImage burgerImage)
        {
            _burgerContext.Images.Add(burgerImage);
            await _burgerContext.SaveChangesAsync();
        }

        public async Task<BurgerImage> SaveBurgerPhotoAsync(IFormFile burgerPhoto)
        {
            BurgerImage burgerImage = null;

            using (var ms = new MemoryStream())
            {
                burgerPhoto.CopyTo(ms);
                var fileBytes = ms.ToArray();
                burgerImage = await SaveBurgerFileAsync(fileBytes, burgerPhoto.FileName).ConfigureAwait(false);
            }

            return burgerImage;
        }

        //  Helpers

        private async Task<BurgerImage> SaveBurgerFileAsync(byte[] bytes, string fileName)
        {
            var newFileName = DateTime.UtcNow.Ticks.ToString();
            var ext = Path.GetExtension(fileName);

            var relative = $"{newFileName}{ext}";
            var absolute = Path.Combine(_folder, relative);
            var dir = Path.GetDirectoryName(absolute);

            Directory.CreateDirectory(dir);
            /*using (var writer = new FileStream(absolute, FileMode.CreateNew))
            {
                await writer.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }*/
            using (var image = new MagickImage(bytes))
            {
                //  set just the width to maintain aspect ratio
                image.Resize(_imageLargeSize, 0);
                image.Write(absolute);
            }

            //  add additional pictures
           

            var burgerImage = new BurgerImage();
            burgerImage.FileNameLarge = newFileName + ext;
            burgerImage.FileNameMedium = await CreateMediumImageAsync(newFileName, ext, bytes);
            burgerImage.FileNameSmall = await CreateThumbnailAsync(newFileName, ext, bytes);

            return burgerImage;

        }

        private Task<string> CreateThumbnailAsync(string fileName, string fileExtension, byte[] bytes)
        {

            return Task.Run<string>(() =>
            {
                var thumbnailFilename = fileName + "-small";
                var relative = $"{thumbnailFilename}{fileExtension}";
                var absolute = Path.Combine(_folder, relative);
                var dir = Path.GetDirectoryName(absolute);

                Directory.CreateDirectory(dir);


                using (var image = new MagickImage(bytes))
                {
                    //  set just the width to maintain aspect ratio
                    image.Resize(_imageSmallSize, 0);
                    image.Write(absolute);
                }

                return thumbnailFilename + fileExtension;
            });

        }

        private Task<string> CreateMediumImageAsync(string fileName, string fileExtension, byte[] bytes)
        {
            return Task.Run<string>(() =>
            {
                var thumbnailFilename = fileName + "-medium";
                var relative = $"{thumbnailFilename}{fileExtension}";
                var absolute = Path.Combine(_folder, relative);
                var dir = Path.GetDirectoryName(absolute);

                Directory.CreateDirectory(dir);


                using (var image = new MagickImage(bytes))
                {
                    //  set just the width to maintain aspect ratio
                    image.Resize(_imageMediumSize, 0);
                    image.Write(absolute);
                }

                return thumbnailFilename + fileExtension;
            });

        }

        private bool CuisineExists(int id)
        {
            return _burgerContext.Cuisines.Any(e => e.Id == id);
        }

        private bool BurgerBaseExists(int id)
        {
            return _burgerContext.BurgerBases.Any(e => e.Id == id);
        }

        private bool BurgerExists(int id)
        {
            return _burgerContext.Burgers.Any(e => e.Id == id);
        }
    }
}
