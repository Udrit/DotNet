﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdritDhakal.Repositories
{
    public class UnitOFWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitOFWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public IGenericRepository<T> GenericRepository<T>() where T : class 
        {
            IGenericRepository<T> repo = new GenericRepository<T>(_context);
            return repo;
        }
        public void Save() 
        {
            _context.SaveChanges();
        }
        
    }
}
