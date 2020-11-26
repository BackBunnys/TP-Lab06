using System;
using System.Collections.Generic;

using WebAppTDD.Data;
using WebAppTDD.Models;

namespace WebAppTDD.Tests.Mocks
{
    class ClassesMockRepository : IRepository<Class>
    {
        private Dictionary<long, Class> classes = new Dictionary<long, Class>();
        private int lastInsertedIndex = 0;

        public int LastInsertedIndex => lastInsertedIndex;

        public void Add(Class obj)
        {
            if (obj != null)
            {
                obj.Id = ++lastInsertedIndex;
                classes.Add(lastInsertedIndex, obj);
            }
            else throw new ArgumentException();
        }

        public void Delete(int id)
        {
            if (!classes.Remove(id)) throw new ArgumentException();
        }

        public void Edit(int id, Class obj)
        {
            Class cl;
            if (classes.ContainsKey(id))
                classes[id] = obj;
            else throw new ArgumentException();
        }

        public IEnumerable<Class> GetAll()
        {
            return classes.Values;
        }

        public Class GetById(int id)
        {
            Class cl;
            if (classes.TryGetValue(id, out cl))
                return cl;
            else throw new ArgumentException();
        }
    }
}
