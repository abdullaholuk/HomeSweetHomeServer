using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Contexts;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Controllers;
using HomeSweetHomeServer.Services;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    //Operates base repository operations
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public DatabaseContext _context;

        //Constructs database context
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        //Adds given model to database
        public void Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

    }
}
