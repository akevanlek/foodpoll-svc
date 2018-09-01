using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Foodpoll.Models;
using MongoDB.Driver;

namespace Foodpoll.Dac
{
    public class Dac : IDac
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Shop> ShopCollection;
        private readonly IMongoCollection<Poll> PollCollection;
        private readonly IMongoCollection<Member> MemberCollection;


        public Dac()
        {
            client = new MongoClient("mongodb://hackfoodpoll:fVzvjysZNLgDBjwBY9VNJq9hruR42o7jCHeQ2311V9Qf1Q0wnij1nrcUDDBPeQYiGSytGNmcDJotH2pR0SD2Fg==@hackfoodpoll.documents.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=150000&minPoolSize=2");
            database = client.GetDatabase("hackfoodpoll");

            ShopCollection = database.GetCollection<Shop>("shop");
            PollCollection = database.GetCollection<Poll>("poll");
            MemberCollection = database.GetCollection<Member>("member");
        }

        public void CreateMember(Member newmember)
        {
            MemberCollection.InsertOne(newmember);
        }

        public void CreatePoll(Poll newPoll)
        {
            PollCollection.InsertOne(newPoll);
        }

        public void CreateShop(Shop newshop)
        {
            ShopCollection.InsertOne(newshop);
        }

        public Member GetMember(Expression<Func<Member, bool>> expression)
        {
            return MemberCollection.Find(expression).FirstOrDefault();
        }

        public Poll GetPoll(Expression<Func<Poll, bool>> expression)
        {
            return PollCollection.Find(expression).FirstOrDefault();
        }

        public Shop GetShop(Expression<Func<Shop, bool>> expression)
        {
            return ShopCollection.Find(expression).FirstOrDefault();
        }

        public IEnumerable<Member> ListMember(Expression<Func<Member, bool>> expression)
        {
            return MemberCollection.Find(expression).ToList();
        }

        public IEnumerable<Poll> ListPoll(Expression<Func<Poll, bool>> expression)
        {
            return PollCollection.Find(expression).ToList();
        }

        public IEnumerable<Shop> ListShop(Expression<Func<Shop, bool>> expression)
        {
            return ShopCollection.Find(expression).ToList();
        }

        public void UpdateMember(Member member)
        {
            MemberCollection.ReplaceOne(x => x._id == member._id, member);
        }

        public void UpdatePoll(Poll poll)
        {
            PollCollection.ReplaceOne(x => x._id == poll._id, poll);
        }

        public void UpdateShop(Shop shop)
        {
            ShopCollection.ReplaceOne(x => x._id == shop._id, shop);
        }
    }
}
