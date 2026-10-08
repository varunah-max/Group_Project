using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using ProjectClassDefinition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ProjectClassDefinition
{
    public class ChatRepository
    {
        private string connectionString;
        private IDbConnection CreateConnection() => new SqlConnection(connectionString);
        public List<User> Users = new List<User>();
        //!!! Викликати цей метод одразу після ствоерння об'єкту цього класу !!!
        public void getConnection()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("settings.json").Build();
            connectionString = config.GetConnectionString("SqlClient");
        }
        //Достає всих користувачів із бази та записує у ліст користувачів
        public async Task LoadAllUsersAsync()
        {
            try
            {
                const string query = @"select id, username, password, roleId, isOnline, created_at as Created_At from Users;";
                using var connection = CreateConnection();
                var users = await connection.QueryAsync<User>(query);
                Users = users.ToList();
            }
            catch (Exception ex)
            {
            }
        }
        //Перевіряє чи існує користувая за даним username та паролем
        public async Task<bool> isUserExists(string username)
        {
            try
            {
                const string query = @"select from Users where username = @Username";
                using var connection = CreateConnection();
                int count = await connection.ExecuteScalarAsync<int>(query, new { Username = username});
                return count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Змінює статус користувача коли він підєднується чи відєднується,Використовувати під час приєднання чи відєднання клієнта
        public async Task changeUserStatus(string username)
        {
            try
            {
                bool isUser = await isUserExists(username);
                const string query = @"update Users set isOnline = @NewSatus where username = @Username";
                if (isUser)
                {
                    var userInList = Users.FirstOrDefault(u => u.Username == username);
                    using var connection = CreateConnection();
                    int rowsAffected = await connection.ExecuteAsync(query, new { NewStatus = !userInList.IsOnline, Username = username });

                    if (rowsAffected > 0 && userInList != null)
                    {
                        userInList.IsOnline = !userInList.IsOnline;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        //Додає нового користувача до бази та ліста 
        public async Task AddUser(string username, string password, DateTime createdAt)
        {
            try
            {
                bool isUser = await isUserExists(username);
                if (isUser)
                {
                    var newUser = new User();
                    newUser.Username = username;
                    newUser.Password = password;
                    newUser.RoleId = 1;
                    newUser.IsOnline = true;
                    newUser.Created_At = createdAt;
                    Users.Add(newUser);

                    const string query = @"insert into Users (username, password, roleId, isOnline, created_at) 
                                     values(@Username, @Password, @RoleId, @isOnline, @CreatedAt)";
                    using var connection = CreateConnection();
                    int newId = await connection.ExecuteScalarAsync<int>(query, new
                    {
                        Username = newUser.Username,
                        Password = newUser.Password,
                        RoleId = newUser.RoleId,
                        isOnline = newUser.IsOnline,
                        CreatedAt = newUser.Created_At
                    });
                    newUser.Id = newId;
                }
            }
            catch (Exception ex)
            {
            }
            
        }
        //Видаляє користувача із бази та ліста,Використовувати тільки адмінам
        public async Task deleteUser(string username)
        {
            try
            {
                bool isUser = await isUserExists(username);
                if (isUser)
                {
                    const string query = @"delete from Users where username = @Username";
                    using var connection = CreateConnection();
                    var user = Users.FirstOrDefault(u => u.Username == username);
                    Users.Remove(user);
                    await connection.ExecuteAsync(query, new { Username = username });

                }
            }
            catch (Exception ex)
            {
            }

        }
        //Додає нове повідомлення до отримувача та бази
        public async Task AddMessage(User sender, User receiver, string content, DateTime sentAt)
        {
            try
            {
                var newMessage = new Message();

                newMessage.SenderId = sender.Id;
                newMessage.RecieverId = receiver.Id;
                newMessage.Content = content;
                newMessage.SentAt = sentAt;
                newMessage.IsDelivered = false;


                const string query = @"insert into Messages (senderId, recieverId, content, sentAt, isDevivered)
                                output inserted.id
                                values (@SenderId, @RecieverId, @Content, @SentAt, @IsDelivered)";
                using var connection = CreateConnection();
                int insertedId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    SenderId = newMessage.SenderId,
                    RecieverId = newMessage.RecieverId,
                    Content = newMessage.Content,
                    SentAt = newMessage.SentAt,
                    IsDevivered = newMessage.IsDelivered
                });

                newMessage.Id = insertedId;
                sender.Messages.Add(newMessage);
            }
            catch (Exception ex)
            {
            }

        }
        //Дістає повідомлення конкретного користувача
        public async Task getCertainUserMessages(string username)
        {
            try
            {
                bool isUser = await isUserExists(username);
                if (isUser)
                {
                    var user = Users.FirstOrDefault(e => e.Username == username);
                    const string query = @"select id, senderId, recieverId, content, sentAt, isDelivered 
                                    from Messages where recieverId = @UserId
                                    order by sentAt ASC";
                    using var connection = CreateConnection();
                    var dbMessages = await connection.QueryAsync<Message>(query, new { UserId = user.Id });
                    user.Messages = dbMessages.ToList();

                }
            }
            catch(Exception ex)
            {

            }
        }


    }
}
