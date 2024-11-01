﻿using InventoryManagementSystemAPI.Models;
using MongoDB.Driver;
namespace InventoryManagementSystemAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _user;

        // Constructor
        public UserService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("user");
        }

        //create new user
        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        // Get Username
        public User GetByUsername(string username)
        {
            return _user.Find(user => user.Username == username).FirstOrDefault();
        }

        public User GetByEmail(string email)
        {
            return _user.Find(user => user.Email == email).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return _user.Find(user => true).ToList();

        }

        public void UpdateUser(string id, User user)
        {
            _user.ReplaceOne(user => user.Id == id, user);
        }

        public void DeleteUser(string id)
        { 
            _user.DeleteOne(user => user.Id == id);
        }

        public User GetById(string id)
        { 
            return _user.Find(user => user.Id == id).FirstOrDefault();
        }
    }
}
