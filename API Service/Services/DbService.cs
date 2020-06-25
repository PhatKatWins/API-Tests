using API_Service.Models;
using Microsoft.AspNetCore.Hosting.Server.Features;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Service.Services
{
    public class DbService
    {
        private readonly string _dbFile = Path.Combine(AppContext.BaseDirectory, "dataBase.db");

        public Task InsertPhraseAsync(Phrase phrase)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.CreateTable<Phrase>();

                    conn.Insert(phrase);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return Task.CompletedTask;
        }

        public Task<Phrase> GetPhraseAsync(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    return Task.FromResult(conn.Get<Phrase>(id));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return Task.FromResult<Phrase>(null);
        }

        public Task<IEnumerable<Phrase>> GetAllPrhasesAsync()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    return Task.FromResult<IEnumerable<Phrase>>(conn.Table<Phrase>().ToList());
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return Task.FromResult<IEnumerable<Phrase>>(null);
        }

        public Task DeletePhraseAsync(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.Delete<Phrase>(id);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return Task.CompletedTask;
        }

        public Task UpdatePhraseAsync(Phrase phrase)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.Update(phrase);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return Task.CompletedTask;
        }

        public Task InsertVariantAsync(Variant variant)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.CreateTable<Variant>();

                    conn.Insert(variant);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Task.CompletedTask;
            }
        }

        public Task<Variant> GetVariantAsync(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    return Task.FromResult<Variant>(conn.Get<Variant>(id));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Task.FromResult<Variant>(null);
            }
        }

        public Task<IEnumerable<Variant>> GetAllVariantsAsync(int phraseId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    return Task.FromResult<IEnumerable<Variant>>(conn.Table<Variant>().Where(variant => variant.PhraseId == phraseId).ToList());
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Task.FromResult<IEnumerable<Variant>>(null);
            }
        }

        public Task DeleteVariantAsync(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.Delete<Variant>(id);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Task.CompletedTask;
            }
        }

        public Task DeleteAllVariantsAsync(int phraseId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.Table<Variant>().Where(variant => variant.PhraseId == phraseId).Delete();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Task.CompletedTask;
            }
        }

        public Task UpdateVariantAsync(Variant variant)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_dbFile))
            {
                try
                {
                    conn.Update(variant);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Task.CompletedTask;
            }
        }
    }
}
